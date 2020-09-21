using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

public class LookAtBehavior : MonoBehaviour
{
    public Transform lookObj;

    private void Update()
    {
        transform.LookAt(lookObj);
        var transformRotation = transform.eulerAngles;
        transformRotation.x = 0;
        transformRotation.y -= 90;
        transform.rotation = Quaternion.Euler(transformRotation);
    }
}
