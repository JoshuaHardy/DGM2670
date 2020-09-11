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

    public float rotateSpeed = 100f, gravity = -1f, jumpforce = 10f;
    private float yVar;

    public FloatData moveSpeed, normalSpeed, fastSpeed;
    
    public int jumpCountMax = 2;
    private int jumpCount;

    public Vector3Data currentSpawnPoint;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = fastSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = normalSpeed;
        }
        
        var vInput = Input.GetAxis("Vertical");
        movement.Set(moveSpeed.value*vInput, -gravity, 0);
        
        var hInput = Input.GetAxis("Horizontal")*Time.deltaTime*rotateSpeed;
        transform.Rotate(0,hInput,0);

        yVar += gravity*Time.deltaTime;

         /*if (controller.isGrounded && movement.y < 0);
         {
             yVar = -1f;
             jumpCount = 0;
         }
*/
         if (Input.GetButtonDown("Jump") && jumpCount < playerJumpCountMax.value) ;
         {
             //yVar += Mathf.Sqrt(jumpforce *  -3f * gravity);
             yVar = jumpforce;
             jumpCount++;
             print(jumpCount);
         }
        
        movement = transform.TransformDirection(movement);
        controller.Move(movement * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
         // Set the location data of the player to the currentSpawnPoint Scene Object.
         
    }

   
}