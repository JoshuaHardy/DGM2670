using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
 
[System.Serializable]
public class floatEvent : UnityEvent<float> { };
 
public class FloatUpdate : MonoBehaviour
{
    public FloatData adjustedfloatData;
    public floatEvent floatEvent ;
    public float coefficient = 0.1f;
    private bool isActive;
    private int numberOfPlayers = 0;
    
    void Update ()
    {
        if (numberOfPlayers >= 1)
        {
            adjustedfloatData.value += coefficient * numberOfPlayers * Time.deltaTime;
        }
    }

    public void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            numberOfPlayers++;
        }
    }
    
    public void OnTriggerExit (Collider other)
    {
        if (other.tag == "Player")
        {
            numberOfPlayers--;
        }
    }
}

/* Referenbce from Unity API
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
 
[System.Serializable]
public class HealthEvent : UnityEvent<float> { };
 
public class HealthUpdate : MonoBehaviour
{
    public float health = 100.0f;
    public HealthEvent healthEvent ;
    private const float coef = 0.2f;
      
    void Update ()
    {
        health -= coef * Time.deltaTime;
        if( healthEvent != null )
            healthEvent.Invoke( health ) ;
    }        
}*/