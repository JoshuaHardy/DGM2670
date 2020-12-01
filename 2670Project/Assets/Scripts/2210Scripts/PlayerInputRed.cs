using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputRed : MonoBehaviour
{
  public GameObject redPlayer;
  private PlayerControls controls;
  private Vector2 redMovement;
  private float sprintPlaceholder = 1.0f;
  public FloatData speed, sprintCoefficient;
  //public BoolData blueSprinting, redSprinting, blueCharging, redCharging;
  private void Awake()
  {
    controls = new PlayerControls();
    
    controls.Gameplay.SprintRed.started += context => RedSprintOn();
    controls.Gameplay.SprintRed.canceled += context => RedSprintOff();
    controls.Gameplay.FireRed.performed += context => RedFire();
    
    controls.Gameplay.MoveRed.performed += context => redMovement = context.ReadValue<Vector2>();
    controls.Gameplay.MoveRed.canceled += context => redMovement = Vector2.zero;

  }

  public void Update()
  {
    Vector3 mR = new Vector3(redMovement.x, +0, redMovement.y) * speed.value * sprintPlaceholder * Time.deltaTime;
    redPlayer.transform.Translate(mR, Space.World);
  }

  private void RedSprintOn()
  {
    sprintPlaceholder *= sprintCoefficient.value; 
    print("Sprint Red On");
  }
  
  private void RedSprintOff()
  {
    print("Sprint Red Off");
    sprintPlaceholder /= sprintCoefficient.value;
  }

  private void RedFire()
  {
    print("Fire Red");
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
  }#1#*/
}

