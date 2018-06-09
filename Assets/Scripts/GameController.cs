using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject pauseUI;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    	
	}


    public void Pause()//一時停止！！
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CancelPause()//Pause解除！！
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
