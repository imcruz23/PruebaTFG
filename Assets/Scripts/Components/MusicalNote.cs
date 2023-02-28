using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicalNote : MonoBehaviour
{

    public Vector3 spawnPos;
    public Vector3 removePos;
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
        spawnPos = new Vector3(
            activatorPoint.transform.position.x,
            activatorPoint.transform.position.y - 300f,
            activatorPoint.transform.position.z
         );
        removePos = activatorPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Vector3.Lerp(
            spawnPos,
            removePos,
            ( 0.4f * (SM.songPosition / SM.beatsShownInAdvance) )
            );

        //transform.position = Vector3.Lerp(transform.position.x, SM.beatsShownInAdvance - (transform.position.y + SM.songPositionInBeats) / SM.beatsShownInAdvance, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            print("In time!");
    }
}
