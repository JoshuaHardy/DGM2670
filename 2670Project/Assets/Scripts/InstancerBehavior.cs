using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InstancerBehavior : MonoBehaviour
{
    public GameObject prefab;
    public Vector3Data rotationDirection;
    /*
 Make a method to call the instance;
 */
    
    public void Instance()
    {
      
        var location = transform.position;
        var rotationDirection = new Vector3(0f,45f,0 );
        Instantiate(prefab, location, Quaternion.Euler(rotationDirection));
        
    }
}


