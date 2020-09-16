using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLocator : MonoBehaviour
{
    private Camera cam;
    public Transform pointObj;

    private void Start()
    {
        cam = Camera.main;
    }
    private void OnMouseDown()
    {
       if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out var Hit, 100))
        {
            pointObj.position = Hit.point;
        }
    }
}
