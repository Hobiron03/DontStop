//Time,score,poseのUI
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIController : MonoBehaviour {

    public GameObject gameController;

    public GameObject playSEObj;
    private AudioSource seAudioSource;

    public GameObject resultUI;
    public GameObject finishUI;
    public GameObject systemUI;

    public GameObject mainasUI;
    Vector3 mainasUIPos = new Vector3(135.4f, -7.0f, 0);
    Vector3 mainusInitPos = new Vector3(93f, 11f, 0f);

    public GameObject timerUI;
    TextMeshProUGUI timeScript;
    private RectTransform timerUITransform;

    public GameObject distanceUI;
    TextMeshProUGUI distScript;
    private int dist = 0;

    public GameObject CoinUI;
    TextMeshProUGUI coinScript;
    private int coinNum = 0;

    public GameObject PoseUI;

    public GameObject resultCoinUI;
    TextMeshProUGUI resultCoinScript;
    private int resultCoinUINum = 0;

    public GameObject resultDistUI;
    TextMeshProUGUI resultDistScript;
    private int resultDistNum = 0;

    public GameObject resultScoreUI;
    TextMeshProUGUI resultScoreScript;
    private int resultScoreNum = 0;

    public GameObject continueButtonUI;
    public GameObject restartButtonUI;
    public AudioClip buttonSE;
    public AudioClip resultSE;
    public AudioClip resultSE2;

    private float CanPlayTime = 30.0f;
    private float count;

    private bool isTimeUp = false;
    private bool rest10minit = true;
    private bool isStartPerform = false;
    private float timeInterval = 1.0f;
    private float time = 1.0f;

    public int gameScore = 0;
    public float seVolume = 1.0f;

    private IEnumerator resultDisplayCoroutine()
    {
        DisplayFinishText();
        resultUI.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        DisplayRsult();
        yield return new WaitForSeconds(0.4f);

        DOTween.To(() => ResultCoinNum, (x) => ResultCoinNum = x, coinNum, 0.4f);
        yield return new WaitForSeconds(0.4f);
        seAudioSource.PlayOneShot(resultSE);


        DOTween.To(() => ResultDistNum, (x) => ResultDistNum = x, dist, 0.4f);
        yield return new WaitForSeconds(0.4f);
        seAudioSource.PlayOneShot(resultSE);

        yield return new WaitForSeconds(0.3f);

        DOTween.To(() => ResultScoreNum, (x) => ResultScoreNum = x, gameScore, 0.6f);
        yield return new WaitForSeconds(0.5f);
        seAudioSource.PlayOneShot(resultSE2);
        resultScoreUI.GetComponent<RectTransform>().DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.2f);


    }

    public int ResultCoinNum
    {
        set
        {
            resultCoinUINum = value;
            resultCoinScript.text = resultCoinUINum.ToString();
        }
        get
        {
            return resultCoinUINum;
        }
    }


    public int ResultDistNum
    {
        set
        {
            resultDistNum = value;
            resultDistScript.text = resultDistNum.ToString();
        }
        get
        {
            return resultDistNum;
        }
    }

    public int ResultScoreNum
    {
        set
        {
            resultScoreNum = value;
            resultScoreScript.text = resultScoreNum.ToString();
        }
        get
        {
            return resultScoreNum;
        }
    }

    private void Awake()
    {
        if (GameData.Instance.isSoundOn)
        {
            seVolume = GameData.Instance.seVolume;
        }
        else
        {
            seVolume = 0;
        }
        seAudioSource = playSEObj.GetComponent<AudioSource>();
        seAudioSource.volume = seVolume;
    }

    // Use this for initialization
    void Start ()
    {
        timeScript = timerUI.GetComponent<TextMeshProUGUI>();
        timeScript.text = CanPlayTime.ToString("F1");
        timerUITransform = timerUI.GetComponent<RectTransform>();

        distScript = distanceUI.GetComponent<TextMeshProUGUI>();
        distScript.text = dist.ToString();

        coinScript = CoinUI.GetComponent<TextMeshProUGUI>();
        coinScript.text = coinNum.ToString();

        resultCoinScript = resultCoinUI.GetComponent<TextMeshProUGUI>();
        resultCoinScript.text = resultCoinUINum.ToString();

        resultDistScript = resultDistUI.GetComponent<TextMeshProUGUI>();
        resultDistScript.text = resultDistNum.ToString();

        resultScoreScript = resultScoreUI.GetComponent<TextMeshProUGUI>();
        resultScoreScript.text = resultScoreNum.ToString();

        

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

    public void IncreaseCoin()
    {
        coinNum += 1;
        coinScript.text = coinNum.ToString();
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

            gameController.GetComponent<GameController>().GameFinish();
            gameScore = coinNum * 2 + dist;
            StartCoroutine("resultDisplayCoroutine");
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
                    timerUITransform.DOScale(new Vector3(1.34f, 1.34f, 1.34f), 0.3f);
                })
                .Append(timerUITransform.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.7f));

            timerSequence.Play();

            time = 0;
        }
    }

    private void DisplayFinishText()
    {
        finishUI.SetActive(true);

        Sequence FinishTextSequence = DOTween.Sequence()
                .OnStart(() =>
                {
                    finishUI.GetComponent<RectTransform>().DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.2f);
                })
                .AppendInterval(0.9f)
                .Append(finishUI.GetComponent<RectTransform>().DOScale(new Vector3(0.01f, 0.01f, 0.01f), 0.2f));

        FinishTextSequence.Play();

        
    }

    void DisplayRsult()
    {
        systemUI.SetActive(false);
        
        resultUI.GetComponent<RectTransform>().DOLocalMoveY(0, 0.3f);
    }

    public void PlayerDamagedUI()
    {
        var mainasUIInitTransform = mainasUI.GetComponent<RectTransform>();

        mainasUI.SetActive(true);
        mainasUIInitTransform.DOLocalMove(mainasUIPos, 1.0f);

        Invoke("SetActiveFalse", 1.0f);

        coinNum -= 5;
        if(coinNum > 0)
        {
            coinScript.text = coinNum.ToString();
        }
        else
        {
            coinNum = 0;
            coinScript.text = "0";
        }
        
    }
    public void SetActiveFalse()
    {
        mainasUI.SetActive(false);
        mainasUI.GetComponent<RectTransform>().localPosition = mainusInitPos;
    }

    public void PushPoseButton()
    {
      
        gameController.GetComponent<GameController>().Pause();
        seAudioSource.PlayOneShot(buttonSE);
    }

    public void PushContinueButton()
    {
       
        gameController.GetComponent<GameController>().CancelPause();
        seAudioSource.PlayOneShot(buttonSE);
    }

    public void PushRestartButton()
    {

        gameController.GetComponent<GameController>().Restart();
        Debug.Log("Helllo");
        seAudioSource.PlayOneShot(buttonSE);
    }
}
