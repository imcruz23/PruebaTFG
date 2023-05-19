using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveStatsComponent : MonoBehaviour
{
    // Para guardar
    public LevelManager LM;
    public TimerManager TM;
    // Para enseñar
    public TMP_Text scoreT;
    public TMP_Text timeT;

    private float time;
    private int score;

    public void SavePlayerStats()
    {
        PlayerPrefs.SetFloat("time", TM.time);
        PlayerPrefs.SetInt("score", LM.score);
    }

    public void GetPlayerScore()
    {
       score =  PlayerPrefs.GetInt("score");
    }

    public void GetPlayerTime()
    {
        time = PlayerPrefs.GetFloat("time");
    }

    public void DrawPlayerScore()
    {
        GetPlayerScore();
        scoreT.text = "Score: " + score;
    }

    public void DrawPlayerTime()
    {
        GetPlayerTime();
        int minutes = (int)(time / 60f);
        int seconds = (int)(time - minutes * 60f);
        int cents = (int)((time - (int)time) * 10f);

        timeT.text = "Time: " + string.Format("{0}:{1}:{2}", minutes, seconds, cents);
    }
}
