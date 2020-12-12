using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followLikeDucks : MonoBehaviour
{
    public Transform followTarget;

    public FloatData speedData, minDist, maxDist;
    private float targetDist;
    public float speed;
    private Vector3 startPos, catchUp;

    
    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector3(followTarget.position.x, followTarget.position.y, followTarget.position.z - minDist.value);
        transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        speed = speedData.value;
        transform.LookAt(followTarget);
        targetDist = (Vector3.Distance(transform.position, followTarget.position));

        if ((targetDist >= minDist.value))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        if ((targetDist >= maxDist.value))
        {
            catchUp = new Vector3(followTarget.position.x, followTarget.position.y,
                followTarget.position.z - minDist.value);
            transform.position = catchUp;
        }
    }
}
