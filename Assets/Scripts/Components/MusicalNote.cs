using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicalNote : MonoBehaviour
{
    public Vector3 spawnPos; // Posicion Inicial
    public Vector3 removePos; // Posicion final
    [SerializeField] private SongManager SM;
    [SerializeField] private GameObject activatorPoint;

    private void Awake()
    {
        if (!SM)
            SM = GameObject.Find("Audio Manager").GetComponent<SongManager>();
        if (!activatorPoint)
            activatorPoint = GameObject.Find("GoTo");

    }
    // Start is called before the first frame update
    void Start()
    {
        /*
        spawnPos = new Vector3(
            activatorPoint.transform.position.x,
            activatorPoint.transform.position.y - 300f,
            activatorPoint.transform.position.z
         );
        removePos = new Vector3(
            activatorPoint.transform.position.x, 
            activatorPoint.transform.position.y + 100f, 
            activatorPoint.transform.position.z
         );
        */
        spawnPos = activatorPoint.transform.position;
        removePos = new Vector3(
            activatorPoint.transform.position.x,
            activatorPoint.transform.position.y + 100f,
            activatorPoint.transform.position.z
         );
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(
            spawnPos,
            removePos,
            ( 0.4f * (SM.songPosition / SM.beatsShownInAdvance) )
            );

        /*if (transform.position.y == removePos.y)
        {
            Destroy(this.gameObject);
        }*/
        Invoke(nameof(DestroyNote), 2.0f);
    }

    private void DestroyNote()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       //print("Colision");
    }

    private void OnDestroy()
    {
        //Debug.Log("Elimino nota");
    }
}
