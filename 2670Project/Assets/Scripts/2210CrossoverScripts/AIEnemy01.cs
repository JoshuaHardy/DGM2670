using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class AIEnemy01 : MonoBehaviour
{
    public Transform bluePlayer, redPlayer, target;
    int MoveSpeed = 5;
    int MaxDist = 6;
    int MinDist = 3;

    public float enemyHealth = 1.0f, engageRange = 10.0f;
    
    private float maxHealth = 1.0f, minHealth = 0.0f;
    //private bool isHit;
    public Image fillImage;
    private float blueDist, redDist, targetDist;
    
    void Awake()
    {
        enemyHealth = maxHealth;
    }

    private void Start()
    {
        
    }

    void Update()
    {
        //enemy HealthBar
        fillImage.fillAmount = enemyHealth;
        
        //Enemy Targeting Behavior
        blueDist = Vector3.Distance(transform.position, bluePlayer.transform.position);
        redDist = Vector3.Distance(transform.position, redPlayer.transform.position);

        if ((blueDist) < (redDist) && ((blueDist) < (engageRange)))
        {
            target = bluePlayer;
        }
        if ((redDist) < (blueDist) && ((redDist) < (engageRange)))
        {
            target = redPlayer;
        }
        
        
        
        
        //Enemy Engage Behavior
        if ((targetDist >= MinDist) && (targetDist <= engageRange))
        {
 
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
 
 
 
            if (Vector3.Distance(transform.position, target.position) <= MaxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }

            /*if (isHit)
            {
                GetComponentInChildren<>()
            }*/
        }
        if (enemyHealth <= minHealth)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
        {
            //if (other.gameObject.tag == "FriendlyBullet")
            {
                enemyHealth += -.25f;
                //isHit = true;
                print("Bullet Hit Registered");
            }
        }
        
    
}
