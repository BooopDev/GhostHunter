using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSetup : MonoBehaviour
{
    private ParticleSystem red;
    private ParticleSystem blue;
    private ParticleSystem decal;

    void Start()
    {
        decal = GameObject.FindGameObjectWithTag("brown").GetComponent<ParticleSystem>();
        red = GameObject.FindGameObjectWithTag("red").GetComponent<ParticleSystem>();
        blue = GameObject.FindGameObjectWithTag("blue").GetComponent<ParticleSystem>();
        red.externalForces.AddInfluence(gameObject.GetComponent<ParticleSystemForceField>());
        blue.externalForces.AddInfluence(gameObject.GetComponent<ParticleSystemForceField>());
        decal.externalForces.AddInfluence(gameObject.GetComponent<ParticleSystemForceField>());
        red.trigger.SetCollider(0, gameObject.GetComponent<Collider>());
        blue.trigger.SetCollider(0, gameObject.GetComponent<Collider>());
        decal.trigger.SetCollider(0, gameObject.GetComponent<Collider>());
    }
}
