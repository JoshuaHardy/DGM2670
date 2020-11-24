using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpRotation : MonoBehaviour
{
    private Vector3 relativePosition;
    //private Quaternion targetRotation;
    public GameObject LerpStart, LerpEnd;
    [Range(0.000f, 1.0f)]
    public float lerpPercent = 0.5f;
    public float speed = 0.1f;  
    
    void Update()
    {
       
        /*{
        relativePosition = target.position - transform.position;
        target.rotation = Quaternion.LookRotation(relativePosition);
        }*/
    
        {
            transform.rotation = Quaternion.Lerp(Quaternion.LookRotation(LerpStart.transform.position), Quaternion.LookRotation(LerpEnd.transform.position), lerpPercent);   
        }
        
    }
    
    
    //public Vector3 LerpStart, LerpEnd;
    /*
    public float LerpPercent = 0.5f;
    
    public Transform LerpStart, LerpEnd;
    void Update()
    {
        transform.position = Vector3.Lerp(LerpStart.position, LerpEnd.position, LerpPercent);
    }*/
}
