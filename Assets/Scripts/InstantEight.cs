    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantEight : MonoBehaviour
{   
    public GameObject _sampleCubePrefab;
    GameObject[] _sampleCube = new GameObject[8];
    public float _maxScale;
    public float _scaleMult;
    public float _startScale;
    public float startPos;
    public float posStep = 10;
    public float baseSize;
    public bool useBuffer = true;
    Material material;
    Color color;

    // Start is called before the first frame update
    void Start()
    {

        startPos = -40 + posStep/2;
        for(int i = 0; i < _sampleCube.Length ; i++){
            GameObject _instanceSampleCube = (GameObject) Instantiate(_sampleCubePrefab);
            _instanceSampleCube.transform.position = this.transform.position;
            _instanceSampleCube.transform.parent = this.transform;
            _instanceSampleCube.name = "SampleCubeMiddle" + "_" + i;
            
            _instanceSampleCube.transform.position += new Vector3(startPos + posStep*i,0,0);
            _sampleCube[i] = _instanceSampleCube;
        }        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < _sampleCube.Length; i++){
            if(_sampleCube != null){
                if(useBuffer == true){
                    _sampleCube[i].transform.localScale = new Vector3(baseSize, Mathf.Min(_maxScale ,MainAudio._audioBandBuffer[i] * _scaleMult) + _startScale ,baseSize);
                    _sampleCube[i].transform.localPosition = new Vector3(
                        _sampleCube[i].transform.localPosition.x, 
                        Mathf.Min(_maxScale ,MainAudio._audioBandBuffer[i] * _scaleMult) /2,
                        _sampleCube[i].transform.localPosition.z
                        );
                    color = new Color(MainAudio._audioBandBuffer[i],MainAudio._audioBandBuffer[i],MainAudio._audioBandBuffer[i]);
                }else{
                    _sampleCube[i].transform.localScale = new Vector3(baseSize, Mathf.Min(_maxScale ,MainAudio._freqBand[i] * _scaleMult) + _startScale ,baseSize);
                    _sampleCube[i].transform.localPosition = new Vector3(
                        _sampleCube[i].transform.localPosition.x, 
                        Mathf.Min(_maxScale ,MainAudio._freqBand[i] * _scaleMult) /2,
                        _sampleCube[i].transform.localPosition.z
                        );
                    color = new Color(MainAudio._audioBand[i],MainAudio._audioBand[i],MainAudio._audioBand[i]);
                }
                 _sampleCube[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(MainAudio._audioBand[i]*0.05f,MainAudio._audioBand[i]*0.05f,MainAudio._audioBand[i]*0.05f));
            }         
        }
    }
}

