using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerInputBlue : MonoBehaviour
{
  public GameObject bluePlayer;
  private PlayerControls controls;
  private Vector2 blueMovement;

  public FloatData 
    speed,
    sprintCoefficient,
    blueEnergy,
    energyRegen,
    energyUseRun,
    energyUseShoot,
    energyUseBig,
    chargeMeter;

  private float sprintPlaceholder = 1.0f, particleSimSpeed;
  public bool canFire = true, isCharging = false, isHoldingFire = false, canSprint = true, isSprinting = false;
  public GameObject fireball_S, fireball_M;
  public Transform gun;
  public int projectileThrust = 15;
  public ParticleSystem chargeParticles;
  public float walkSpeed, sprintSpeed, holdFireTime;
  
  //Energy Management Variables
  public FloatData minValue, maxValue, positiveCoefficient, negativeCoefficient;

  //public BoolData blueSprinting, blueCharging;
  private void Awake()
  {
    controls = new PlayerControls();
    chargeParticles = GetComponent<ParticleSystem>();
    chargeMeter.value = 0.0f;
    // Input System API 
    https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Interactions.html
    
    controls.Gameplay.SprintBlue.started += context => BlueSprintOn();
    controls.Gameplay.SprintBlue.canceled += context => BlueSprintOff();
    
    /*controls.Gameplay.TestWestPressed.started += context => BlueFire();
    controls.Gameplay.TestWestHold.started += context => StartCharging();
    controls.Gameplay.TestWestHold.performed += context => print(" ChargeBlue Hold performed");//ShootFireballBlueBig();
    controls.Gameplay.TestWestHold.canceled += context => StopCharging();*/
    
    controls.Gameplay.FireBlue.started += context => BlueFire();
    controls.Gameplay.ChargeBlue.started += context => StartCharging();
    controls.Gameplay.ChargeBlue.performed += context => print(" ChargeBlue Hold performed");//ShootFireballBlueBig();
    controls.Gameplay.ChargeBlue.canceled += context => StopCharging();
   
    /*controls.Gameplay.TestWestPressed.started += context => print("Start West Registered");
    controls.Gameplay.TestWestPressed.canceled += context => print("Start West canceled");
    controls.Gameplay.TestWestPressed.performed += context => print("Start West performed");
    controls.Gameplay.TestWestHold.started += context => print("Start West Hold Started");
    controls.Gameplay.TestWestHold.canceled += context => print("Start West Hold canceled");
    controls.Gameplay.TestWestHold.performed += context => print("Start West Hold performed");*/
    
    controls.Gameplay.MoveBlue.performed += context => blueMovement = context.ReadValue<Vector2>();
    controls.Gameplay.MoveBlue.canceled += context => blueMovement = Vector2.zero;

  }

  private void Start()
  {
    blueEnergy.value = 1.0f;
    walkSpeed = speed.value;
    sprintSpeed = (speed.value * sprintCoefficient.value);
  }

  public void Update()
  {
    Vector3 mBlue = new Vector3(blueMovement.x, +0, blueMovement.y) * speed.value * Time.deltaTime;
    bluePlayer.transform.Translate(mBlue, Space.World);

    var input = new Vector3(blueMovement.x, 0, blueMovement.y);
    if ((input != Vector3.zero) && (!isCharging))
    {
      transform.forward = input;
    }
    var pSmain = chargeParticles.main;
    pSmain.simulationSpeed = particleSimSpeed;
    
      //Energy Management
    if ((!isSprinting) && (blueEnergy.value < maxValue.value))
    {
      blueEnergy.value += positiveCoefficient.value * Time.deltaTime;
    }
    else
    {
      if ((isSprinting) && (blueEnergy.value >= minValue.value))
      {
        blueEnergy.value -= negativeCoefficient.value * Time.deltaTime;
      }
    }

    if (blueEnergy.value > maxValue.value)
    {
      blueEnergy.value = maxValue.value;
    }
    if (blueEnergy.value <= minValue.value)
    {
      blueEnergy.value = minValue.value;
      canSprint = false;
      isSprinting = false;
      speed.value = walkSpeed;
    }
    if (blueEnergy.value >= .5)
    {
      canSprint = true;
    }
  }

  private void BlueSprintOn()
  {
    if (canSprint)
    {
      speed.value = sprintSpeed;
      isSprinting = true;
      Debug.Log("Sprint Blue On");
    }
  }

  void BlueSprintOff()
  {
    Debug.Log("Sprint Blue Off");
    speed.value = walkSpeed;
    isSprinting = false;
  }

  private void StartCharging()
  {
    Debug.Log(("ChargeBlue.started"));
    isHoldingFire = true;
    StartCoroutine(ChargeBigShot());
  }
  private void BlueFire()
  {
    Debug.Log(("FireBlue Started"));
    
    if (!isHoldingFire)
    {
      FireShot();
    }
  }

  

  private void StopCharging()
  {
    isHoldingFire = false;
    isCharging = false;
    Debug.Log("ChargeBlue.canceled");
    //StopCoroutine(ChargeBigShot());
    chargeMeter.value = 0.0f;
    particleSimSpeed = 0.0f;
    StartCoroutine(Reload());
    /*ShootFireballBlueSmall();
    blueEnergy.value -= energyUseShoot.value;*/
  }
  
  

  private void OnEnable()
  {
    controls.Gameplay.Enable();
  }

  private void OnDisable()
  {
    controls.Gameplay.Disable();
  }

  private void FireShot()
  {
    while ((canFire) && (blueEnergy.value >= energyUseShoot.value))
    {
      canFire = false;
      ShootFireballBlueSmall();
      Debug.Log("Fire Small Blue");
      blueEnergy.value -= energyUseShoot.value;
    }
  }

  IEnumerator ChargeBigShot()
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

      if ((chargeMeter.value >= .95f) && (blueEnergy.value >= .98f) && (isCharging))
      {
        Debug.Log("ChargeBigShot:FireBig");
        ShootFireballBlueBig();
        blueEnergy.value -= energyUseBig.value;
        isCharging = false;
        particleSimSpeed = 0.0f;
        chargeMeter.value = 0.0f;
      }

      if ((chargeMeter.value < .95f) || ((blueEnergy.value < .98f) && (blueEnergy.value >= energyUseShoot.value )))
      {
        Debug.Log("ChargeBigShot:FireSmall");
        canFire = true;
        FireShot();
      }
      Debug.Log("Charging Coroutine Exit");
      yield break;
    }
   

    void ShootFireballBlueSmall()
    {
      var newBullet = Instantiate(fireball_S, gun.position, gun.rotation);
      newBullet.GetComponent<Rigidbody>().velocity = (gun.forward * projectileThrust);
      StartCoroutine(Reload());
    }

    void ShootFireballBlueBig()
    {
      Debug.Log(("ChargeBlue.performed"));
      var newBullet = Instantiate(fireball_M, gun.position, gun.rotation);
      newBullet.GetComponent<Rigidbody>().velocity = (gun.forward * projectileThrust * .75f);
      GetComponent<Rigidbody>().velocity = (transform.forward * projectileThrust * .01f); 
      // rBody; somethingsomething rbody.addforce -value forward for knockback?
      StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
      StopCoroutine(ChargeBigShot());
      yield return new WaitForSeconds(0.5f);
      canFire = true;
    }
}
