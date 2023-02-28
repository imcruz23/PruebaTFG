using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    private bool collided;
    private void OnCollisionEnter(Collision collision)
    {
        /*
        if(collision.gameObject.layer != (int)Layers.Player && collision.gameObject.layer != (int) Layers.Bullet && !collided)
        {
            collided = true;
            Destroy(gameObject);
        }
        */
    }
}
