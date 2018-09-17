using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkyController : MonoBehaviour {

    private bool DontDestroyEnable = true;
	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.Rotate(0,Time.deltaTime * 3.0f,0);	
	}
}
