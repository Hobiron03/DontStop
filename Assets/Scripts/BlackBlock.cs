using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBlock : MonoBehaviour {

    GameObject childNeedle;
    MeshRenderer mesh;
	// Use this for initialization
	void Start ()
    {
        mesh = GetComponent<MeshRenderer>();
        childNeedle = gameObject.transform.Find("needle").gameObject;
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position.z >= 11f)
        {
            childNeedle.SetActive(false);
        }
        else if (transform.position.z < 11f)
        {
           
            childNeedle.SetActive(true);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            childNeedle.GetComponent<Animator>().SetTrigger("startNeedle");
        }
    }
}
