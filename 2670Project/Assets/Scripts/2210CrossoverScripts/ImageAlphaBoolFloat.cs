using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAlphaBoolFloat : MonoBehaviour
{
    public BoolData toggleBool;
    public Image image;
    
    public float boolOnAlpha = 1.0f, boolOffAlpha = 0.0f, crossfadeTimeOn = 0.5f, crossfadeTimeOff = 0.5f;
    public FloatData floatCheck, floatThreshold;
    
    // Start is called before the first frame update
    void Start()
    {
        /*if (image = null)
        {
            GetComponent<Image>();
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if ((toggleBool.value) && (floatCheck.value >= floatThreshold.value))
        {
            image.CrossFadeAlpha(boolOnAlpha,crossfadeTimeOn, false);
        }
        
        if ((!toggleBool.value) && (floatCheck.value < floatThreshold.value))
        {
            image.CrossFadeAlpha(boolOffAlpha,crossfadeTimeOff, false);
        }
    }
}
