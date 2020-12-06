/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    private PlayerControls controls;
    public BoolData IsPaused;
    
    void Awake()
    {
        
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void PauseGame ()
    {
        if (!IsPaused)
        {
            print("Game Paused");
            Time.timeScale = 0f;
        IsPaused.value = true;
        }
        else
        {
            Time.timeScale = 1f;
            IsPaused.value = false;
        }
    }
}
*/
