using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public GameObject FadeCanvas;
    Fade fade;

    public GameObject soundControllButtonImage;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    public int num = 2;

    // Use this for initialization
    void Start ()
    {
        if (GameData.Instance.isSoundOn)
        {
            soundControllButtonImage.GetComponent<Image>().sprite = soundOnSprite;
        }
        else
        {
            soundControllButtonImage.GetComponent<Image>().sprite = soundOffSprite;
        }

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

    public void LoadGameScene()
    {
        fade.FadeIn(0.5f);
        Invoke("LoadMain", 0.5f);

    }
    void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void PushSoundButton()
    {
        if(GameData.Instance.isSoundOn)
        {
            soundControllButtonImage.GetComponent<Image>().sprite = soundOffSprite;
            GameData.Instance.isSoundOn = false;
        }
        else
        {
            soundControllButtonImage.GetComponent<Image>().sprite = soundOnSprite;
            GameData.Instance.isSoundOn = true;
        }

    }
}
