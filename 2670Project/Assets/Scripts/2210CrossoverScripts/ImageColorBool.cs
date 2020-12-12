using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorBool : MonoBehaviour
{
    public BoolData toggleBool;
    public Image image;
    public Color colorOn, colorOff;
    

    public float boolOnAlpha = 1.0f, boolOffAlpha = 0.0f, crossfadeTimeOn = 0.5f, crossfadeTimeOff = 0.5f; 
  
    //Formatting   image.CrossFadeColor(targetColor,floatDuration, bool ignoreTimeScale,bool useAlpha, bool UsRGB)
               //image.CrossFadeColor(targetColor,floatDuration, bool ignoreTimeScale,bool useAlpha, bool UsRGB)

    void Update()
    {
        if (toggleBool.value)
        {
            image.CrossFadeColor(colorOn, crossfadeTimeOn,false, false, true);
        }
        
        if (!toggleBool.value)
        {
            image.CrossFadeColor(colorOff, crossfadeTimeOff,false, false, true);
        }
    }
}
