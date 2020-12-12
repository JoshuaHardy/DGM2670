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
  public IntData shotsFiredData, chargedShotsFiredData;

  public FloatData
    walkSpeed,
    sprintSpeed,
    blueEnergy,
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

  public Rigidbody rBodyBlue;
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
    rBodyBlue = GetComponent<Rigidbody>();
    controls = new PlayerControls();
    chargeMeter.value = 0.0f;
    // Input System API 
    // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Interactions.html
    
    controls.Gameplay.SprintBlue.started += context => BlueSprintOn();
    controls.Gameplay.SprintBlue.canceled += context => BlueSprintOff();
    controls.Gameplay.FireBlue.started += context => BlueFire();
    controls.Gameplay.LeftStickPress.started += context => BlueFireBig();
    controls.Gameplay.MoveBlue.performed += context => blueMovement = context.ReadValue<Vector2>();
    controls.Gameplay.MoveBlue.canceled += context => blueMovement = Vector2.zero;
  }
  private void Start()
  {
    chargeFloat = (1 / (chargeTime.value ));
    blueEnergy.value = 1.0f;
    moveSpeed = walkSpeed.value;
    fullyCharged.value = false;
    isCharging = false;
    canSprint = true;
    isSprinting.value = false;
    regenDataHolder = energyRegen.value;
  }
  public void Update()
  {
    Vector3 mBlue = new Vector3(blueMovement.x, +0, blueMovement.y) * (moveSpeed * Time.deltaTime);
    bluePlayer.transform.Translate(mBlue, Space.World);
    currentMoveSpeed.value = moveSpeed;
    
    var input = new Vector3(blueMovement.x, 0, blueMovement.y);
    if ((input != Vector3.zero) && (!isCharging))
    {
      transform.forward = input;
    }
    //Energy Management
    if ((!isSprinting.value) && (blueEnergy.value < maxValue.value))
    {
      blueEnergy.value += energyRegen.value * Time.deltaTime;
    }
    else
    {
      if ((isSprinting.value) && (blueEnergy.value >= minValue.value) && (blueMovement != Vector2.zero))
      {
        blueEnergy.value -= sprintCost.value * Time.deltaTime;
      }
    }

    if (blueEnergy.value > maxValue.value)
    {
      blueEnergy.value = maxValue.value;
    }
    if ((blueEnergy.value <= minValue.value) && (canSprint))
    {
      blueEnergy.value = minValue.value;
      isExhausted.value = true;
      isSprinting.value = false;
      moveSpeed = walkSpeed.value;
      canSprint = false;
    }
    if ((blueEnergy.value >= 1.0) && (!isExhausted.value))
    {
      isExhausted.value = false;
    }
    if ((!isSprinting.value) && (blueMovement == Vector2.zero))
    {
      isIdle.value = true;
    }

    if (blueMovement != Vector2.zero)
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
  private void BlueSprintOn()
  {
    if ((canSprint) && (!isExhausted.value))
    {
      moveSpeed = sprintSpeed.value;
      isSprinting.value = true;
      Debug.Log("Sprint Blue On");
    }
  }

  private void BlueSprintOff()
  {
    Debug.Log("Sprint Blue Off");
    moveSpeed = walkSpeed.value;
    isSprinting.value = false;
  }
  private void BlueFire()
  {
    Debug.Log(("FireBlue Started"));
    StartCoroutine(FireShot());
  }
  
  private void BlueFireBig()
  {
    Debug.Log(("BlueFireBig Started"));
    if ((blueEnergy.value >= 1.0f) && (fullyCharged.value == false) && (canFire))
    {
      StartCoroutine(ChargeShotBig());
      StartCoroutine(ParticleParty());
    }

    if ((blueEnergy.value >= energyUseShoot.value) && (fullyCharged.value == false) && (!canFire))
    {
      Debug.Log(("ChargeShotCanceled"));
      StopCoroutine(ChargeShotBig());
      StopCoroutine(ParticleParty());
      ShootFireballBlueSmall();
      chargeParticles.Stop();
      chargeMoreParticles.Stop();
      energyRegen.value = regenDataHolder;
      canFire = true;
      blueEnergy.value = 0.0f;
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
      while ((canFire) && (blueEnergy.value >= energyUseShoot.value) && (!isCharging))
      {
        canFire = false;
        ShootFireballBlueSmall();
        Debug.Log("Fire Small Blue");
        blueEnergy.value -= energyUseShoot.value;
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
      blueEnergy.value = 0.01f;
      chargeMeter.value = 0.01f;
      fullyCharged.value = false;
      energyRegen.value = 0.0f;
      canSprint = false;
      moveSpeed = walkSpeed.value;
      Debug.Log("ChargeBigShot:Init");
      canFire = false;
      isCharging = true;
      blueEnergy.value += chargeFloat;
      chargeMeter.value += chargeFloat;
    }
    while ((chargeMeter.value < maxValue.value) && (!canFire))
    {
      Debug.Log("ChargeShotBig:Charging... "+chargeMeter.value.ToString()+"% complete");
      yield return new WaitForSeconds(1.0f);
      blueEnergy.value += chargeFloat;
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
    if ((fullyCharged.value == true) && (blueEnergy.value >= 1.0f) && (chargeMeter.value >= 1.0f))
    {
      Debug.Log("FireChargedShot");
      ShootFireballBlueLarge();
      energyRegen.value = regenDataHolder;
      canFire = true;
      blueEnergy.value = 0.0f;
      chargeMeter.value = 0.0f;
      fullyCharged.value = false;
      canSprint = true;
    }
    Debug.Log("FireChargedShot Exit");
  }
   void ShootFireballBlueSmall()
    {
      var newBullet = Instantiate(fireball_S, gun.position, gun.rotation);
      newBullet.GetComponent<Rigidbody>().velocity = (gun.forward * projectileThrust.value);
      shotsFiredData.value++;
    }
  private void ShootFireballBlueLarge()
    {
      Debug.Log(("ChargeBlue.performed"));
      var newBullet = Instantiate(fireball_M, gun.position, gun.rotation);
      newBullet.GetComponent<Rigidbody>().velocity = (gun.forward * largeProjectileThrust.value);
      GetComponent<Rigidbody>().velocity = (-transform.forward * recoil.value);
      chargedShotsFiredData.value++;
    }
}

