using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    public GameObject AOE;

    public float timer;

    private void Awake()
    {
        Invoke(nameof(DestroyBullet), timer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (int) Layers.Floor)
        {
            Instantiate(AOE, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            DestroyBullet();
        }
           
        else if (other.gameObject.layer == (int)Layers.Player)
        {
            var health = other.gameObject.GetComponent<HealthComponent>() as HealthComponent;
            health.life -= 10;
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
