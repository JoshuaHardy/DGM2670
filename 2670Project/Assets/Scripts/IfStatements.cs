using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfStatements : MonoBehaviour
{
    public bool canRun;
    public string password;
    public int number;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World.");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canRun)
        {
            Debug.Log("Running");
        }

        if (password == "OU812")
        {
            Debug.Log("Correct");
        }
        else
        {
            Debug.Log("incorrect");
        }
    }
}
