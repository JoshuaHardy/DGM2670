using System;
using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputBlue : MonoBehaviour
{
  public GameObject bluePlayer;
  private PlayerControls controls;
  private Vector2 blueMovement;
  public FloatData speed, sprintCoefficient, blueEnergy, energyRegen, energyUseRun, energyUseShoot;
  private float sprintPlaceholder = 1.0f;
  public bool canFire;
  public BoolData isSprinting;
  public GameObject fireball_S; 
  public Transform gun;
  public int projectileThrust = 10;

  //public BoolData blueSprinting, blueCharging;
  private void Awake()
  {
    controls = new PlayerControls();

    controls.Gameplay.SprintBlue.started += context => BlueSprintOn();
    controls.Gameplay.SprintBlue.canceled += context => BlueSprintOff();
    controls.Gameplay.FireBlue.started += context => BlueFire();
    //StartCoroutine (IncreaseEnergy ());
    

    controls.Gameplay.MoveBlue.performed += context => blueMovement = context.ReadValue<Vector2>();
    controls.Gameplay.MoveBlue.canceled += context => blueMovement = Vector2.zero;

  }

  public void Update()
  {
    Vector3 mB = new Vector3(blueMovement.x, +0, blueMovement.y) * speed.value * sprintPlaceholder * Time.deltaTime;
    bluePlayer.transform.Translate(mB, Space.World);
  }

  private void BlueSprintOn()
  {
    sprintPlaceholder *= sprintCoefficient.value;
   // StopCoroutine(IncreaseEnergy());
   isSprinting.value = true;
    print("Sprint Blue On");
  }

  void BlueSprintOff()
  {
    print("Sprint Blue Off");
   // StartCoroutine (IncreaseEnergy ());
   isSprinting.value = false;
    sprintPlaceholder /= sprintCoefficient.value;
  }

  private void BlueFire()
  {
    print("Fire Blue");
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
    while ((canFire) && (blueEnergy.value >= energyUseShoot.value))
    {
        canFire = false;
        shootFireballBlueSmall();
        blueEnergy.value -= energyUseShoot.value;
        yield return new WaitForSeconds(0.5f);
        canFire = true;
        yield break;
    }  
  }
  /*IEnumerator IncreaseEnergy()
  {
    while (canRegen)
    {
      blueEnergy.value += energyRegen.value;
      yield return new WaitForSeconds(1.0f);
    }
  }*/
  
  public void shootFireballBlueSmall()
  {
    var newBullet = Instantiate(fireball_S, gun.transform.position, gun.transform.rotation);
    newBullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(gun.transform.forward * projectileThrust);
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
