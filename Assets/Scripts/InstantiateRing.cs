﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateRing : MonoBehaviour
{   
    public GameObject _sampleCubePrefab;
    GameObject[] _sampleCube = new GameObject[512];
    public float _maxScale;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 512; i++){
            GameObject _instanceSampleCube = (GameObject) Instantiate(_sampleCubePrefab);
            _instanceSampleCube.transform.position = this.transform.position;
            _instanceSampleCube.transform.parent = this.transform;
            _instanceSampleCube.name = "SampleCube" + "_" + i;
            this.transform.eulerAngles = new Vector3(0, -360f/512f * i, 0);
            _instanceSampleCube.transform.position = Vector3.forward * 50;
            _sampleCube[i] = _instanceSampleCube;
        }        
        this.transform.eulerAngles = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < _sampleCube.Length; i++){
           
            if(_sampleCube != null){
                _sampleCube[i].transform.localScale = new Vector3(2, Mathf.Pow((MainAudio._samples[i] * _maxScale),0.7f)*2 + 2 ,2);
                _sampleCube[i].transform.localPosition = new Vector3(
                    _sampleCube[i].transform.localPosition.x, 
                    Mathf.Pow((MainAudio._samples[i] * _maxScale),0.7f)*2/2,
                    _sampleCube[i].transform.localPosition.z
                    );
                //
            }
        }
    }
}
