//Playerのアニメーションとエフェクト、効果音管理
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAnimAndEffect : MonoBehaviour {

    public GameObject destroyEffect;
    public AudioClip audioClip;
    private Vector3 effectPos;

    private AudioSource audio;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BlackBlock")
        {
            PlayerDestroy();
        }
    }


    void PlayerDestroy()
    {
        effectPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        Instantiate(destroyEffect, effectPos, Quaternion.identity);
        Destroy(gameObject);
    }
}
