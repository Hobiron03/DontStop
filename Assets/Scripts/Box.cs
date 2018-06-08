using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

    MeshRenderer mesh;
	// Use this for initialization
	void Start () {
        mesh = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.z >= 11f)
        {
            mesh.enabled = false;
        }
        else if (transform.position.z < 12f)
        {
            mesh.enabled = true;
        }

	}
}
