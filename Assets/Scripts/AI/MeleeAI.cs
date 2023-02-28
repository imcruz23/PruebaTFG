using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : AIComponent
{
    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
    }


    public override void Attack()
    {
        // Le vamos a restar vida al jugador
        HealthComponent HC = player.GetComponent<HealthComponent>();
        if (HC)
            HC.TakeDamage(damage);
    }
}
