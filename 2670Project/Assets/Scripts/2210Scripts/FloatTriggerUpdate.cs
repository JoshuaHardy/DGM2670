using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
 
[System.Serializable]
public class FloatTriggerUpdate : MonoBehaviour
{
    public FloatData adjustedfloatData;
    //public FloatData changeInHealth;
    public float changeInHealth = 0.25f;
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEnterCalled: ");
        adjustedfloatData.value += changeInHealth;
        
    }

    private IEnumerator UpdateFloat ()
    {
        Debug.Log("CoRoutineExecuted: ");
        
        yield return null;
    }   
}