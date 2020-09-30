[2:09 PM] Anthony Romrell
using System.Collections;
using UnityEngine;
public class CharacterKnocbackBehavior : MonoBehaviour
{    //Direction of the hit
    //
    https://docs.unity3d.com/ScriptReference/CharacterController.OnControllerColliderHit.html
    https://docs.unity3d.com/ScriptReference/CharacterController-collisionFlags.html
    //
    
    public CharacterController controller;
    public Vector3 knockBackVector;
    public float knockBackForce = 50f;
    private float tempForce;
    private void Start()
    {
        tempForce = knockBackForce;
    }
    private IEnumerator OnTriggerEnter(Collider other)
    {
        knockBackForce = tempForce;
        while (knockBackForce > 0)
        {
            knockBackVector.x = knockBackForce*Time.deltaTime;
            controller.Move(knockBackVector);
            knockBackForce -= 0.1f;
            yield return new  WaitForFixedUpdate();
        }
    }
}