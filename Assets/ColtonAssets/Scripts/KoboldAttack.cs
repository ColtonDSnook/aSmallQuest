using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoboldAttack : MonoBehaviour
{
    public ParticleSystem slashKobold;

    void KobAttackEvent()
    {
        slashKobold.Play();
    }
}
