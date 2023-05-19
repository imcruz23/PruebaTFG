using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicalNote : MonoBehaviour
{
    public Vector3 spawnPos; // Posicion Inicial
    //public Vector3 removePos; // Posicion final
    [SerializeField] private SongManager SM;
    [SerializeField] private GameObject activatorPoint;
    public InputManager IM;
    public LevelManager LM;
    public GameObject inTimeText;
    public HealthComponent pHealth;
    public static bool hitOnTime = false;

    private void Awake()
    {
        if (!SM)
            SM = GameObject.Find("Audio Manager").GetComponent<SongManager>();
        //if (!activatorPoint)
            //activatorPoint = GameObject.Find("GoTo");

    }

    // Start is called before the first frame update
    void Start()
    {

        spawnPos = activatorPoint.transform.position;
        //spawnPos = transform.position;
        /*
        removePos = new Vector3(
            activatorPoint.transform.position.x,
            activatorPoint.transform.position.y + 100f,
            activatorPoint.transform.position.z
         );
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.activeSelf)
            return;
        /*
        if (transform.position != removePos)
        {
            
            transform.position = Vector3.Lerp(
            spawnPos,
            removePos,
            (0.4f * (SM.songPosition / SM.beatsShownInAdvance))
            );
            var _ypos = (SM.songPosition / SM.beatsShownInAdvance) * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y + _ypos, transform.position.z);
        }

        else
            transform.position = spawnPos;
        */

        Invoke(nameof(DestroyNote), 0.5f);

        if (IM.fire.WasPressedThisFrame() && gameObject.activeSelf)
        {
            //Debug.Log("IN TIME");
            LM.AddScore(5);
            inTimeText.SetActive(true);
            DestroyNote();
            Invoke(nameof(TimeTextOff), 0.5f);
            MusicalNote.hitOnTime = true;
        }
        else
        {
            MusicalNote.hitOnTime = false;
        }
        /*
        else
        {
            //Invoke(nameof(DestroyNote), 0.5f);
            //DestroyNote();
        }
        */

    }

    private void DestroyNote()
    {
        gameObject.SetActive(false);
    }

    private void TimeTextOff()
    {
        inTimeText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Colision");
    }

    private void OnDestroy()
    {
        //Debug.Log("Elimino nota");
    }
}
