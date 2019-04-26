using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantSphere : MonoBehaviour
{   
    public GameObject _spherePrefab; 
    public int bandID = 0;
    GameObject sphere;
    // Start is called before the first frame update
    void Start()
    {
        sphere = (GameObject) Instantiate(_spherePrefab);
        sphere.transform.position = this.transform.position + new Vector3(0f,15f,10f);
        sphere.transform.parent = this.transform;
        sphere.transform.localScale = new Vector3(10f,10f,10f);
    }

    // Update is called once per frame
    void Update()
    {
        sphere.transform.localScale = new Vector3(10f + MainAudio._audioBand[bandID]*2, 10f + MainAudio._audioBand[bandID]*2,10f + MainAudio._audioBand[bandID]*2);
    }
}
