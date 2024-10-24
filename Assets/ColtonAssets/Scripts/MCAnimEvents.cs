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
}
