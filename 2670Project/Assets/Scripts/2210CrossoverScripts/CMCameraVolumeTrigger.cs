using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CMCameraVolumeTrigger : MonoBehaviour
{
    public int numberOfPlayers = 0;
    public bool farCamBool = false, midCamBool = false, nearCamBool = false;
    public CinemachineVirtualCamera defaultCam, farBossRoomCam, midBossRoomCam, nearBossRoomCam;
    // Start is called before the first frame update
    void Update ()
    {
        
    }
    void Start ()
    {
        
    }

    public void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            defaultCam.Priority--;
            if (farCamBool == true)
            {
                farBossRoomCam.Priority++;
            }
            if (midCamBool == true)
            {
                midBossRoomCam.Priority++;
            }
            if (nearCamBool == true)
            {
                nearBossRoomCam.Priority++;
            }
        }
    }
    
    public void OnTriggerExit (Collider other)
    {
        if (other.tag == "Player")
        {
            defaultCam.Priority++;
            if (farCamBool == true)
            {
                farBossRoomCam.Priority--;
            }
            if (midCamBool == true)
            {
                midBossRoomCam.Priority--;
            }
            if (nearCamBool == true)
            {
                nearBossRoomCam.Priority--;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}
