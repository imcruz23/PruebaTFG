using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamageComponent : MonoBehaviour
{
    private GameObject player;
    private bool onArea = false;
    private int counter;

    private void FixedUpdate()
    {
        if (onArea)
            DamagePlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (int) Layers.Player)
        {
            player = other.gameObject;
            onArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == (int)Layers.Player)
        {
            player = other.gameObject;
            onArea = false;
        }
    }

    private void DamagePlayer()
    {
        // Restamos vida al jugador de forma progresiva
        HealthComponent hc = player.GetComponent<HealthComponent>();
         hc.life -= 1;
    }
}
