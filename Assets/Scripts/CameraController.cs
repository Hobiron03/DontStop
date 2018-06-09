using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour {

    [SerializeField] float duration;
    [SerializeField] float strength = 1f;
    [SerializeField] int vibrato = 60;
    [SerializeField] float randomness = 130f;
    [SerializeField] bool snapping = false;


    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
    
	}


    public void ShakeCamera()//ダメージを受けた時の揺れ演出
    {
        gameObject.transform.DOShakePosition(0.5f, new Vector3(0.4f, 0.4f, 0.1f), vibrato, randomness, snapping);
    }
}
