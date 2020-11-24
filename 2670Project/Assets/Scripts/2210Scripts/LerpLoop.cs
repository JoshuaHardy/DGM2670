using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LerpLoop : MonoBehaviour
{
   //public Vector3 LerpStart, LerpEnd;
    [Range(0.000f, 1.000f)] 
    public float LerpPercent = 0.0f;
    
    public Transform LerpStart, LerpEnd;
    public WaitForSeconds HoldStartTime;
    public WaitForSeconds HoldEndTime;
    public float periodSeconds;
    public bool holdStartBool = false, holdEndBool = false;
    
    //I guess this is something probably easier done with an animation controller.
    //I'm going for a sine wave sort of movement with optional plateaued peaks for holding position.

    void Update()
    {
        transform.position = Vector3.Lerp(LerpStart.position, LerpEnd.position, LerpPercent);
        
        if (transform.position == LerpStart.position)
        {
            holdStartBool = true;
            StartCoroutine(HoldStart());
           // StartCoroutine(goThere());
        }
        if (transform.position == LerpEnd.position)
        {
            
            StartCoroutine(HoldEnd());
           // StartCoroutine(comeBack());
        }
    }

    IEnumerator HoldStart()
    {
        holdStartBool = true;
        yield return HoldStartTime;
        holdStartBool = false;
    }
    
    IEnumerator HoldEnd()
    {
        holdEndBool = true;
        yield return HoldEndTime;
        holdEndBool = false;
    }

    /*IEnumerator goThere()
    {
        .5 * periodSeconds 

    }

    IEnumerator comeBack()
    {
        .5 * periodSeconds

    }*/
}
