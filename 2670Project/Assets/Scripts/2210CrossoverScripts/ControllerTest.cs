/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour
{
    private Vector3 startPos;
    private Transform thisTransform;
    private MeshRenderer mR;
    void Start()
    {
        thisTransform = transform;
        startPos = thisTransform.position;
        mR = thisTransform.GetComponent<MeshRenderer>();
    }
    void Update()
    {
        if (isButton)
        {
            mR.enabled = Input.GetButton (buttonName);
        }
        else
        {
            if (leftJoystick)
            {
                Vector3 inputDirection = Vector3.zero;
                inputDirection.x = Input.GetAxis("LeftJoystickHorizontal");
                inputDirection.z = Input.GetAxis("LeftJoystickHorizontal");
                thisTransform.position = startPos + inputDirection;
            }
            else
            {
                Vector3 inputDirectoin = Vector3.zero;
                inputDirection.x = Input.GetAxis("RightJoystickHorizontal");
                inputDirection.z = Input.GetAxis("RightJoystickHorizontal");
                thisTransform.position = startPos + inputDirection;
            }
        }
    }
}
*/
