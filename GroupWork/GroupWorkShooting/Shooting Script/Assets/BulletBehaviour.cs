using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody rBody;
    public float bulletForce;
    public float lifeTime;

    private IEnumerator Start()
    {
        rBody = GetComponent<Rigidbody>();
        
        rBody.AddRelativeForce(Vector3.forward * bulletForce);
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    
}
