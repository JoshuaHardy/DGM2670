using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputTest : MonoBehaviour
{
  private PlayerControls controls;
  private Vector2 blueMovement, redMovement;

  private void Awake()
  {
    controls = new PlayerControls();

    controls.Gameplay.Grow.performed += context => Grow();
    controls.Gameplay.Shrink.performed += context => Shrink();
    controls.Gameplay.MoveBlue.performed += context => blueMovement = context.ReadValue<Vector2>();
    controls.Gameplay.MoveBlue.canceled += context => blueMovement = Vector2.zero;
    controls.Gameplay.MoveRed.performed += context => redMovement = context.ReadValue<Vector2>();
    controls.Gameplay.MoveRed.canceled += context => redMovement = Vector2.zero;

  }

  private void Update()
  {
    Vector2 mB = new Vector2(blueMovement.x, blueMovement.y)*Time.deltaTime;
    transform.Translate(mB, Space.World);
    Vector2 mR = new Vector2(-redMovement.y, redMovement.x) * (100f * Time.deltaTime);
    transform.Rotate(mR, Space.World);
  }

  private void Grow()
  {
    transform.localScale *= 1.1f;
  }
  
  private void Shrink()
  {
    transform.localScale *= 0.9f;
  }

  private void OnEnable()
  {
    controls.Gameplay.Enable();
  }
  private void OnDisable()
  {
    controls.Gameplay.Disable();
  }

  /*private void moveBlue()
  {
    float horAxis = Input.GetAxis("Horizontal1");
    float vertAxis = Input.GetAxis("Vertical1");
       
    if (Input.GetButtonDown("Run1"))
      moveSpeed = sprintSpeed;
    else
      moveSpeed = walkSpeed;
  }*/
}
