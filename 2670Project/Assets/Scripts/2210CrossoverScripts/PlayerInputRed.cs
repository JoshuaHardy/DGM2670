using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
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
  public IntData shotsFiredData, chargedShotsFiredData;

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
    

  private void OnAudioFilterRead(float[] data, int channels)
  {
    throw new NotImplementedException();
  }

  public Rigidbody rBodyRed;
  private float chargeFloat, regenDataHolder;
  public bool canFire = true, isCharging = false, canSprint = true;
  public GameObject fireball_S, fireball_M;
  public Transform gun;
  public ParticleSystem chargeParticles, chargeMoreParticles;
  public float moveSpeed;
  
  //Energy Management Variables
  public FloatData minValue, maxValue, energyRegen, sprintCost;

  public BoolData fullyCharged, isSprinting, isIdle, isExhausted;
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
    chargeFloat = (1 / (chargeTime.value ));
    redEnergy.value = 1.0f;
    moveSpeed = walkSpeed.value;
    fullyCharged.value = false;
    isCharging = false;
    canSprint = true;
    isSprinting.value = false;
    regenDataHolder = energyRegen.value;
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
    if ((!isSprinting.value) && (redEnergy.value < maxValue.value))
    {
      redEnergy.value += energyRegen.value * Time.deltaTime;
    }
    else
    {
      if ((isSprinting.value) && (redEnergy.value >= minValue.value) && (redMovement != Vector2.zero))
      {
        redEnergy.value -= sprintCost.value * Time.deltaTime;
      }
    }

    if (redEnergy.value > maxValue.value)
    {
      redEnergy.value = maxValue.value;
    }
    if ((redEnergy.value <= minValue.value) && (canSprint))
    {
      redEnergy.value = minValue.value;
      isExhausted.value = true;
      isSprinting.value = false;
      moveSpeed = walkSpeed.value;
      canSprint = false;
    }
    if ((redEnergy.value >= 1.0) && (!isExhausted.value))
    {
      isExhausted.value = false;
    }
    if ((!isSprinting.value) && (redMovement == Vector2.zero))
    {
      isIdle.value = true;
    }

    if (redMovement != Vector2.zero)
    {
      isIdle.value = false;
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
    if ((canSprint) && (!isExhausted.value))
    {
      moveSpeed = sprintSpeed.value;
      isSprinting.value = true;
      Debug.Log("Sprint Red On");
    }
  }

  private void RedSprintOff()
  {
    Debug.Log("Sprint Red Off");
    moveSpeed = walkSpeed.value;
    isSprinting.value = false;
  }
  private void RedFire()
  {
    Debug.Log(("FireRed Started"));
    StartCoroutine(FireShot());
  }
  
  private void RedFireBig()
  {
    Debug.Log(("RedFireBig Started"));
    if ((redEnergy.value >= 1.0f) && (fullyCharged.value == false) && (canFire))
    {
      StartCoroutine(ChargeShotBig());
      StartCoroutine(ParticleParty());
    }

    if ((redEnergy.value >= energyUseShoot.value) && (fullyCharged.value == false) && (!canFire))
    {
      Debug.Log(("ChargeShotCanceled"));
      StopCoroutine(ChargeShotBig());
      StopCoroutine(ParticleParty());
      ShootFireballRedSmall();
      chargeParticles.Stop();
      chargeMoreParticles.Stop();
      energyRegen.value = regenDataHolder;
      canFire = true;
      redEnergy.value = 0.0f;
      chargeMeter.value = 0.0f;
      fullyCharged.value = false;
      canSprint = true;
      isCharging = false;
    }

    if ((fullyCharged.value == true))
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

  private IEnumerator ChargeShotBig()
  {
    Debug.Log("ChargeShotBig: Enter");
    while (canFire)
    {
      redEnergy.value = 0.01f;
      chargeMeter.value = 0.01f;
      fullyCharged.value = false;
      energyRegen.value = 0.0f;
      canSprint = false;
      moveSpeed = walkSpeed.value;
      Debug.Log("ChargeBigShot:Init");
      canFire = false;
      isCharging = true;
      redEnergy.value += chargeFloat;
      chargeMeter.value += chargeFloat;
    }
    while ((chargeMeter.value < maxValue.value) && (!canFire))
    {
      Debug.Log("ChargeShotBig:Charging... "+chargeMeter.value.ToString()+"% complete");
      yield return new WaitForSeconds(1.0f);
      redEnergy.value += chargeFloat;
      chargeMeter.value += chargeFloat;
      
    }

    if ((chargeMeter.value >= maxValue.value))
    {
      fullyCharged.value = true;
      Debug.Log("ChargeShotBig:fullyCharged");
    }
    Debug.Log("ChargeShotBig:Exit");
    yield break;
  }

  private IEnumerator ParticleParty()
  {
    chargeParticles.Play();
    yield return new WaitForSeconds(chargeTime.value - 1);
    chargeMoreParticles.Play();
    yield break;
  }


  private void ReleaseBigShot()
  {
    if ((fullyCharged.value == true) && (redEnergy.value >= 1.0f) && (chargeMeter.value >= 1.0f))
    {
      Debug.Log("FireChargedShot");
      ShootFireballRedLarge();
      energyRegen.value = regenDataHolder;
      canFire = true;
      redEnergy.value = 0.0f;
      chargeMeter.value = 0.0f;
      fullyCharged.value = false;
      canSprint = true;
    }
    Debug.Log("FireChargedShot Exit");
  }
   void ShootFireballRedSmall()
    {
      var newBullet = Instantiate(fireball_S, gun.position, gun.rotation);
      newBullet.GetComponent<Rigidbody>().velocity = (gun.forward * projectileThrust.value);
      shotsFiredData.value++;
    }
  private void ShootFireballRedLarge()
    {
      Debug.Log(("ChargeRed.performed"));
      var newBullet = Instantiate(fireball_M, gun.position, gun.rotation);
      newBullet.GetComponent<Rigidbody>().velocity = (gun.forward * largeProjectileThrust.value);
      GetComponent<Rigidbody>().velocity = (-transform.forward * recoil.value);
      chargedShotsFiredData.value++;
    }
}



/*
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
}*/

