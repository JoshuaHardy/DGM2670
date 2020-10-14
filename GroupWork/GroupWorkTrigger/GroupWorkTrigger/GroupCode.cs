// IntData
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class IntData : ScriptableObject
{
    public int value;
}

// Player Controller
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 movement, lookDirection;
    private float gravity = -9.81f, yAxisVar;
    public float speed, normalSpeed, fastSpeed, jumpForce;
    public int jumpMax;
    private int jumpCount;
    

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = normalSpeed;
    }

    private void Update()
    {
        yAxisVar += gravity * Time.deltaTime;

        if (controller.isGrounded && movement.y < 0)
        {
            yAxisVar = -1;
            jumpCount = 0;
        }

        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            yAxisVar = jumpForce;
            jumpCount++;
        }
        
        var verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Fire3"))
        {
            speed = fastSpeed;
        }
        else if (Input.GetButtonUp("Fire3"))
        {
            speed = normalSpeed;
        }

        lookDirection.Set(horizontalInput, 0, verticalInput);

        if (lookDirection == Vector3.zero)
        {
            lookDirection.Set(0.0001f, 0, 0.0001f);
        }
        
        if (horizontalInput > 0.5f || horizontalInput < -0.5f ||verticalInput > 0.5f || verticalInput < -0.5f)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }

        movement.Set(horizontalInput, yAxisVar, verticalInput);
        controller.Move(movement * (speed * Time.deltaTime));
    }
}
//TimerUI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class TimerUI : MonoBehaviour
{
    public int maxTime;
    public IntData time;
    public Text timerText;
    
    // Start is called before the first frame update
    void Start()
    {
        time.value = maxTime;
    }

    public IEnumerator Countdown()
    {
        time.value = maxTime;
        while (time.value >= 0)
        {
            DisplayTimer();
            yield return new WaitForSeconds(1.0f);
            time.value--;
        }
    }

    public void DisplayTimer()
    {
        timerText.text = time.value.ToString();
    }
}

//TriggerBehavior

using System.Collections;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class TriggerBehaviour : MonoBehaviour
{
    public Color newColor;
    public Color defaultColor;
    private MeshRenderer meshR;
    private WaitForSeconds wfs;
    public int holdTime = 10;
    public GameObject door;
    public TimerUI timer;

    void Start()
    {
        meshR = GetComponent<MeshRenderer>();
        meshR.material.color = defaultColor;
        wfs = new WaitForSeconds(holdTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        newColor.a = 0.5f;
        meshR.material.color = newColor;
        door.SetActive(false);
        
        
    }

    private IEnumerator OnTriggerExit(Collider other)
    { 
        StartCoroutine(timer.Countdown());
        yield return wfs;
        meshR.material.color = defaultColor;
        door.SetActive(true);
    }
    
  
}//
