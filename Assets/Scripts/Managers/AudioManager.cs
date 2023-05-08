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
    public List<AudioClip> enemyClips;

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

    public void PlayPlayerDashSound()
    {
        sourceVFX.clip = playerClips[3];
        if (!sourceVFX.isPlaying)
            sourceVFX.Play();
    }

    // Controlador de disparo

    public void PlayShootingSound(int id)
    {
        if(id == (int)Weapons.Pistol)
            PlayPistolShootingSound();
        if (id == (int)Weapons.AR)
            PlayARShootingSound();
    }

    public void PlayReloadingSound(int id)
    {
        if (id == (int)Weapons.Pistol)
            PlayPistolReloadingSound();
        if (id == (int)Weapons.AR)
            PlayARReloadingSound();
    }
    // Sonido de disparo
    void PlayPistolShootingSound()
    {
        sourceVFX.PlayOneShot(pistolClips[0], 0.6f);
    }

    // Sonidos de AR
    void PlayARShootingSound()
    {
        sourceVFX.PlayOneShot(arClips[0], 0.6f);
    }

    // Sonido de recarga
    void PlayPistolReloadingSound()
    {
        sourceVFX.PlayOneShot(pistolClips[1], 0.6f);
    }

    void PlayARReloadingSound() {}

    // Sonido de enemigos
    public void PlayEnemyShootingSound()
    {
        sourceVFX.PlayOneShot(enemyClips[0], 0.6f);
    }

}
