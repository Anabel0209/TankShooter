using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : MonoBehaviour
{
    public float fireInterval = 2.0f;
    public float launchForce = 30.0f;
    public float archHeight = 2.0f;
    public GameObject mortarAmo;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        //find the target in the scene
        target = GameObject.Find("Player").transform;

        //fire every interval of time
        InvokeRepeating("FireAmo", 0f, fireInterval);
    }

    private void FireAmo()
    {
        //instantiate a projectile
        GameObject projectile = Instantiate(mortarAmo, transform.position, Quaternion.identity);

        //calculate the arc and the direction
        Vector3 direction = (target.position - transform.position).normalized;

        //add an arc to the shot
        direction.y += archHeight;

        //normalize the direction
        direction = direction.normalized;

        //apply the force 
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.velocity = direction * launchForce;
        }
    }
}
