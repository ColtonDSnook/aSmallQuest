using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMAttack : MonoBehaviour
{
    public ParticleSystem dmAttack;

    void DMAttackEvent()
    {
        dmAttack.Play();
    }
}
