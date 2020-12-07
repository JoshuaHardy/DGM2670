using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ImageFillEnemyHealth : MonoBehaviour
{
    public Image fillImage;
    //[Range(0.000f, 1.0f)] 
    public float healthFromAI;
    //private AIEnemy01 inheritHealth;
    private AIEnemy01 parentHealth;
    
    void Awake()
    {
        parentHealth = GetComponent<AIEnemy01>();
        parentHealth.enemyHealth = healthFromAI;
        
        //inheritHealth = GetComponent<AIEnemy01>();
        //healthFromAI= inheritHealth.enemyHealth;
    }

    void Update()
    {
        fillImage.fillAmount = healthFromAI;
    }
}