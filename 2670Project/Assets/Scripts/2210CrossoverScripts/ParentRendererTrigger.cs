using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentRendererTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Color originalColor, newColor;

    public void Start()
    {
        originalColor = Color.clear;
        GetComponentInParent <Renderer>().material.color = originalColor;
        newColor = Color.green;
    }


    public void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponentInParent <Renderer>().material.color = newColor;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponentInParent <Renderer>().material.color = originalColor;
        }
    }

}
