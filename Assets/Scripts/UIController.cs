//Time,score,poseのUI
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIController : MonoBehaviour {

    public GameObject gameController;

    public GameObject timerUI;
    TextMeshProUGUI timeScript;
    private RectTransform timerUITransform;

    public GameObject distanceUI;
    TextMeshProUGUI distScript;
    private int dist = 0;

    public GameObject PoseUI;

    private float CanPlayTime = 30.0f;
    private float count;

    private bool isTimeUp = false;
    private bool rest10minit = true;
    private bool isStartPerform = false;
    private float timeInterval = 1.0f;
    private float time = 1.0f;

	// Use this for initialization
	void Start ()
    {
        timeScript = timerUI.GetComponent<TextMeshProUGUI>();
        timeScript.text = CanPlayTime.ToString("F1");
        timerUITransform = timerUI.GetComponent<RectTransform>();

        distScript = distanceUI.GetComponent<TextMeshProUGUI>();
        distScript.text = dist.ToString();


	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isTimeUp)
        {

        }
        else
        {
            CountTime();
        }

        if(isStartPerform)
        {
            UIPerformance();
        }


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

    public void CountTime()
    {
        //タイムを1秒ずつ減らしていく
        count += Time.deltaTime;
       
        timeScript.text = (CanPlayTime - count).ToString("F1");

        if(rest10minit)
        {
            if(count >= 20.0f)
            {
                timeScript.color = new Color(251f, 255f, 0);
                isStartPerform = true;
                //timeScript.fontSize = 46;
                
                rest10minit = false;
            }

        }


        if(count >= 30.0f)
        {
            timeScript.text = 0.0f.ToString("F1");
            isTimeUp = true;
            isStartPerform = false;
        }
    }

    public void UIPerformance()
    {
        time += Time.deltaTime;
        if (time >= timeInterval)
        {
            Sequence timerSequence = DOTween.Sequence()
                .OnStart(() =>
                {
                    timerUITransform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 0.3f);
                })
                .Append(timerUITransform.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.7f));

            timerSequence.Play();

            time = 0;
        }
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
