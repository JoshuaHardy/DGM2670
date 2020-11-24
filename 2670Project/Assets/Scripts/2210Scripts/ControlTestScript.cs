using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log(Input.GetAxis("Joy1 Axis 10"));
        Debug.Log(Input.GetAxis("Joy1 Axis 9"));
    }
}
