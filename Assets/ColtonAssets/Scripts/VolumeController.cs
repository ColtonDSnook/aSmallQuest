using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour, IPointerClickHandler
{
    public Slider _sfxSlider;

    public void SFXVolume()
    {
        SoundManager.Instance.SFXVolume(_sfxSlider.value);
    }

    public void OnPointerClick(PointerEventData data)
    {
        SoundManager.Instance.PlaySFX("MCHurt");
    }
}
