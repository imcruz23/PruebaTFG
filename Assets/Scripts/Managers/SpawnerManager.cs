using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// La idea es hacer un spawn por tiempo en el que pases un enemigo y lo spawnee cada x segundos.
public class SpawnerManager : MonoBehaviour
{
    // Enemigo que necesitamos
    public GameObject enemy;

    // Flag que va a comprobar si el trigger se ha activado
    [HideInInspector] public bool trigger;

    // Timer
    public float timeToSpawn;

    // Enemigos maximos
    public int enemiesToSpawn;

    private void Start()
    {
        trigger = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!trigger)
            return;
        if (enemiesToSpawn <= 0)
            return;
        InvokeRepeating("Countdown", 1.0f, timeToSpawn);
    }

    void Countdown()
    {
        print("ENTRO");
        if (--enemiesToSpawn == 0)
            CancelInvoke("Countdown");
        else
        {
            Instantiate(enemy, transform.position, transform.rotation);
        }
    }
}
