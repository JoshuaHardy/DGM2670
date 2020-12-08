using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwitchVolume : MonoBehaviour
{
    public string switchSceneTo;
    void OnTriggerEnter()
    {
        //Debug.Log ("Loading "switchSceneTo," Scene");
        SceneManager.LoadScene(switchSceneTo);
    }
}
