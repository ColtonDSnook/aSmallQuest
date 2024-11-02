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
    }

    private void Start()
    {
        PlayMusic("MenuMusic");
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
}
