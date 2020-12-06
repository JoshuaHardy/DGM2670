using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatBoolUpdate : MonoBehaviour
{
    public FloatData adjustedFloatData, minValue, maxValue, positiveCoefficient, negativeCoefficient;
    public BoolData triggerBool;

    void OnAwake()
    {
        /*if (negativeCoefficient.value > 0)
        {
            negativeCoefficient.value *= -1;
        }*/
        
    }
    void Update ()
    {
        if ((triggerBool) && (adjustedFloatData.value < maxValue.value))
        {
            adjustedFloatData.value += positiveCoefficient.value * Time.deltaTime;
        }
        else
        {
            if (adjustedFloatData.value > minValue.value)
            {
                adjustedFloatData.value -= negativeCoefficient.value * Time.deltaTime;
            }   
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