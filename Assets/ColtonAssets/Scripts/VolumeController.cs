using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;

    public void MusicVolume()
    {
        SoundManager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        SoundManager.Instance.SFXVolume(_sfxSlider.value);
    }
}
