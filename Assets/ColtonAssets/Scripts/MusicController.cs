using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MusicController: MonoBehaviour
{
    public Slider _musicSlider;

    public void Awake()
    {
        _musicSlider.value = PlayerPrefs.GetFloat("music");
    }

    public void MusicVolume()
    {
        SoundManager.Instance.MusicVolume(_musicSlider.value);
        PlayerPrefs.SetFloat("music", _musicSlider.value);
    }
}
