using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour, IEndDragHandler
{
    public Slider _sfxSlider;

    public void SFXVolume()
    {
        SoundManager.Instance.SFXVolume(_sfxSlider.value);
    }
    public void OnEndDrag(PointerEventData data)
    {
        SoundManager.Instance.PlaySFX("MCHurt");
    }
}
