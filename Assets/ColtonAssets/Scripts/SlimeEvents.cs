using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEvents : MonoBehaviour
{
    public ParticleSystem slimeAttack;

    void SlimeAttackEvent()
    {
        slimeAttack.Play();
    }
    void SlimeAttackS()
    {
        SoundManager.Instance.PlaySFX("SlimeAttack");
    }

    void SlimeHurtS()
    {
        SoundManager.Instance.PlaySFX("SlimeHurt");
    }

    void SlimeDeathS()
    {
        SoundManager.Instance.PlaySFX("SlimeDeath");
    }
}
