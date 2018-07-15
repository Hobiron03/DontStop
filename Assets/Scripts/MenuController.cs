using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public GameObject FadeCanvas;
    Fade fade;

	// Use this for initialization
	void Start ()
    {
        Time.timeScale = 1.0f;
      
        fade = FadeCanvas.GetComponent<Fade>();
       
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadGameScene();
        }

	}

    void LoadGameScene()
    {
        fade.FadeIn(0.5f);
        Invoke("LoadMain", 0.5f);
    }
    void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }
}
