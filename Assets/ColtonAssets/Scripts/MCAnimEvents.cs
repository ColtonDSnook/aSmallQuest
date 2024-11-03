using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCAnimEvents : MonoBehaviour
{
    public ParticleSystem spinSlash;
    public ParticleSystem slash;
    public ParticleSystem stabAttack;

    void MCAttackEvent()
    {
        slash.Play();
    }
    void MCSpinEvent()
    {
        spinSlash.Play();
    }
    void MCStabEvent()
    {
        stabAttack.Play();
    }
    void MCAttackS()
    {
        SoundManager.Instance.PlaySFX("MCAttack");
    }

    void MCHurtS()
    {
        SoundManager.Instance.PlaySFX("MCHurt");
    }

    void MCDeathS()
    {
        SoundManager.Instance.PlaySFX("MCDeath");
    }
}