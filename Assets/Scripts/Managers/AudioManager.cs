using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Weapons
{
    Pistol = 0,
    AR = 1,
    Shotgun = 2
}

public class AudioManager : MonoBehaviour
{
    private AudioSource sourceVFX;
    [HideInInspector] public AudioSource musicSource;
    public List<AudioClip> pistolClips;
    public List<AudioClip> playerClips;
    public List<AudioClip> arClips;
    public List<AudioClip> sgClips;
    public List<AudioClip> enemyClips;

    // Start is called before the first frame update
    void Awake()
    {

        AudioSource[] audioSources = GetComponents<AudioSource>();
        musicSource = audioSources[0];
        sourceVFX = audioSources[1];
        AudioListener.volume = PlayerPrefs.GetFloat("audioVolume");
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
        if (musicSource)
        {
            sourceVFX.clip = playerClips[0];
            if (!sourceVFX.isPlaying)
                sourceVFX.PlayOneShot(playerClips[0], 0.4f);
        }
        
    }

    public void PlayPlayerLandingSound()
    {
        if (musicSource)
            sourceVFX.PlayOneShot(playerClips[1], 0.6f);
    }

    public void PlayPlayerSlidingSound()
    {
        if (musicSource)
        sourceVFX.PlayOneShot(playerClips[2], 0.4f);
    }

    public void PlayPlayerDashSound()
    {
        if (musicSource)
        {
            sourceVFX.clip = playerClips[3];
            if (!sourceVFX.isPlaying)
                sourceVFX.PlayOneShot(playerClips[3], 0.4f);
        }
        
    }
    public void PlayPlayerDamageSound()
    {
        if (musicSource)
        {
            sourceVFX.clip = playerClips[4];
            if (!sourceVFX.isPlaying)
                sourceVFX.PlayOneShot(playerClips[4], 1.2f);
        }
       
    }

    public void PlayPlayerHealSound()
    {
        if (musicSource)
            sourceVFX.PlayOneShot(playerClips[5], 0.4f);
    }

    // Controlador de disparo

    public void PlayShootingSound(int id)
    {
        if (musicSource)
        {
            if (id == (int)Weapons.Pistol)
                PlayPistolShootingSound();
            if (id == (int)Weapons.AR)
                PlayARShootingSound();
            if (id == (int)Weapons.Shotgun)
                PlaySGShootingSound();
        }
    }

    public void PlayReloadingSound(int id)
    {
        if (musicSource)
        {
            if (id == (int)Weapons.Pistol || id == (int)Weapons.Shotgun)
                PlayPistolReloadingSound();
            if (id == (int)Weapons.AR)
                PlayARReloadingSound();
        }
    }
    // Sonido de disparo
    void PlayPistolShootingSound()
    {
        if (musicSource)
            sourceVFX.PlayOneShot(pistolClips[0], 0.6f);
    }

    // Sonidos de AR
    void PlayARShootingSound()
    {
        if (musicSource)
            sourceVFX.PlayOneShot(arClips[0], 0.6f);
    }

    void PlaySGShootingSound()
    {
        if (musicSource)
            sourceVFX.PlayOneShot(sgClips[0], 0.6f);
    }

    // Sonido de recarga
    void PlayPistolReloadingSound()
    {
        if (musicSource)
            sourceVFX.PlayOneShot(pistolClips[1], 0.6f);
    }

    void PlayARReloadingSound() {}

    // Sonido de enemigos
    public void PlayEnemyShootingSound()
    {
        if (musicSource)
            sourceVFX.PlayOneShot(enemyClips[0], 0.6f);
    }

    public void PlayEnemyDamageSound()
    {
        if (musicSource)
            sourceVFX.PlayOneShot(enemyClips[1], 0.6f);
    }

}
