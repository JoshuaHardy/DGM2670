using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public bool moduleEnabled;

    void Start()
    {
        //particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        particleSystem.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        particleSystem.Stop();
    }

    void Update()
    {
        
    }
}
