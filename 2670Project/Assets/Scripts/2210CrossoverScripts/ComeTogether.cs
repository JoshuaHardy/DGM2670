using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[RequireComponent(typeof(Rigidbody))]
public class ComeTogether : MonoBehaviour
{
    public Rigidbody rbBlue, rbRed;
    public float maxDist = 30f, knockBack = 30f;

    public GameObject blueCharacter;
    public GameObject redCharacter;
    private Vector3 comeMidBlue, comeMidRed;

    private void Start()
    {
        /*Rigidbody rbBlue = LerpStart.GetComponent<Rigidbody>();
        Rigidbody rbRed = LerpEnd.GetComponent<Rigidbody>();*/
    }

    void Update()
    {
        float dist = Vector3.Distance(blueCharacter.transform.position, redCharacter.transform.position);
        //print("Distance to other: " + dist);
        if (dist >= maxDist)
        {
            closerNow();
        }
    }

    public void closerNow()
    {
        print("Knockback!");
        comeMidBlue = (redCharacter.transform.position - blueCharacter.transform.position).normalized;
        comeMidRed = (blueCharacter.transform.position - redCharacter.transform.position).normalized;
        rbBlue.AddForce (comeMidBlue * knockBack, ForceMode.VelocityChange);
        rbRed.AddForce (comeMidRed * knockBack, ForceMode.VelocityChange);
        
    }
    
    //https://answers.unity.com/questions/1249109/moving-an-objectbullets-towards-a-vector-position.html
    
    
    
}
