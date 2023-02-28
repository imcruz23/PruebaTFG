using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float maxLife;
    public float life;

    // Recibir da�o
    // Amount positivo: Hacer da�o
    // Amount negativo: Curar
    public void TakeDamage(float amount)
    {
        life -= amount;

        if(life <= 0)
        {
            life = 0;
            Die();
        }
    }

    private void Die()
    {
        if(this.gameObject.layer != (int) Layers.Player)
        {
            Destroy(gameObject);
            return;
        }
        print("Me he morido");
    }
}
