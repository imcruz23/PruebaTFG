using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawnerComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public SpawnerManager spawnerToTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == (int) Layers.Player)
            spawnerToTrigger.trigger = true;
    }
}