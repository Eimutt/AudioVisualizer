using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class MainAudio : MonoBehaviour
{
    AudioSource _audioSource;
    public static float[] _samples = new float[512];
    public static float[] _freqBand = new float[8];
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.time = _audioSource.clip.length * 0.3f;
        _audioSource.volume = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        getSpectrumAudioSource();
        MakeFreqBands();
    }

    void getSpectrumAudioSource(){
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
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
