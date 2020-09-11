using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
   public IntData playerJumpCount, powerUpCount, normalJumpCount;
   public float waitTime = 2f;
   

   private void Start()
   {
       playerJumpCount.value = normalJumpCount.value;
   }


   private IEnumerator OnTriggerEnter(Collider other)
   {
       playerJumpCount.value = powerUpCount.value;
       Destroy(GetComponent<MeshRenderer>());
       GetComponent<MeshRenderer>().enabled = false;
       yield return new WaitForSeconds(waitTime);
       playerJumpCount.value = normalJumpCount.value;
       gameObject.SetActive(false);
   }

    
}