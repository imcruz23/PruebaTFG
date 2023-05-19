using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAI : AIComponent
{
    private Transform spawnPoint;
    public GameObject projectile;
 
    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
        spawnPoint = transform.GetChild(0);
    }
    public override void Attack()
    {
        RaycastHit hit;
        AM.PlayEnemyShootingSound();
        // Vamos a intentar randomizar las balas para que algunas fallen
        float randomX = Random.Range(transform.forward.x - 0.1f, transform.forward.x + 0.1f);
        float randomY = Random.Range(transform.forward.y - 0.1f, transform.forward.y + 0.1f);
        Vector3 randomF = new Vector3(randomX, randomY, transform.forward.z);

        InstantiateProjectile();

        // Raycast de disparo
        if (Physics.Raycast(spawnPoint.transform.position, randomF, out hit, base.attackRange))
        {
            print("PIUM");
            // Comprobamos que el hit haya sido al jugador
            if (hit.transform.gameObject.layer == (int)Layers.Player)
            {
                print("Le he dado al player");

                // Procedemos a bajarle la vida
                HealthComponent HC = player.GetComponent<HealthComponent>();
                if (HC)
                    HC.TakeDamage(damage);
            }
            else
                print("FALLE");
        }

        DisableProjectile();
    }

    private void InstantiateProjectile()
    {
        // Instanciar MuzzleFlash
        projectile.transform.position = spawnPoint.position;
        projectile.transform.forward = transform.forward;
        projectile.gameObject.SetActive(true);
        Invoke(nameof(DisableProjectile), 0.03f);
    }

    private void DisableProjectile()
    {
        if (projectile.activeSelf)
            projectile.gameObject.SetActive(false);
    }
}
