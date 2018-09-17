using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData{

    public readonly static GameData Instance = new GameData();

    public int score = 0;

    public bool isSoundOn = true;

    public float gameBgmVolume = 0.7f;
    public float poseBgmVolume = 0.2f;
    public float seVolume = 1.0f;

}
