using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    public ParticleSystem particleSystem;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            particleSystem.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            particleSystem.Stop();
        }
    }
}
