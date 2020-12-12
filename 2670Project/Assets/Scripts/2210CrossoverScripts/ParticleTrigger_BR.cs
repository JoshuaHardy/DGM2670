using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger_BR : MonoBehaviour
{
    public ParticleSystem psBlue,psRed;
   void Start()
    {
        //particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "BluePlayer"))
        {
            psBlue.Play();
            Debug.Log("EnterBlue");
        }
        
        if ((other.tag == "RedPlayer"))
        {
            psRed.Play();
            Debug.Log("EnterRed");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.tag == "BluePlayer"))
        {
            psBlue.Stop();
            Debug.Log("ExitBlue");
        }
        
        if ((other.tag == "RedPlayer"))
        {
            psRed.Stop();
            Debug.Log("ExitRed");
        }

        
    }

    void Update()
    {
        
    }
}
