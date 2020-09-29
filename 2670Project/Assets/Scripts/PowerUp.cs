using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PowerUp : MonoBehaviour
{
    public float value;

    public void UpdateValue(float number)
    {
        value += number;
    }

}
