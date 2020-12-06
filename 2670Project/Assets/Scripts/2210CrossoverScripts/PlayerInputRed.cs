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
  public FloatData speed, sprintCoefficient, redEnergy, energyRegen, energyUseRun, energyUseShoot;
  private float sprintPlaceholder = 1.0f;
  public bool canFire;
  public BoolData isSprinting;
  public GameObject fireball_S; 
  public Transform gun;
  public int projectileThrust = 15;
  //public BoolData blueSprinting, redSprinting, blueCharging, redCharging;
  private void Awake()
  {
    controls = new PlayerControls();
    
    controls.Gameplay.SprintRed.started += context => RedSprintOn();
    controls.Gameplay.SprintRed.canceled += context => RedSprintOff();
    controls.Gameplay.FireRed.started += context => RedFire();
    
    controls.Gameplay.MoveRed.performed += context => redMovement = context.ReadValue<Vector2>();
    controls.Gameplay.MoveRed.canceled += context => redMovement = Vector2.zero;

  }

  public void Update()
  {
    Vector3 mR = new Vector3(redMovement.x, +0, redMovement.y) * speed.value * sprintPlaceholder * Time.deltaTime;
    redPlayer.transform.Translate(mR, Space.World);
    
    var input = new Vector3(redMovement.x, 0, redMovement.y);
    if(input != Vector3.zero)
    {
      transform.forward = input;
    }
  }
  private void RedSprintOn()
  {
    sprintPlaceholder *= sprintCoefficient.value;
    // StopCoroutine(IncreaseEnergy());
    isSprinting.value = true;
    print("Sprint Red On");
  }

  void RedSprintOff()
  {
    print("Sprint Red Off");
    // StartCoroutine (IncreaseEnergy ());
    isSprinting.value = false;
    sprintPlaceholder /= sprintCoefficient.value;
  }

  private void RedFire()
  {
    print("Fire Red");
    StartCoroutine(FireShot());
  }


  private void OnEnable()
  {
    controls.Gameplay.Enable();
  }
  private void OnDisable()
  {
    controls.Gameplay.Disable();
  }

  IEnumerator FireShot()
  {
    while ((canFire) && (redEnergy.value >= energyUseShoot.value))
    {
      canFire = false;
      shootFireballBlueSmall();
      redEnergy.value -= energyUseShoot.value;
      yield return new WaitForSeconds(0.5f);
      canFire = true;
      yield break;
    }  
  }
  public void shootFireballBlueSmall()
  {
    var newBullet = Instantiate(fireball_S, gun.position, gun.transform.rotation);
    newBullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(gun.transform.forward * projectileThrust);
  }
}

/*
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
  }#2##1#
}
*/

