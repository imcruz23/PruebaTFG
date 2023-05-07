using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToBoss : MonoBehaviour
{

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.layer == (int)Layers.Player)
        {
            GameObject t = other.gameObject;
            // Conseguimos el PlayerComponent
            t.GetComponent<CharacterController>().Move(new Vector3(-800f, -67.1f, 10f));

            //other.gameObject.transform.position = new Vector3(-800f, -67.1f, 10f);
        }
    }
}
