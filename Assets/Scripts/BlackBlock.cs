using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBlock : MonoBehaviour {

    GameObject childNeedle;
	// Use this for initialization
	void Start ()
    {
        childNeedle = gameObject.transform.Find("needle").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            childNeedle.GetComponent<Animator>().SetTrigger("startNeedle");
        }
    }
}
