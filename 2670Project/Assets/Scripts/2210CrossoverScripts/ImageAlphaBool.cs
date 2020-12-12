using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAlphaBool : MonoBehaviour
{
    
    public Image image;
    public BoolData toggleBool;
    public float  crossfadeTimeOn = 0.5f, crossfadeTimeOff = 0.5f; 
    [Range(0,1)] public float boolOnAlpha = 1, boolOffAlpha = 0;
    
    void Update()
    {
        if (toggleBool.value)
        {
            image.CrossFadeAlpha(boolOnAlpha, crossfadeTimeOn,false);
        }
        
        if (!toggleBool.value)
        {
            image.CrossFadeAlpha(boolOffAlpha, crossfadeTimeOff,false);
        }
    }
}
