using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform cameraLocation;
    //public GameObject camera;
 
    // Use this for initialization
    void Start ()
    {
        cameraLocation = (Camera.main.transform);
    }
 
    // Update is called once per frame
    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        transform.LookAt(cameraLocation);
    }
}
