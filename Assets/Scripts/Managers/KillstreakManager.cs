using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillstreakManager : MonoBehaviour
{
    public static int killTimer = 0;
    public static int enemiesKilled = 0;

    [SerializeField] private UIManager UIM;

    // Update is called once per frame
    private void Awake()
    {
        enemiesKilled = 0;
    }
    void FixedUpdate()
    {
        if (enemiesKilled <= 0)
            return;
        UIM.DrawSpree(enemiesKilled);
        InvokeRepeating("Countdown", 1.0f, 1.0f);
    }
    void Countdown()
    {
        if (--killTimer == 0)
        {
            enemiesKilled = 0;
            UIM.UndrawSpree();
            CancelInvoke("Countdown");
        }
    }
}
