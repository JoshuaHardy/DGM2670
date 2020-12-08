using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDespawn : MonoBehaviour
{
    public float timer;
    public float lifespan = 3;
  
    void Start()
    {
    }

    private void Update()
    {
        timer += 1.0F * Time.deltaTime;

        if (timer >= lifespan)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
