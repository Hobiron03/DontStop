using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject pauseUI;
    public GameObject bgmObject;
    public GameObject uiController;
    public GameObject fadeCanvas;

    public GameObject canvas;

    Fade fade;

    private int countdown;
    private int getCoinNum = 0; //獲得したコインの数

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
        bgmObject.GetComponent<AudioSource>().volume = 0.2f;
    }
    public void CancelPause()//Pause解除！！
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        bgmObject.GetComponent<AudioSource>().volume = 0.7f;
    }

    public void returnTitle()
    {
        SceneManager.LoadSceneAsync("Title");
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    public void AddCoinScore()
    {
        getCoinNum += 1;
        Debug.Log(getCoinNum);
    }

    void FadeLoad()
    {
        fade = fadeCanvas.GetComponent<Fade>();//シーン読み込み時にfadeしながら読み込む
        fade.FadeIn(0.2f);
        Invoke("FadeLoad2", 0.3f);
    }
    void FadeLoad2()
    {
        canvas.SetActive(false);
        fade.FadeOut(0.5f);
    }
}
