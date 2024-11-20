using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCAnimEvents : MonoBehaviour
{
    public ParticleSystem spinSlash;
    public ParticleSystem slash;
    public ParticleSystem stabAttack;
    public ParticleSystem LeftFoot;
    public ParticleSystem RightFoot;

    void FootStepEvent(int whichFoot)
    {
        if (whichFoot == 0)
        {
            LeftFoot.Play();
        }
        else
        {
            RightFoot.Play();
        }
    }
    void MCAttackEvent()
    {
        slash.Play();
    }
    void MCSpinEvent()
    {
        spinSlash.Play();
        SoundManager.Instance.PlaySFX("Spin");
    }
    void MCStabEvent()
    {
        stabAttack.Play();
        SoundManager.Instance.PlaySFX("Stab");
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