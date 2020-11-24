using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFill : MonoBehaviour
{
    public Image FillImage;
    //[Range(0.000f, 1.0f)] 
    public FloatData FillFloat;
    
    void Start()
    {
        
    }

    void Update()
    {
        FillImage.fillAmount = FillFloat.value;
    }
}
