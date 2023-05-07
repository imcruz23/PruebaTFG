using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableComponent : MonoBehaviour
{
    // Eliminar un objeto
    public float timer;

    private void Awake()
    {
        Invoke(nameof(DestroyObject), timer);
    }

    // Elimina el objeto
    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
