//Time,score,poseのUI管理（見た目だけ）

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour {

    public GameObject timer;
    TextMeshProUGUI timeScript;

    public GameObject score;
    TextMeshProUGUI scoreScript;

    private float CanPlayTime = 30;
    private float count;

	// Use this for initialization
	void Start ()
    {
        timeScript = timer.GetComponent<TextMeshProUGUI>();
        timeScript.text = CanPlayTime.ToString();
        scoreScript = score.GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //タイムを1秒ずつ減らしていく
        count += Time.deltaTime;
        timeScript.text = Mathf.Floor(CanPlayTime - count).ToString();

        //キョリを上げていく
	}
}
