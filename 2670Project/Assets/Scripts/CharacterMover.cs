using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 movement;

    public float moveSpeed = 5f, rotateSpeed = 100f, gravity = -1f, jumpforce = 10f;
    private float yVar;

    public int jumpCountMax = 2;
    private int jumpCount;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    
    private void Update()
    {
        var vInput = Input.GetAxis("Vertical");
        movement.Set(moveSpeed*vInput, -gravity, 0);
        
        var hInput = Input.GetAxis("Horizontal")*Time.deltaTime*rotateSpeed;
        transform.Rotate(0,hInput,0);

        yVar += gravity*Time.deltaTime;

        if (controller.isGrounded && movement.y < 0) ;
        {
            yVar = -1f;
            jumpCount = 0;
        }

        if (Input.GetButtonDown("Jump") && jumpCount < jumpCountMax) ;
        {
            //yVar += Mathf.Sqrt(jumpforce *  -3f * gravity);
            yVar = jumpforce;
            jumpCount++;
            print(jumpCount);
        }
        
        movement = transform.TransformDirection(movement);
        controller.Move(movement * Time.deltaTime);
    }
}