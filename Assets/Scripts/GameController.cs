using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject pauseUI;
    public GameObject bgmObject;
    public GameObject seObject;
    public GameObject uiController;
    public GameObject fadeCanvas;

    public GameObject canvas;

    public GameObject player;

    public AudioClip buttonSE;

    Fade fade;

    private int countdown;
    private int getCoinNum = 0; //獲得したコインの数

    public float bgmVolume = 0.7f;
    public float poseVolume = 0.2f;
    public float seVolume = 1.0f;

    GameObject menuController;

    private void Awake()
    {
        if (GameData.Instance.isSoundOn)
        {
            bgmVolume = GameData.Instance.gameBgmVolume;
            poseVolume = GameData.Instance.poseBgmVolume;
            seVolume = GameData.Instance.seVolume;
        }
        else
        {
            bgmVolume = 0;
            poseVolume = 0;
            seVolume = 0;
        }
        bgmObject.GetComponent<AudioSource>().volume = bgmVolume;
        seObject.GetComponent<AudioSource>().volume = seVolume;
    }

    // Use this for initialization
    void Start ()
    {
        Time.timeScale = 1.0f; //シーン遷移時にアニメーションが動かなくるバグの回避処理
        FadeLoad(); //fadeoutしながら遷移する
       
    }
	
	// Update is called once per frame
	void Update ()
    {
	    	
	}

    public void GameStart()
    {
        uiController.SetActive(true);

        bgmObject.SetActive(true);
    }

    public void Pause()//一時停止！！
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        //ポーズ中はbgmの音量を小さくする
        bgmObject.GetComponent<AudioSource>().volume = poseVolume;
    }
    public void CancelPause()//Pause解除！！
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        bgmObject.GetComponent<AudioSource>().volume = bgmVolume;
    }

    public void returnTitle()
    {
        LoadTitleScene();
        seObject.GetComponent<AudioSource>().Play();
    }

    public void Restart()
    {
        ReloadMainScene();
        seObject.GetComponent<AudioSource>().Play();
    }

    public void AddCoinScore()
    {
        uiController.GetComponent<UIController>().IncreaseCoin();
        getCoinNum += 1;
        Debug.Log(getCoinNum);
    }

    public void GameFinish()
    {
        player.GetComponent<PlayerController>().enabled = false;
        bgmObject.SetActive(false);
    }


    void ReloadMainScene()
    {
        Time.timeScale = 1.0f;
        fade.FadeIn(0.5f);
        Invoke("LoadMain", 0.5f);
    }
    void LoadTitleScene()
    {
        Time.timeScale = 1.0f;
        fade.FadeIn(0.5f);
        Invoke("LoadMenu", 0.5f);
    }

    void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }
    void LoadMenu()
    {
        SceneManager.LoadSceneAsync("Title");
    }


    //fade start
    void FadeLoad()
    {
        fade = fadeCanvas.GetComponent<Fade>();//シーン読み込み時にfadeしながら読み込む
        fade.FadeIn(0.1f);
        Invoke("FadeLoad2", 0.5f);
    }
    void FadeLoad2()
    {
        canvas.SetActive(false);
        fade.FadeOut(0.5f);
    }
    //fade end
}
