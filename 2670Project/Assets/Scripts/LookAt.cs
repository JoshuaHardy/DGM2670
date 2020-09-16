using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
   public Transform lookObj;

   private void Update()
   {
   
         (transform1 = transform).LookAt(lookObj);
         var transformRotation = transform1.rotation;
         transform.rotation = Quaternion.Euler(transformRotation);
   }
}
