using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletReactionShrinkOrReplace : MonoBehaviour
{
    public bool canHit, weakToLargeBlue, weakToLargeRed, weakToSmallBlue, weakToSmallRed,  shrinkObject, newObject;
    public float invulnTime = 3.0f;
    [Range(0.01f, 0.99f)] public float shrinkRateLarge, shrinkRateSmall;
    public GameObject gameObjectNew;

    public int shrinkHitMax, smallShrinkCount, largeShrinkCount, hitCoolDown;
    private (float, float, float) shrinkRateLargeV3;
    //private Vector3 shrinkRateSmallV3;

    private int shrinkHitCurrent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        canHit = false;
        shrinkHitCurrent = 0;
        //shrinkRateLargeV3 == (transform.localScale.x * shrinkRateLarge,shrinkRateLarge,shrinkRateLarge)
        StartCoroutine(HitEnable());
    }

    public void OnTriggerEnter (Collider other)
    {
        if ((other.tag == "BlueBulletLarge")&&(weakToLargeBlue)&&(canHit))
        {
            canHit = !canHit;
            StartCoroutine(HitEnable());
            if (shrinkObject)
            {
                transform.localScale = new Vector3(transform.localScale.x * shrinkRateLarge,
                    transform.localScale.y * shrinkRateLarge,transform.localScale.z * shrinkRateLarge);
                shrinkHitCurrent += largeShrinkCount;
                if (shrinkHitCurrent >= shrinkHitMax)
                {
                    gameObject.SetActive(false);
                }
            }
            if (newObject)
            {
                //objects should be siblings no parent/child relationships
                gameObject.SetActive(false);
                gameObjectNew.SetActive(true);
            }
        }
        
        if ((other.tag == "RedBulletLarge")&&(weakToLargeRed)&&(canHit))
        {
            canHit = !canHit;
            StartCoroutine(HitEnable());
            if (shrinkObject)
            {
                transform.localScale = new Vector3(transform.localScale.x * shrinkRateLarge,
                    transform.localScale.y * shrinkRateLarge,transform.localScale.z * shrinkRateLarge);
                shrinkHitCurrent += largeShrinkCount;
                if (shrinkHitCurrent >= shrinkHitMax)
                {
                    gameObject.SetActive(false);
                }
            }
            if (newObject)
            {
                //objects should be siblings no parent/child relationships
                gameObject.SetActive(false);
                gameObjectNew.SetActive(true);
            }
        }
        
        if ((other.tag == "BlueBulletSmall")&&(weakToSmallBlue)&&(canHit))
        {
            canHit = !canHit;
            StartCoroutine(HitEnable());
            if (shrinkObject)
            {
                transform.localScale = new Vector3(transform.localScale.x * shrinkRateSmall,
                    transform.localScale.y * shrinkRateSmall,transform.localScale.z * shrinkRateSmall);
                shrinkHitCurrent += smallShrinkCount;
                if (shrinkHitCurrent >= shrinkHitMax)
                {
                    gameObject.SetActive(false);
                }
            }
            if (newObject)
            {
                //objects should be siblings no parent/child relationships
                gameObject.SetActive(false);
                gameObjectNew.SetActive(true);
            }
        }
        
        if ((other.tag == "RedBulletSmall")&&(weakToSmallRed)&&(canHit))
        {
            canHit = !canHit;
            StartCoroutine(HitEnable());
            if (shrinkObject)
            {
                transform.localScale = new Vector3(transform.localScale.x * shrinkRateSmall,
                    transform.localScale.y * shrinkRateSmall,transform.localScale.z * shrinkRateSmall);
                shrinkHitCurrent += smallShrinkCount;
                if (shrinkHitCurrent >= shrinkHitMax)
                {
                    gameObject.SetActive(false);
                }
            }
            if (newObject)
            {
                //objects should be siblings no parent/child relationships
                gameObject.SetActive(false);
                gameObjectNew.SetActive(true);
            }
        }
    }
 
    IEnumerator HitEnable()
    {
        yield return new WaitForSeconds(hitCoolDown);
        canHit = true;
    }
}
