using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttack : MonoBehaviour
{
    public ParticleSystem goblinAttack;

    void GobAttackEvent()
    {
        goblinAttack.Play();
    }
}

