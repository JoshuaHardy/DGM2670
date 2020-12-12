using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerBlue, playerRed, respawnLocation;
    public FloatData playerHealth;
    private Vector3 offsetVector3, redDestination, blueDestination, destination;
    public IntData deathCount, runTimeSeconds, runTimeMinutes, runTimeHours;
    //fireballs shot, blue, red, blue charged, red charged, teleports used,
    //clear time, time spent sprinting, timeSpent Exhausted, time spent Idle, time spent in healing well,
    
    void Start()
    {
        /*playerHealth.value = 1.0f;
        runTimeSeconds.value = 00;
        runTimeMinutes.value = 00;
        runTimeHours.value = 00;
        StartCoroutine(gameClock());*/
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.value <= 0.0f)
        {
            blueDestination = respawnLocation.transform.position;
            StartCoroutine(Teleport());
            playerHealth.value = 0.25f;
            deathCount.value++;
        }
        if (playerHealth.value > 1.0f)
        {
            playerHealth.value = 1.0f;
        }
    }

    IEnumerator gameClock()
    {
        while (runTimeSeconds.value < 5)
        {
            runTimeSeconds.value++;
            yield return new WaitForSeconds(1);
        }

        if (runTimeSeconds.value == 60)
        {
            runTimeSeconds.value = 0;
            runTimeMinutes.value++;
        }
        if (runTimeMinutes.value == 60)
        {
            runTimeMinutes.value = 0;
            runTimeHours.value++;
        }
    }

    IEnumerator Teleport()
    {
        offsetVector3 = new Vector3(2, 3f, 0);
        blueDestination = new Vector3(destination.x - offsetVector3.x, destination.y + + offsetVector3.y,
            destination.z);
        redDestination = new Vector3(destination.x + offsetVector3.x, destination.y + offsetVector3.y,
            destination.z);
        playerBlue.transform.position = blueDestination;
        playerRed.transform.position = redDestination;
        
        yield break;
    }
    
}
