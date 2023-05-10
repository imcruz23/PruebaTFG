using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour
{
    public float maxLife;
    public float life;
    public AudioManager AM;

    // Recibir daño
    // Amount positivo: Hacer daño
    // Amount negativo: Curar
    public void TakeDamage(float amount)
    {
        life -= amount;

        if(life <= 0)
        {
            life = 0;
            Die();
        }
        if (gameObject.layer == (int) Layers.Player)
        {
            AM.PlayPlayerDamageSound();
        }
    }

    private void Die()
    {
        if(this.gameObject.layer != (int) Layers.Player)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            //Application.LoadLevel(Application.loadedLevel);
            SceneManager.LoadScene(Application.loadedLevel);
        }
    }
}
