using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Interactable : MonoBehaviour
{
    protected bool inRange;
    protected virtual void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.layer == (int) Layers.Player)
            inRange = true;
    }

    protected virtual void OnTriggerExit (Collider other)
    {
        if (other.gameObject.layer == (int)Layers.Player)
            inRange = false;
    }
}
