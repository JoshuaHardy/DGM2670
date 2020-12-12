using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageTest : MonoBehaviour
{
    public float invulnTime = 1.0f;
    public FloatData bossEnemyHealth;
    public bool canBeHit;
    // Start is called before the first frame update
    void Start()
    {
        bossEnemyHealth.value = 1;
        GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossEnemyHealth.value <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "RedBulletSmall") && (canBeHit))
        {
            bossEnemyHealth.value += -.01f ;
            //isHit = true;
            print("RedBulletSmall Hit Registered");
            StartCoroutine(CantHit());
        }
            
        if ((other.gameObject.tag == "BlueBulletSmall") && (canBeHit))
        {
            bossEnemyHealth.value += -0.01f;
            //isHit = true;
            StartCoroutine(CantHit());
            print("BlueBulletSmall Hit Registered");
        }
            
        if ((other.gameObject.tag == "RedBulletLarge") && (canBeHit))
        {
            bossEnemyHealth.value += -0.05f;
            //isHit = true;
            print("RedBulletLarge Hit Registered");
            StartCoroutine(CantHit());
        }
            
        if ((other.gameObject.tag == "BlueBulletLarge") && (canBeHit))
        {
            bossEnemyHealth.value += -0.05f;
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
