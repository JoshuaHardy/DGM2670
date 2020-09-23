using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class EnemyConfig : MonoBehaviour
{
    private EnemyHealth eHealth;
    // Start is called before the first frame update
    void Start()
    {
        //eHealth = ScripatableObject.CreateInstamce<EnemyHealth>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //eHealth.value -= 0.1f;
    }
}
        
