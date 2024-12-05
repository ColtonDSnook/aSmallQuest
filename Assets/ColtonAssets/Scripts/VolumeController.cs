using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour, IPointerClickHandler
{
    public Slider _sfxSlider;

    public void Awake()
    {
        _sfxSlider.value = PlayerPrefs.GetFloat("volume");
    }

    public void SFXVolume()
    {
        SoundManager.Instance.SFXVolume(_sfxSlider.value);
        PlayerPrefs.SetFloat("volume", _sfxSlider.value);
    }

    public void OnPointerClick(PointerEventData data)
    {
        SoundManager.Instance.PlaySFX("MCHurt");
    }
}
