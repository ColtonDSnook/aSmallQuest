using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMEvents : MonoBehaviour
{
    public ParticleSystem dmAttack;

    void DMAttackEvent()
    {
        dmAttack.Play();
    }
    void DMAttackS()
    {
        SoundManager.Instance.PlaySFX("DMAttack");
    }

    void DMHurtS()
    {
        SoundManager.Instance.PlaySFX("DMHurt");
    }

    void DMDeathS()
    {
        SoundManager.Instance.PlaySFX("DMDeath");
    }
}
