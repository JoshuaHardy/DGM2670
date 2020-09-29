using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TriggerEventBehavior : MonoBehaviour
{
    public UnityEvent triggerEnterEvent;
    public float delayTime = 1f;
    private WaitForSeconds waitObj;

    private void Start()
    {
        waitObj = new WaitForSeconds(delayTime);
    }
    private IEnumerator OnTriggerEnter(Collider other)
    {
        yield return waitObj;
        triggerEnterEvent.Invoke();
    }
}