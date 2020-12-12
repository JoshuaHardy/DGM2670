using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ComeTogether : MonoBehaviour
{
    public Rigidbody rbBlue, rbRed;
    public float maxDist = 30f, knockBack = 30f;

    public GameObject blueCharacter;
    public GameObject redCharacter;
    private Vector3 comeMidBlue, comeMidRed;
    public CanvasGroup  GUINotice;

    private void Start()
    {
        /*Rigidbody rbBlue = LerpStart.GetComponent<Rigidbody>();
        Rigidbody rbRed = LerpEnd.GetComponent<Rigidbody>();*/
        //GUINotice = GetComponent <CanvasGroup>();
        GUINotice.alpha = 0.0f;
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
        Debug.Log("Knockback!");
        GUINotice.alpha = .75f;
        comeMidBlue = (redCharacter.transform.position - blueCharacter.transform.position).normalized;
        comeMidRed = (blueCharacter.transform.position - redCharacter.transform.position).normalized;
        rbBlue.AddForce (comeMidBlue * knockBack, ForceMode.VelocityChange);
        rbRed.AddForce (comeMidRed * knockBack, ForceMode.VelocityChange);
        StartCoroutine(Wait());
        GUINotice.alpha = 0.0f;
    }

    private IEnumerator Wait()
    {
        Debug.Log("Wait CoroutineStarted");
        new WaitForSeconds(1.5f);
        yield break;
    }
    
    //https://answers.unity.com/questions/1249109/moving-an-objectbullets-towards-a-vector-position.html
}
