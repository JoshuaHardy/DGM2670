using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeTriggerGameObject : MonoBehaviour
{
    private int playersInVolume = 0;

    public GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            playersInVolume++;
            if (playersInVolume < 0)
            {
                playersInVolume = 0;
            }
            if (playersInVolume > 2)
            {
                playersInVolume = 2;
            }
            if (playersInVolume == 2)
            {
                gameObject.SetActive(true);
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playersInVolume--;
            if (playersInVolume < 0)
            {
                playersInVolume = 0;
            }
            if (playersInVolume > 2)
            {
                playersInVolume = 2;
            }
            if ((playersInVolume != 2))
            {
                gameObject.SetActive(false);
            }
        }
    }
    
    
}
