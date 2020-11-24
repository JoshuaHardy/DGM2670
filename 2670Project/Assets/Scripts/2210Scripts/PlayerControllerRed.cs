using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerControllerRed : MonoBehaviour
{
    public float walkSpeed = 20;
    public float sprintSpeed = 50;
    private float moveSpeed;
    //public float rotationSpeed = 5;
    private Rigidbody rbody;
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        moveSpeed = walkSpeed;
  
    }
    
    void Update()
    {
        float horAxis = Input.GetAxis("Horizontal2");
        float vertAxis = Input.GetAxis("Vertical2");
       
        if (Input.GetKey(KeyCode.LeftShift))
            moveSpeed = sprintSpeed;
        else
            moveSpeed = walkSpeed;
       
        var movement = new Vector3(horAxis, 0, vertAxis) * (moveSpeed * Time.deltaTime);

        rbody.MovePosition(transform.position + movement);
        
        var input = new Vector3(horAxis, 0, vertAxis);
        if(input != Vector3.zero)
        {
            transform.forward = input;
        }

    }
}


