using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBlue : MonoBehaviour
{
    public GameObject fireball_S;
    public int projectileThrust = 1000;
    public Transform gun;
    public bool canFire;
    public int fireRate = 2;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && (canFire));
        {
            StartCoroutine (FireShot ());
        }
    }

    public void shootFireballBlueSmall()
    {
        var newBullet = Instantiate(fireball_S, gun.transform.position, gun.transform.rotation);
        newBullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(gun.transform.forward * projectileThrust);
    }

    IEnumerator FireShot()
    {
        canFire = false;
        shootFireballBlueSmall();
        yield return new WaitForSeconds(3);
        canFire = true;
    }
}
