using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ForceVolume : MonoBehaviour
{
    public float moveForce = 1f;
    public GameObject pushToThis;
    private Vector3 pushVector;

    /*public void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.tag == "Player") && (other.gameObject.GetComponent<Rigidbody>() != null) && (pushThisWay !=null))
        {
            other.attachedRigidbody.AddForce(pushThisWay.value * moveForce);
        }
        if ((other.gameObject.tag == "Player") && (other.gameObject.GetComponent<Rigidbody>() != null) && (pushThisWay = null))
        {
            other.attachedRigidbody.AddForce(Vector3.up * moveForce);
        }
    }*/

    public void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.tag == "Player") && (other.gameObject.GetComponent<Rigidbody>() != null))
        {
            pushVector = (pushToThis.transform.position - other.gameObject.transform.position).normalized;
            other.attachedRigidbody.AddForce(pushVector, ForceMode.VelocityChange);
        }
    }
    
    /*public void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.tag == "Player") && (other.gameObject.GetComponent<Rigidbody>() != null))
        {
            other.attachedRigidbody.AddForce(Vector3.up * moveForce, ForceMode.VelocityChange);
        }
    }*/
}