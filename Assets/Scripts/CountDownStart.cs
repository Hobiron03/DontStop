using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownStart : MonoBehaviour {

    public GameObject player;
    public GameObject uiController;
    public GameObject gameController;

    public GameObject countDownUI;
    TextMeshProUGUI countDown;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(CountDownCoroutine());


        player.GetComponent<PlayerController>().enabled = false;
        uiController.SetActive(false);
        countDown = countDownUI.GetComponent<TextMeshProUGUI>();
       

       // Invoke("GameStart", 3.0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    IEnumerator CountDownCoroutine()
    {
        countDownUI.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        countDown.text = "3";
        yield return new WaitForSeconds(1.0f);

        countDown.text = "2";
        yield return new WaitForSeconds(1.0f);

        countDown.text = "1";
        yield return new WaitForSeconds(1.0f);

        countDown.text = "スタート";
        yield return new WaitForSeconds(1.0f);

        countDownUI.SetActive(false);
        GameStart();
    }

    void GameStart()
    {
        player.GetComponent<PlayerController>().enabled = true;
        gameController.GetComponent<GameController>().GameStart();
    }
}
