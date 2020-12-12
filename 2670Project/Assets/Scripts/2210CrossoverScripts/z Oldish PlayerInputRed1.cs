/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerInputRed : MonoBehaviour
{
  public GameObject redPlayer;
  private PlayerControls controls;
  private Vector2 redMovement;

  public FloatData
    walkSpeed,
    sprintSpeed,
    redEnergy,
    energyUseShoot,
    chargeMeter,
    projectileThrust,
    largeProjectileThrust,
    recoil,
    currentMoveSpeed,
    fireRate,
    chargeTime;

  public Rigidbody rBodyRed;
  private float particleSimSpeed;
  public bool canFire = true, isCharging = false, canSprint = true, isSprinting = false;
  public GameObject fireball_S, fireball_M;
  public Transform gun;
  public ParticleSystem chargeParticles, chargeMoreParticles;
  public float moveSpeed;
  
  //Energy Management Variables
  public FloatData minValue, maxValue, energyRegen, sprintCost;

  public BoolData fullyCharged;
  private void Awake()
  {
    rBodyRed = GetComponent<Rigidbody>();
    controls = new PlayerControls();
    chargeMeter.value = 0.0f;
    // Input System API 
    // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Interactions.html
    
    controls.Gameplay.SprintRed.started += context => RedSprintOn();
    controls.Gameplay.SprintRed.canceled += context => RedSprintOff();
    controls.Gameplay.FireRed.started += context => RedFire();
    controls.Gameplay.RightStickPress.started += context => RedFireBig();
    controls.Gameplay.MoveRed.performed += context => redMovement = context.ReadValue<Vector2>();
    controls.Gameplay.MoveRed.canceled += context => redMovement = Vector2.zero;
  }
  private void Start()
  {
    redEnergy.value = 1.0f;
    moveSpeed = walkSpeed.value;
    fullyCharged.value = false;
    isCharging = false;
    canSprint = true;
    isSprinting = false;
    
  }
  public void Update()
  {
    Vector3 mRed = new Vector3(redMovement.x, +0, redMovement.y) * (moveSpeed * Time.deltaTime);
    redPlayer.transform.Translate(mRed, Space.World);

    currentMoveSpeed.value = moveSpeed;
    
    var input = new Vector3(redMovement.x, 0, redMovement.y);
    if ((input != Vector3.zero) && (!isCharging))
    {
      transform.forward = input;
    }
    //Energy Management
    if ((!isSprinting) && (redEnergy.value < maxValue.value))
    {
      redEnergy.value += energyRegen.value * Time.deltaTime;
    }
    else
    {
      if ((isSprinting) && (redEnergy.value >= minValue.value) && (redMovement != Vector2.zero))
      {
        redEnergy.value -= sprintCost.value * Time.deltaTime;
      }
    }

    if (redEnergy.value > maxValue.value)
    {
      redEnergy.value = maxValue.value;
    }
    if (redEnergy.value <= minValue.value)
    {
      redEnergy.value = minValue.value;
      canSprint = false;
      isSprinting = false;
      moveSpeed = walkSpeed.value;
    }
    if ((redEnergy.value >= .25) && (!canSprint))
    {
      canSprint = true;
    }
  }

  private void OnEnable()
  {
    controls.Gameplay.Enable();
  }

  private void OnDisable()
  {
    controls.Gameplay.Disable();
  }
  private void RedSprintOn()
  {
    if (canSprint)
    {
      moveSpeed = sprintSpeed.value;
      isSprinting = true;
      Debug.Log("Sprint Red On");
    }
  }

  private void RedSprintOff()
  {
    Debug.Log("Sprint Red Off");
    moveSpeed = walkSpeed.value;
    isSprinting = false;
  }
  private void RedFire()
  {
    Debug.Log(("FireRed Started"));
    StartCoroutine(FireShot());
  }
  private void RedFireBig()
  {
    Debug.Log(("FireRedBig Started"));
    StartCoroutine(BigFireSetup());
  }
  private IEnumerator BigFireSetup()
  {
    if ((redEnergy.value >= 1.0f))
    {
      StartCoroutine(ChargeShotBig());
      chargeParticles.Play();
      yield return new WaitForSeconds(3.0f);
      chargeMoreParticles.Play();
      fullyCharged.value = true;
    }
    if (fullyCharged.value = true)
    {
      ReleaseBigShot();
      chargeParticles.Stop();
      chargeMoreParticles.Stop();
      isCharging = false;
    }
  }
  private IEnumerator FireShot()
    {
      while ((canFire) && (redEnergy.value >= energyUseShoot.value) && (!isCharging))
      {
        canFire = false;
        ShootFireballRedSmall();
        Debug.Log("Fire Small Red");
        redEnergy.value -= energyUseShoot.value;
        yield return new WaitForSeconds(fireRate.value);
        canFire = true;
        yield break;
      }
    }
  IEnumerator ChargeShotBig()
  {
    Debug.Log("ChargeShotBig: Enter");
    StopCoroutine(BigFireSetup());
    while (canFire)
    {
      redEnergy.value = 0.01f;
      chargeMeter.value = 0.01f;
      fullyCharged.value = false;
      canSprint = false;
      moveSpeed = walkSpeed.value;
      chargeParticles.Play();
      Debug.Log("ChargeBigShot:Init");
      canFire = false;
    }
    while ((chargeMeter.value < maxValue.value) && (!canFire))
    {
      Debug.Log("ChargeShotBig:Charging");
      chargeMeter.value += 0.165f;
      isCharging = true;
      yield return new WaitForSeconds(0.5f);
    }
    Debug.Log("ChargeShotBig:Exit");
    yield break;
  }
  private void ReleaseBigShot()
  {
    if ((fullyCharged.value = true) && (redEnergy.value >= 1.0f) && (chargeMeter.value >= 1.0f))
    {
      Debug.Log("FireChargedShot");
      ShootFireballRedBig();
      canFire = true;
      redEnergy.value = 0.0f;
      chargeMeter.value = 0.0f;
      fullyCharged.value = false;
      canSprint = true;
    }
    Debug.Log("FireChargedShot Exit");
  }
  private void ShootFireballRedSmall()
    {
      var newBullet = Instantiate(fireball_S, gun.position, gun.rotation);
      newBullet.GetComponent<Rigidbody>().velocity = (gun.forward * projectileThrust.value);
    }
  private void ShootFireballRedBig()
    {
      Debug.Log(("ChargeRed.performed"));
      var newBullet = Instantiate(fireball_M, gun.position, gun.rotation);
      newBullet.GetComponent<Rigidbody>().velocity = (gun.forward * largeProjectileThrust.value);
      GetComponent<Rigidbody>().velocity = (-transform.forward * recoil.value);
    }
}
*/

