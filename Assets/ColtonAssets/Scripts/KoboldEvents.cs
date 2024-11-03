using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoboldEvents : MonoBehaviour
{
    public ParticleSystem slashKobold;

    void KobAttackEvent()
    {
        slashKobold.Play();
    }
    void KoboldAttackS()
    {
        SoundManager.Instance.PlaySFX("KoboldAttack");
    }

    void KoboldHurtS()
    {
        SoundManager.Instance.PlaySFX("KoboldHurt");
    }

    void KoboldDeathS()
    {
        SoundManager.Instance.PlaySFX("KoboldDeath");
    }
}
