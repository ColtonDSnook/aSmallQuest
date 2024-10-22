using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MonoBehaviour
{
    public ParticleSystem slimeAttack;

    void SlimeAttackEvent()
    {
        slimeAttack.Play();
    }
}
