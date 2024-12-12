using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missiles : MonoBehaviour
{
    //Variables to set
    public float launchSpeed = 40.0f;
    public float launchAngle = 45.0f;
    public int missilesThrown = 4;
    public float automaticDelayBetweenShots = 0.3f;
    public float playerDelayBetweenShots = 0f;

    public GameObject amo;
    public GameObject readyStateDisplayAmo;
    public GameObject myTank;
    public TrajectoryLine trajectoryLine;

    private bool canFire = true;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    private Vector3 forward;
    private Vector3 launchDirection;
    private Vector3 projectileVelocityRelativeToTank;
    private Vector3 tankVelocity;
    private Vector3 totalVelocity;

    // Update is called once per frame
    void Update()
    {
        //get out forward vector
        forward = -transform.forward;

        //calculate the launch direction
        launchDirection = Quaternion.Euler(-launchAngle, 0, 0) * forward;

        //Calculate the velocity of the projectile
        projectileVelocityRelativeToTank = launchDirection * launchSpeed;

        //get the velocity of the tank
        tankVelocity = myTank.GetComponent<Rigidbody>().velocity;

        //Total velocity
        totalVelocity = projectileVelocityRelativeToTank + tankVelocity;

        //show the target
        trajectoryLine.ShowTrajectoryLine(transform.position, totalVelocity);

        //shoot 4 missiles when right button is clicked
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(SpawnObject());
        }
    }

    //coroutine that spawns 4 missilies one after the other
    IEnumerator SpawnObject()
    {
        if (canFire)
        {
            for (int i = 0; i < missilesThrown; i++)
            {
                //spawn the bullet
                spawnPosition = transform.position;
                spawnRotation = Quaternion.identity;
                GameObject newObject = Instantiate(amo, spawnPosition, spawnRotation);

                //apply velocity
                Rigidbody rb = newObject.GetComponent<Rigidbody>();
                rb.velocity = totalVelocity;

                canFire = false;
                readyStateDisplayAmo.SetActive(false);
                yield return new WaitForSeconds(automaticDelayBetweenShots);
            }

            //start the coroutine to give the player cooldown
            StartCoroutine(FireTimer());
        }
    }

    //cooldown coroutine
    IEnumerator FireTimer()
    {
        yield return new WaitForSeconds(playerDelayBetweenShots);
        canFire = true;
        readyStateDisplayAmo.SetActive(true);
    }
}

