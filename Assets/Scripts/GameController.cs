using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject pauseUI;
    public GameObject bgmObject;

    private int countdown;
    private int getCoinNum = 0; //獲得したコインの数

	// Use this for initialization
	void Start ()
    {
       // Time.timeScale = 0f;	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    	
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
        SceneManager.LoadScene("Title");
    }

    public void AddCoinScore()
    {
        getCoinNum += 1;
        Debug.Log(getCoinNum);
    }
}
