//Time,score,poseのUI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour {

    public GameObject gameController;

    public GameObject timerUI;
    TextMeshProUGUI timeScript;

    public GameObject distanceUI;
    TextMeshProUGUI distScript;
    private int dist = 0;

    public GameObject PoseUI;

    private float CanPlayTime = 31;
    private float count;

	// Use this for initialization
	void Start ()
    {

        timeScript = timerUI.GetComponent<TextMeshProUGUI>();
        timeScript.text = CanPlayTime.ToString();

        distScript = distanceUI.GetComponent<TextMeshProUGUI>();
        distScript.text = dist.ToString();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //タイムを1秒ずつ減らしていく
        count += Time.deltaTime;
        timeScript.text = Mathf.Floor(CanPlayTime - count).ToString();

        //キョリを上げていく
	}

    //スコアである距離を伸ばす
    public void IncreaseDist()
    {
        dist += 1;
        distScript.text = dist.ToString();
    }
    public void DecreaseDist()
    {
        dist = dist > 0 ? dist - 1 : dist;
        distScript.text = dist.ToString();
    }

    public void PushPoseButton()
    {
        gameController.GetComponent<GameController>().Pause();
        PoseUI.GetComponent<AudioSource>().Play();
    }

    public void PushContinueButton()
    {
        gameController.GetComponent<GameController>().CancelPause();
    }

    public void PushRestartButton()
    {
        gameController.GetComponent<GameController>().Restart();
    }
}
