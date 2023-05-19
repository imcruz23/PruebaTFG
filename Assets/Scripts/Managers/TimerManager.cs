using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    // Cronometro para poder hacer speedruns
    public float time;
    [HideInInspector] public int minutes, seconds, cents;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        minutes = (int) (time / 60f);
        seconds = (int) (time - minutes * 60f);
        cents = (int)((time - (int)time) * 10f);
    }
}
