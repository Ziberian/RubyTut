using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffectDock : MonoBehaviour
{
    public ParticleSystem healEffect;

    public void HealParticle()
    {
        ParticleSystem healParticle =  Instantiate(healEffect, transform.position + Vector3.up, Quaternion.identity);
        healParticle.transform.parent = this.transform;
    }
}
