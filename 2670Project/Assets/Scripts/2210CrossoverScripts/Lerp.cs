using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Lerp : MonoBehaviour
{
    public Rigidbody rbBlue, rbRed;
   //public Vector3 LerpStart, LerpEnd;
   public float maxDist = 30f, knockBack = 50f;
   [Range(0.000f, 1.0f)] public float LerpPercent = 0.5f;
    
    public GameObject LerpStart, LerpEnd;
    private Vector3 comeMidBlue, comeMidRed;

    private void Start()
    {
        /*Rigidbody rbBlue = LerpStart.GetComponent<Rigidbody>();
        Rigidbody rbRed = LerpEnd.GetComponent<Rigidbody>();*/
    }

    void Update()
    {
        transform.position = Vector3.Lerp(LerpStart.transform.position, LerpEnd.transform.position, LerpPercent);
        
        float dist = Vector3.Distance(LerpStart.transform.position, LerpEnd.transform.position);
        //print("Distance to other: " + dist);
        if (dist >= maxDist)
        {
            closerNow();
        }
    }

    public void closerNow()
    {
        print("Knockback!");
        comeMidBlue = (LerpEnd.transform.position - LerpStart.transform.position).normalized;
        comeMidRed = (LerpStart.transform.position - LerpEnd.transform.position).normalized;
        rbBlue.AddForce (comeMidBlue * knockBack, ForceMode.VelocityChange);
        rbRed.AddForce (comeMidRed * knockBack, ForceMode.VelocityChange);
        
    }
    
    //https://answers.unity.com/questions/1249109/moving-an-objectbullets-towards-a-vector-position.html
    
    
    
}
