using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPad : MonoBehaviour
{
    public Vector3Data teleportDestination, teleportDestinationReturn;
    public GameObject playerBlue, playerRed;
    public float teleportDelaySeconds = 1f;
    private float offset = 2f;
    public Vector3 ActiveTeleportCoordinates;
    public BoolData IsCheckPointActive;


    void Start()
    {
        ActiveTeleportCoordinates = teleportDestination.value;
    }
    public void OnTriggerEnter (Collider other)
    {
        if(other.tag == "Player") /* &&(IsCheckPointActive.value) */
        {
            
            Vector3 offsetVector3 = new Vector3(offset, 0, 0);
            Vector3 blueDestination = new Vector3(ActiveTeleportCoordinates.x - offsetVector3.x, ActiveTeleportCoordinates.y,
                ActiveTeleportCoordinates.z);
            Vector3 redDestination = new Vector3(ActiveTeleportCoordinates.x + offsetVector3.x, ActiveTeleportCoordinates.y,
                ActiveTeleportCoordinates.z);
            
            /*Vector3 offsetVector3 = new Vector3(offset, 0, 0);
            Vector3 blueDestination = new Vector3(teleportTarget.position.x - offsetVector3.x, teleportTarget.position.y,
                teleportTarget.position.z);
            Vector3 redDestination = new Vector3(teleportTarget.position.x + offsetVector3.x, teleportTarget.position.y,
                teleportTarget.position.z);*/

            StartCoroutine(teleportStall());
        
            playerBlue.transform.position = blueDestination;
            playerRed.transform.position = redDestination;
            ActiveTeleportCoordinates = teleportDestinationReturn.value;   
        }
        
    }

    IEnumerator teleportStall()
    {
        yield return new WaitForSeconds(teleportDelaySeconds);
    }

}
