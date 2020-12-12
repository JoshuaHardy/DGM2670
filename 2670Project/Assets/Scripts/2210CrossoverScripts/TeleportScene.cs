using System;
using System.Collections;
using UnityEngine;
public class TeleportScene : MonoBehaviour
{
    public GameObject playerBlue, playerRed;
    public GameObject outDestination;
    public Vector3Data destination;
    private Vector3 offsetVector3, blueDestination, redDestination;
    void Start()
    {
        //destination = outDestination.transform.position;
    }
    public void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            TeleportPlayer();
        }
    }
    private void TeleportPlayer()
    {
        offsetVector3 = new Vector3(2, 1.5f, 0);
       blueDestination = new Vector3(destination.value.x - offsetVector3.x, destination.value.y + + offsetVector3.y,
           destination.value.z);
        redDestination = new Vector3(destination.value.x + offsetVector3.x, destination.value.y + offsetVector3.y,
            destination.value.z);
        playerBlue.transform.position = blueDestination;
            playerRed.transform.position = redDestination;
    }
}
