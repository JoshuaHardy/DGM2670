using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFillTest : MonoBehaviour
{
    public Image FillImage;
    public bool coolingDown;
    public float waitTime = 5.0f;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (coolingDown == true)
        {
            //Reduce fill amount over 30 seconds
            FillImage.fillAmount -= 1.0f / waitTime * Time.deltaTime;
        }
    }
}
