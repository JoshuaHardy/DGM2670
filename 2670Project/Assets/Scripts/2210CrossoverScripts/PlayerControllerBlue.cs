using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerControllerBlue : MonoBehaviour
 {
    public float walkSpeed = 20;
    public float sprintSpeed = 50;
    public float inputAngle = 0f;
    private float moveSpeed;
    public float rotationSpeed = 5;
    private Rigidbody rbody;
    [Range(0.0f, 1.0f)] public bool i; 
    private Vector3 input;
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        moveSpeed = walkSpeed;
  
    }
    
    void Update()
    {
        float horAxis = Input.GetAxis("Horizontal1");
        float vertAxis = Input.GetAxis("Vertical1");
       
        if (Input.GetButtonDown("Run1"))
            moveSpeed = sprintSpeed;
        else
            moveSpeed = walkSpeed;
        
        /*if (Input.GetKey(KeyCode.LeftShift))
            moveSpeed = sprintSpeed;
        else
            moveSpeed = walkSpeed;
            */
       
        var movement = new Vector3(horAxis, 0, vertAxis) * (moveSpeed * Time.deltaTime);

        rbody.MovePosition(transform.position + movement);
        
        var input = new Vector3(horAxis, 0, vertAxis);
        if(input != Vector3.zero)
        {
            transform.forward = input;
        }
    }
    /*IEnumerator Rotator ()
    {
        Quaternion targetRotation = Quaternion.LookRotation(input,Vector3.up );
        Quaternion currentRotation = transform.rotation;
        for (float i = 0; i < 1.0f; i += Time.deltaTime / rotationSpeed)
        {
            transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, i);
            yield return null;
        }
    }*/
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRed : MonoBehaviour
{
    public float walkSpeed = 20,sprintSpeed = 50;
    private float moveSpeed;
    public Quaternion targetRotation, lookRotation;
    public float rotationSpeed = 5;
    private Rigidbody rbody;
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        moveSpeed = walkSpeed;
        
    }
    
    void FixedUpdate()
    {
        float horAxis = Input.GetAxis("Horizontal2");
        float vertAxis = Input.GetAxis("Vertical2");
       
        if (Input.GetKey(KeyCode.LeftShift))
            moveSpeed = sprintSpeed;
        else
            moveSpeed = walkSpeed;
       
        var movement = new Vector3(horAxis, 0, vertAxis) * (moveSpeed * Time.deltaTime);

        rbody.AddForce(movement * moveSpeed);
        //rbody.MovePosition(transform.position + movement);
        Quaternion targetRotation = Quaternion.LookRotation(movement);
        var input = new Vector3(horAxis, 0, vertAxis);
        
        if(input != Vector3.zero)
        {
            transform.forward = input;
        }
    }
}

In Input make sure you set up:
L_Trigger
grav 0
Dead 0.001
Sensitivity - 1000
Type - Joystick Axis
Axis - 3rd axis (joysticks and ScrollWheel)
JoyNum - Get motion from all
//Right Trigger
if (Input.GetAxisRaw("L_Trigger") == 1){
}
 
//Left Trigger
if (Input.GetAxisRaw("L_Trigger") == -1){
}*/