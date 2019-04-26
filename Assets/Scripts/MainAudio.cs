using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class MainAudio : MonoBehaviour
{
    AudioSource _audioSource;
    public static float[] _samples = new float[512];
    public static float[] _freqBand = new float[8];
    public static float[] _bandBuffer = new float[8];
    float[] _bufferDecrease = new float[8];
    float[] highestBands = new float[8];

    public static float[] _audioBand = new float[8];
    public static float[] _audioBandBuffer = new float[8];
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.time = _audioSource.clip.length * 0.3f;
        //_audioSource.volume = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        getSpectrumAudioSource();
        MakeFreqBands();
        BandBuffer();
        createAudioBands();
    }

    void getSpectrumAudioSource(){
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void BandBuffer(){
        for(int i = 0; i < 8; i++){
            if(_freqBand[i] > _bandBuffer[i]){
                _bandBuffer[i] = _freqBand[i];
                _bufferDecrease[i] = 0.005f;
            }else{
                _bandBuffer[i] -= _bufferDecrease[i];
                _bufferDecrease[i] *= 1.2f;
            }
        }
    }

    void createAudioBands(){
        for(int i = 0; i < 8; i++){
            if(_freqBand[i] > highestBands[i]){
                highestBands[i] = _freqBand[i];
            }
            _audioBand[i] = (_freqBand[i] / highestBands[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / highestBands[i]);
        }
    }

    //Divide the audio to 8 floats
    //Original hertz is 22050, we have 512 samples.
    //43 hertz per sample
    //
    void MakeFreqBands(){
        int count = 0;
        for (int i = 0; i < 8; i++){
            float sampleAverage = 0;
            int sampleCount = (int) Mathf.Pow(2,i) * 2;
            if(i == 7){
                sampleCount += 2;
            }
            for(int j = 0; j < sampleCount; j++){
                sampleAverage += _samples[count] * (count + 1);
                count++;
            }

            sampleAverage /= count;

            _freqBand[i] = sampleAverage*10;
        }
    }
}
