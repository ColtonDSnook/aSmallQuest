using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    public GameManager gameManager;

    private bool isPlayingMenuMusic = false;
    private bool isPlayingGameMusic = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

        PlayMusic("MenuMusic");
        isPlayingMenuMusic = true;
    }

    private void Start()
    {

        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager.gameState == GameManager.GameState.Gameplay)
        {
            if (!isPlayingGameMusic)
            {
                SoundManager.Instance.musicSource.Stop();
                PlayMusic("GameMusic");
                isPlayingGameMusic = true;
                isPlayingMenuMusic = false;
            }

        }

        else if (gameManager.gameState != GameManager.GameState.Gameplay)
        {
            if (!isPlayingMenuMusic)
            {
                SoundManager.Instance.musicSource.Stop();
                PlayMusic("MenuMusic");
                isPlayingMenuMusic = true;
                isPlayingGameMusic = false;
            }
        }
    }

    public void PlayMusic(String clipName)
    {
        Sound s = Array.Find(musicSounds, x => x.clipName == clipName);

        if ( s == null)
        {
            Debug.Log("No Sound");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string clipName)
    {
        Sound s = Array.Find(sfxSounds, x => x.clipName == clipName);

        if (s == null)
        {
            Debug.Log("No Sound");
        }

        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
