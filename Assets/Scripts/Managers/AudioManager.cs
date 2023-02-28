using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Weapons
{
    Pistol = 0,
    AR = 1,
    SMG = 2,
    Shotgun = 3
}

public class AudioManager : MonoBehaviour
{
    private AudioSource sourceVFX;
    [HideInInspector] public AudioSource musicSource;
    public List<AudioClip> pistolClips;
    public List<AudioClip> playerClips;
    public List<AudioClip> arClips;

    // Start is called before the first frame update
    void Awake()
    {

        AudioSource[] audioSources = GetComponents<AudioSource>();
        musicSource = audioSources[0];
        sourceVFX = audioSources[1];

    }

    private void Init()
    {
        musicSource.Play();
    }

    private void Start()
    {
        Init();
    }

    // Sonidos del jugador
    public void PlayPlayerWalkingSound()
    {
        sourceVFX.clip = playerClips[0];
        if (!sourceVFX.isPlaying)
            sourceVFX.PlayOneShot(playerClips[0], 0.4f);
    }

    public void PlayPlayerLandingSound()
    {
        sourceVFX.PlayOneShot(playerClips[1], 0.6f);
    }

    public void PlayPlayerSlidingSound()
    {
        sourceVFX.PlayOneShot(playerClips[2], 0.4f);
    }

    // Controlador de disparo

    public void PlayShootingSound(int id)
    {
        if(id == (int)Weapons.Pistol)
            PlayPistolShootingSound();
        if (id == (int)Weapons.AR)
            PlayARShootingSound();
    }

    // Sonido de disparo
    void PlayPistolShootingSound()
    {
        sourceVFX.PlayOneShot(pistolClips[0], 0.6f);
    }

    //Sonidos de AR
    void PlayARShootingSound()
    {
        sourceVFX.PlayOneShot(arClips[0], 0.6f);
    }

}
