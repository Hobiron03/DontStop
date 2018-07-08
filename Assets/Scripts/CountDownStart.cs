using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownStart : MonoBehaviour {

    public GameObject uiController;
    public GameObject gameController;

    public GameObject countDownUI;
    TextMeshProUGUI countDown;

	// Use this for initialization
	void Start ()
    {
        uiController.SetActive(false);
        countDown = countDownUI.GetComponent<TextMeshProUGUI>();
        StartCoroutine(CountDownCoroutine());

       // Invoke("GameStart", 3.0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    IEnumerator CountDownCoroutine()
    {
        countDownUI.SetActive(true);
        yield return new WaitForSeconds(1.0f);

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
        gameController.GetComponent<GameController>().GameStart();
    }
}
