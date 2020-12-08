﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwitch : MonoBehaviour
{
    public string switchSceneTo;
    void StartScene()
    {
        //Debug.Log ("Loading "switchSceneTo," Scene");
        SceneManager.LoadScene(switchSceneTo);
    }
}
