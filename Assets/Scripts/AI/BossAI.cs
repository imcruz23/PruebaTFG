using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : AIComponent
{
    private Transform spawnPoint;
    public GameObject projectile;
    public float force;
    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
        spawnPoint = transform.GetChild(0);

    }
    public override void Attack()
    {
        //RaycastHit hit;

        // Vamos a intentar randomizar las balas para que algunas fallen
        /*float randomX = Random.Range(transform.forward.x - 0.2f, transform.forward.x + 0.2f);
        float randomY = Random.Range(transform.forward.y - 0.2f, transform.forward.y + 0.2f);
        Vector3 randomF = new Vector3(randomX, randomY, transform.forward.z);*/

        // Raycast de disparo
        //if (Physics.Raycast(spawnPoint.transform.position, randomF, out hit, base.attackRange))
        //{


        // Instanciar bala
        var bullet = Instantiate(
                projectile,
                new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z),
                Quaternion.identity
                );
        bullet.transform.forward = transform.forward;
        //var _ydistance = transform.position.y - player.transform.position.y;
        float _xdistance = Mathf.Abs(transform.position.x - player.transform.position.x);
        float _force = force * _xdistance;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * _force);
        
            /*
            print("PIUM");
            // Comprobamos que el hit haya sido al jugador
            if (hit.transform.gameObject.layer == (int)Layers.Player)
            {
                print("Le he dado al player");

                // Procedemos a bajarle la vida
                HealthComponent HC = player.GetComponent<HealthComponent>();
                if (HC)
                    HC.TakeDamage(damage);
            */
        //}
    }
}
