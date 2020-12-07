using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RendererTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Color originalColor, newColor;
    public float newAlpha = 100f, originalAlpha = 0.0f;
    private Renderer rend;

    public void Start()
    {
        //originalColor = Color = 0f;
        //GetComponent <Renderer>().material.color = originalColor;
        rend = GetComponent<Renderer>();
        rend.enabled = false;
    }


    public void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            rend.enabled = true;
            /*GetComponent<Renderer>().material.enable;
            GetComponent <Renderer>().material.color = newColor;*/
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            rend.enabled = false;
           //GetComponent <Renderer>().material.color = originalColor;
        }
    }
}