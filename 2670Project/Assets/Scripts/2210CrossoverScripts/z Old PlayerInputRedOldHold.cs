using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerInputRedOldHold : MonoBehaviour
{
  public GameObject redPlayer;
  private PlayerControls controls;
  private Vector2 redMovement;

  public FloatData
    walkSpeed,
    sprintSpeed,
    redEnergy,
    energyUseShoot,
    energyUseBig,
    chargeMeter;

  public Rigidbody rBodyRed;
  private float particleSimSpeed;
  public bool canFire = true, isCharging = false, isHoldingFire = false, canSprint = true, isSprinting = false;
  public GameObject fireball_S, fireball_M;
  public Transform gun;
  public int projectileThrust = 15;
  public ParticleSystem chargeParticles;
  public float moveSpeed;
  
  //Energy Management Variables
  public FloatData minValue, maxValue, positiveCoefficient, negativeCoefficient;

  public BoolData fullyCharged;
  private void Awake()
  {
    rBodyRed = GetComponent<Rigidbody>();
    controls = new PlayerControls();
    chargeParticles = GetComponent<ParticleSystem>();
    chargeMeter.value = 0.0f;
    // Input System API 
    // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Interactions.html
    
    controls.Gameplay.SprintRed.started += context => RedSprintOn();
    controls.Gameplay.SprintRed.canceled += context => RedSprintOff();
    
    //controls.Gameplay.TestWestPressed.started += context => RedFire();
    //controls.Gameplay.TestWestHold.started += context => StartCharging();
    //controls.Gameplay.TestWestHold.performed += context => StartCoroutine(CompleteCharging());
    //controls.Gameplay.TestWestHold.canceled += context => StopCharging();
    
    controls.Gameplay.FireRed.started += context => RedFire();
    /*controls.Gameplay.ChargeRed.started += context => StartCharging();
    controls.Gameplay.ChargeRed.performed += context => StartCoroutine(CompleteCharging());
    controls.Gameplay.ChargeRed.canceled += context => StopCharging();*/
   
    /*controls.Gameplay.TestWestPressed.started += context => print("Start West Registered");
    controls.Gameplay.TestWestPressed.canceled += context => print("Start West canceled");
    controls.Gameplay.TestWestPressed.performed += context => print("Start West performed");
    controls.Gameplay.TestWestHold.started += context => print("Start West Hold Started");
    controls.Gameplay.TestWestHold.canceled += context => print("Start West Hold canceled");
    controls.Gameplay.TestWestHold.performed += context => print("Start West Hold performed");*/
    
    controls.Gameplay.MoveRed.performed += context => redMovement = context.ReadValue<Vector2>();
    controls.Gameplay.MoveRed.canceled += context => redMovement = Vector2.zero;

  }

  private void Start()
  {
    redEnergy.value = 1.0f;
    moveSpeed = walkSpeed.value;
    fullyCharged.value = false;
    isCharging = false;
  }

  public void Update()
  {
    Vector3 mRed = new Vector3(redMovement.x, +0, redMovement.y) * (moveSpeed * Time.deltaTime);
    redPlayer.transform.Translate(mRed, Space.World);
   
    var input = new Vector3(redMovement.x, 0, redMovement.y);
    if ((input != Vector3.zero) && (!isCharging))
    {
      transform.forward = input;
    }
    var pSmain = chargeParticles.main;
    pSmain.simulationSpeed = particleSimSpeed;
    
      //Energy Management
    if ((!isSprinting) && (redEnergy.value < maxValue.value))
    {
      redEnergy.value += positiveCoefficient.value * Time.deltaTime;
    }
    else
    {
      if ((isSprinting) && (redEnergy.value >= minValue.value))
      {
        redEnergy.value -= negativeCoefficient.value * Time.deltaTime;
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
    if (redEnergy.value >= .5)
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

  private void StartCharging()
  {
    Debug.Log(("ChargeRed.started"));
    isHoldingFire = true;
    StartCoroutine(ChargeBigShot());
  }
  private void RedFire()
  {
    Debug.Log(("FireRed Started"));
    
    if (!isHoldingFire)
    {
      StartCoroutine(FireShot());
    }
  }

  

  private void StopCharging()
  {
    isHoldingFire = false;
    isCharging = false;
    Debug.Log("ChargeRed.canceled");
    //StopCoroutine(ChargeBigShot());
    chargeMeter.value = 0.0f;
    particleSimSpeed = 0.0f;
    StopCoroutine(ChargeBigShot());
    /*yield return new WaitForSeconds(0.5f);
    canFire = true;*/
    ShootFireballRedSmall();
    redEnergy.value -= energyUseShoot.value;
  }
  
  private IEnumerator FireShot()
    {
      while ((canFire) && (redEnergy.value >= energyUseShoot.value))
      {
        canFire = false;
        ShootFireballRedSmall();
        Debug.Log("Fire Small Red");
        redEnergy.value -= energyUseShoot.value;
        yield return new WaitForSeconds(0.25f);
        canFire = true;
        yield break;
      }
    }

  private IEnumerator ChargeBigShot()
    {
      Debug.Log("ChargeBigShot: Enter");
      while ((canFire) && (!isCharging) && (chargeMeter.value <= 0.1f))
      {
        Debug.Log("ChargeBigShot:Init");
        canFire = false;
        isCharging = true;
        yield return particleSimSpeed = 1.0f;
        
      }

      while ((chargeMeter.value <= 1.1f) && (isCharging))
      {
        Debug.Log("ChargeBigShot:Charging");
        chargeMeter.value += .165f;
        yield return new WaitForSeconds(0.5f);
        particleSimSpeed += 0.5f;
      }

      if ((chargeMeter.value >= .95f) && (redEnergy.value >= .98f) && (isCharging))
      {
        Debug.Log("ChargeBigShot:FireBig");
        fullyCharged.value = true;
        ShootFireballRedBig();
        redEnergy.value -= energyUseBig.value;
        isCharging = false;
        particleSimSpeed = 0.0f;
        chargeMeter.value = 0.0f;
      }

      if ((chargeMeter.value < .95f) || ((redEnergy.value < .98f) && (redEnergy.value >= energyUseShoot.value )))
      {
        Debug.Log("ChargeBigShot:FireSmall");
        canFire = true;
        StartCoroutine(FireShot());
        yield return new WaitForSeconds(0.5f);
        canFire = true;
        isCharging = false;
      }
      Debug.Log("ChargeBigShot Exit");
      yield break;
    }
  
  private IEnumerator CompleteCharging()
  {
    Debug.Log(" ChargeRed Hold performed");
    while ((isHoldingFire) && (fullyCharged.value))
    {
      ShootFireballRedBig();
      fullyCharged.value = false;
    }
    yield break;
  }
  
    private void ShootFireballRedSmall()
    {
      var newBullet = Instantiate(fireball_S, gun.position, gun.rotation);
      newBullet.GetComponent<Rigidbody>().velocity = (gun.forward * projectileThrust);
    }

    private void ShootFireballRedBig()
    {
      Debug.Log(("ChargeRed.performed"));
      var newBullet = Instantiate(fireball_M, gun.position, gun.rotation);
      newBullet.GetComponent<Rigidbody>().velocity = (gun.forward * projectileThrust * .75f);
      GetComponent<Rigidbody>().velocity = (transform.forward * projectileThrust * .01f); 
      // rBody; somethingsomething rbody.addforce -value forward for knockback?
    }

  
}

