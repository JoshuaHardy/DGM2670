using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpVector3 : MonoBehaviour
{
  //public Vector3 LerpStart, LerpEnd;
  [Range(0.000f, 1.0f)] 
  public float LerpPercent = 0.5f;
    
    public GameObject LerpStart, LerpEnd;
    
    void Update()
    {
        transform.position = Vector3.Lerp(LerpStart.transform.position, LerpEnd.transform.position, LerpPercent);
        
        float dist = Vector3.Distance(LerpStart.transform.position, LerpEnd.transform.position);
      
    }
}