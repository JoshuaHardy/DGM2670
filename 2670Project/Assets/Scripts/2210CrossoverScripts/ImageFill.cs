using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFill : MonoBehaviour
{
    public Image fillImage;
    //[Range(0.000f, 1.0f)] 
    public FloatData fillFloat;
    
    void Start()
    {
        
    }

    void Update()
    {
        fillImage.fillAmount = fillFloat.value;
    }
}
