using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class AIEasyEnemyRed : MonoBehaviour
{
    public Transform bluePlayer, redPlayer, target;
    int MoveSpeed = 5;
    int MaxDist = 6;
    int MinDist = 3;

    public float enemyHealth = 1.0f, engageRange = 10, invulnTime = 1.0f, agroRed = 1.5f, agroBlue = 1.0f;
    
    private float maxHealth = 1.0f, minHealth = 0.0f;
    //private bool isHit;
    public Image fillImage;
    public bool canBeHit = true;
    private float blueDist, redDist, targetDist;
    
    void Awake()
    {
        enemyHealth = maxHealth;
        //fillImage = GetComponentInChildren()<Image>();
    }

    private void Start()
    {
        
    }

    void Update()
    {
        //enemy HealthBar
        fillImage.fillAmount = enemyHealth;
        
        //Enemy Targeting Behavior Favoring Red
        blueDist = Vector3.Distance(transform.position, bluePlayer.transform.position);
        redDist = Vector3.Distance(transform.position, redPlayer.transform.position);

        if (((blueDist * agroRed) < (redDist * agroBlue)) && ((blueDist) < (engageRange * agroBlue)))
        {
            target = bluePlayer;
        }
        if ((redDist * agroBlue) < (blueDist* agroRed) && ((redDist) < (engageRange * agroRed)))
        {
            target = redPlayer;
        }
        
        transform.LookAt(target);
        targetDist = (Vector3.Distance(transform.position, target.position));
        
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
    
    //bulletVulnerability takes less from blue
    void OnTriggerEnter(Collider other)
        {
            if ((other.gameObject.tag == "RedBulletSmall") && (canBeHit))
            {
                enemyHealth += -.25f ;
                //isHit = true;
                print("Bullet Hit Registered");
                StartCoroutine(CantHit());
            }
            
            if ((other.gameObject.tag == "BlueBulletSmall") && (canBeHit))
            {
                enemyHealth += -0.05f;
                //isHit = true;
                StartCoroutine(CantHit());
                print("Blue Bullet Hit Registered");
            }
            
            if ((other.gameObject.tag == "RedBulletLarge") && (canBeHit))
            {
                enemyHealth += -0.5f;
                //isHit = true;
                print("RedBulletLarge Hit Registered");
                StartCoroutine(CantHit());
            }
            
            if ((other.gameObject.tag == "BlueBulletLarge") && (canBeHit))
            {
                enemyHealth += -0.1f;
                //isHit = true;
                StartCoroutine(CantHit());
                print("BlueBulletLarge Hit Registered");
            }
        }

    IEnumerator CantHit()
    {
        canBeHit = false;
        new WaitForSeconds(invulnTime);
        canBeHit = true;
        yield break;
    }
}
