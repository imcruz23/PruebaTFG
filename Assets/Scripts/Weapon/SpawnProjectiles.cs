using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{
    public GameObject firePoint; // Desde donde va a salir los VFX
    public List<GameObject> vfx = new List<GameObject>(); // Los efectos
    private GameObject effectToSpawn; // El efecto que se va a ejecutar
    // Start is called before the first frame update
    void Start()
    {
        effectToSpawn = vfx[0];
    }
}
