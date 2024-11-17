using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinEvents : MonoBehaviour
{
    public ParticleSystem goblinAttack;

    void GobAttackEvent()
    {
        goblinAttack.Play();
    }
    void GoblinAttackS()
    {
        SoundManager.Instance.PlaySFX("GoblinAttack");
    }

    void GoblinHurtS()
    {
        SoundManager.Instance.PlaySFX("GoblinHurt");
    }

    void GoblinDeathS()
    {
        SoundManager.Instance.PlaySFX("GoblinDeath");
    }
}

