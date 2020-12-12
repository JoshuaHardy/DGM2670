
using System.Collections;
using UnityEngine;
public class PrintMyLocation : MonoBehaviour
{
    public Vector3 location;
    public GameObject thisObject;
    public string printThis;
    public bool isPrinting = false;
    public Vector3Data pushedVector3;

    void Start()
    {
        location = gameObject.transform.position;
        pushedVector3.value = location;
        //StartCoroutine(PrintRepeater());


    }
    /*public IEnumerator PrintRepeater()
    {
        while (isPrinting)
        {
            
            Debug.Log("Gameobject"+ name + " is located at scene coordinates: " + gameObject.transform.position);
            yield return new WaitForSeconds(5.0f);
        }
    }*/
    void Update()
    {
        
    }
}
