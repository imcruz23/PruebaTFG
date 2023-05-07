using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlaneComponent : MonoBehaviour
{

    // Variables
    public LevelManager LM;

    private void Awake()
    {
        if (!LM)
            LM = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Devolver al jugador a la posición inicial
        if (other.gameObject.layer == (int) Layers.Player)
        {
            Application.LoadLevel(Application.loadedLevel);
            /*GameObject t = other.gameObject;
            t.GetComponentInChildren<CapsuleCollider>().enabled = false;
            t.GetComponent<CharacterController>().Move(new Vector3(-20f, 1f, 100f));*/
        }
    }
}
