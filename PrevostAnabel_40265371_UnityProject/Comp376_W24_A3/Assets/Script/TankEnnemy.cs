using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TankEnnemy : MonoBehaviour
{
    public float fireInterval = 2.0f;
    public float launchForce = 30.0f;
    public float archHeight = 2.0f;
    public float detectionRange = 15.0f;
    public float rotationSpeed = 2f;
    public GameObject mainCanonAmo;
    public GameObject tankBody;
    private Transform target;


    // Start is called before the first frame update
    void Start()
    {
        //find the target in the scene
        target = GameObject.Find("Player").transform;

        //fire every given interval
        InvokeRepeating("FireAmo", 0f, fireInterval);
    }

    private void Update()
    {
        //calculate the distance between the player and the enemy tank
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if(distanceToPlayer <= detectionRange)
        {
            RotateTowardsPlayer();
        }
    }

    private void RotateTowardsPlayer()
    {
        //calculate direction to the player
        Vector3 directionToPlayer = target.position - transform.position;

        //calculate a rotation to look at the player
        Quaternion targetRotation = Quaternion.LookRotation(-directionToPlayer);

        //rotate the tank towards the player
        tankBody.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void FireAmo()
    {
        //instantiate the amo
        GameObject projectile = Instantiate(mainCanonAmo, transform.position, Quaternion.identity);

        //calculate the arc and the direction
        Vector3 direction = -transform.forward;

        //modify the y direction
        direction.y += archHeight;

        //normalize the vector
        direction = direction.normalized;

        //apply the force 
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.Log(direction * launchForce);
            rb.velocity = direction * launchForce;
        }
    }
}
