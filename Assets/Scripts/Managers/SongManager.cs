using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    private AudioManager AM;

    // La cancion actual
    private AudioSource music;

    // El BPM de la cancion
    public float bpm;

    // Los segundos por cada beat de la cancion
    public float secPerBeat;

    // Posicion de la cancion en segundos
    public float songPosition;

    // Posicion de la cancion en beats
    public float songPositionInBeats;

    // El tiempo que ha pasado desde que la cancion ha empezado (en segundos)
    public float dspSongTime;

    // El ligero retraso que puede tener la cancion
    public float firstBeatOffset;

    // Las notas de la cancion en posiciones
    float[] notes = { 1f, 2f, 2.5f, 3f, 3.5f, 4.5f, 5f, 6f, 6.5f };

    // El siguiente indice de la nota que debe aparecer
    int nextIndex = 0;

    // La cantidad de notas que quiero mostrar antes
    public int beatsShownInAdvance;

    // El tiempo que quiero para poder hacer la accion
    public float timeToAction;

    // La nota
    public GameObject notePrefab;

    // Para evitar que se haga la comprobacion cada frame (no funciona)
    bool onTime;

    [SerializeField] private GameObject activatorPoint;


    [SerializeField] private UIManager UIM;

    private void Awake()
    {
        TryGetComponent(out AM);
        if (!UIM)
            UIM = GameObject.Find("UI Manager").GetComponent<UIManager>();
        if (!activatorPoint)
            activatorPoint = GameObject.Find("GoTo");
    }
    // Start is called before the first frame update
    void Start()
    {
        music = AM.musicSource;

        // Calculo del numero de segundos por cada beat de la cancion
        secPerBeat = 60f / bpm;

        // Guardar el tiempo en el que empieza la cancion
        //dspSongTime = (float)AudioSettings.dspTime + secPerBeat;
        dspSongTime = music.time + secPerBeat;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        songPosition += Time.deltaTime;

        if(songPosition >= dspSongTime)
        {
            Debug.Log("Beat!");
            SpawnNote();
            dspSongTime += secPerBeat;
        }


        // Saber cuanto tiempo ha pasado desde que ha comenzado la cancion
        /*
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);
        songPositionInBeats = songPosition / secPerBeat;
        print(songPositionInBeats + " - " + dspSongTime);
        if (songPositionInBeats >= dspSongTime)
        {
            Debug.Log("Beat!");
            dspSongTime += secPerBeat;
        }
        */


        // Saber cuantos beats han pasado desde que la cancion ha comenzado
        //songPositionInBeats = (int) songPosition / secPerBeat;

        //InvokeRepeating(nameof(CalculateSPIB), 0.0f, 2.0f);

        /*
        songPositionInBeats = Mathf.Floor(songPositionInBeats);
        print(songPositionInBeats);
        */

        //InvokeRepeating("Countdown", 1.0f, 1.0f);
        //notes[nextIndex] = songPositionInBeats;
        //beatOfThisNote = notes[nextIndex];

        // Comprobamos que puedan seguir saliendo notas
        /*
        if (nextIndex < notes.Length && notes[nextIndex] && nextIndex < songPositionInBeats + beatsShownInAdvance)
        {
           Instantiate(note, GameObject.Find("Rythm").transform);
           nextIndex++;
        }
        */
       
    }

    void SpawnNote()
    {
       print("Instancio nota");
       Instantiate(notePrefab, GameObject.Find("Rythm").transform);
    }


    /*
    public bool PressedOnTime()
    {
        // Redondeamos los decimales del beat
        //float rounded = Mathf.Round(songPositionInBeats);
        float action = songPositionInBeats % timeToAction;
        if (action == 0)
        {
            UIM.DrawInTime();
            onTime = true;
            return true;
        }  
        return false;
    }
    */

    /*
    void Countdown()
    {
        //print("Invoco a countdown");
        if(--timer == 0)
        {
            onTime = false;
        }
    }
    */
}
