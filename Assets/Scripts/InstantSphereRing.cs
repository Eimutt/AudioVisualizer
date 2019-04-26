using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantSphereRing : MonoBehaviour
{
    public GameObject _spherePrefab; 
    public int nrOfSpheres = 20;
    public float spinSpeed = 10f;
    GameObject[] spheres;

    // Start is called before the first frame update
    void Start()
    {
        spheres = new GameObject[nrOfSpheres];
        for(int i = 0; i < nrOfSpheres; i++){
            spheres[i] = (GameObject) Instantiate(_spherePrefab);
            spheres[i].transform.position = this.transform.position;
            spheres[i].transform.parent = this.transform;
            spheres[i].name = "ringSphere_" + i;
            this.transform.eulerAngles = new Vector3(0, 360f/nrOfSpheres * i, 0);
            spheres[i].transform.position = Vector3.forward * 60 + new Vector3(0f,10f,0f);
            spheres[i].transform.localScale = new Vector3(5f,5f,5f);
        }
        this.transform.eulerAngles = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < nrOfSpheres; i++){
            this.transform.eulerAngles = new Vector3(0, 360f/nrOfSpheres * i, 0);
            spheres[i].transform.RotateAround(new Vector3(0f,0f,0f), Vector3.up, 10f * Time.deltaTime);
            spheres[i].transform.localPosition = new Vector3(spheres[i].transform.localPosition.x, Mathf.Sin(Time.time) * 5f + 10f, spheres[i].transform.localPosition.z);
            //spheres[i].transform.localPosition = new Vector3(spheres[i].transform.position.x, Mathf.Sin(Time.time)* Time.deltaTime,spheres[i].transform.localPosition.z);
        }
        this.transform.eulerAngles = new Vector3(0,0,0);

    }
}
