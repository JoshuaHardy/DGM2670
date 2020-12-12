using System.Collections;
using UnityEngine;
public class TeleportPad : MonoBehaviour
{
    public Vector3Data teleporterHome, teleporterReturn;
    public GameObject playerBlue, playerRed;
    public float teleportDelaySeconds = 5f;
    public Vector3 teleportTarget,thisLocation, homeLocation;
    public bool abortTeleport = false;
    private Vector3 offsetVector3, blueDestination, redDestination;
    public int playersOnPad = 0;
    public BoolData teleporterRecharging;
    public ParticleSystem psActive, psSlow, psGo;
    void Start()
    {
        thisLocation = transform.position;
        teleportTarget = teleporterHome.value;
        teleporterReturn.value = teleporterHome.value;
        psSlow.Play();
        teleporterRecharging.value = false;
    }
    public void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            psActive.Play();
            if (playersOnPad < 0)
            {
                playersOnPad = 0;
            }
            playersOnPad++;
            abortTeleport = false;
            StartCoroutine(TeleportSequence());
            if (teleportTarget != teleporterHome.value);
            {
                teleportTarget = thisLocation;
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playersOnPad--;
            if ((playersOnPad < 0))
            {
                playersOnPad = 0;
            }
            if ((playersOnPad != 2))
            {
                abortTeleport = true;
            }
            psActive.Play();
            psSlow.Stop();
            psGo.Stop();
            StopCoroutine(TeleportSequence());
        }
    }
    IEnumerator TeleportSequence()
    {
        if (thisLocation != teleporterHome.value)
        {
            teleporterReturn.value = thisLocation;
            teleportTarget = teleporterHome.value;
        }
        
        if (thisLocation == teleporterHome.value)
        {
            teleportTarget = teleporterReturn.value;
        }
        
        if (teleportTarget == thisLocation)
        {
            abortTeleport = true;
        }
        offsetVector3 = new Vector3(2, 1.5f, 0);
       blueDestination = new Vector3(teleportTarget.x - offsetVector3.x, teleportTarget.y + + offsetVector3.y,
            teleportTarget.z);
        redDestination = new Vector3(teleportTarget.x + offsetVector3.x, teleportTarget.y + offsetVector3.y,
            teleportTarget.z);

        if ((!abortTeleport) && (playersOnPad == 2) && (!teleporterRecharging.value))
        {
            teleporterRecharging.value = true;
            psGo.Play();
            yield return new WaitForSeconds(teleportDelaySeconds);
            playerBlue.transform.position = blueDestination;
            playerRed.transform.position = redDestination;
            playersOnPad = 0;
            psGo.Stop();
            psActive.Stop();
            psSlow.Play();
            yield return new WaitForSeconds(teleportDelaySeconds);
            teleporterRecharging.value = false;
        }
    }
}
