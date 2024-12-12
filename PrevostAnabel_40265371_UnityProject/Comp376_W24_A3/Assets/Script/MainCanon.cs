using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainCanon : MonoBehaviour
{
    //Variables to set
    public float launchSpeed = 40.0f;
    public float launchAngle = 5f;
    public float delayBetweenShots = 0f;
    public GameObject amo;
    public GameObject readyStateDisplayAmo;
    public CameraShake cameraShake;
    public GameObject myTank;
    public TrajectoryLine trajectoryLine;
    public bool inCutScene = false;

    private bool canFire = true;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    private Vector3 forward;
    private Vector3 launchDirection;

    //velocity
    private Vector3 totalProjectileVelocity;
    private Vector3 projectileVelocityRelativeToTank;
    private Vector3 myTankVelocity;

    // Update is called once per frame
    void Update()
    {
        //get the velocity of the tank
        myTankVelocity = myTank.gameObject.GetComponent<Rigidbody>().velocity;
        
        //get our forward vector
        forward = -transform.forward;

        //calculate the launch direction
        launchDirection = Quaternion.Euler(-launchAngle, 0, 0) * forward;

        //calculate the velocity of the projectile
        projectileVelocityRelativeToTank = launchDirection * launchSpeed;

        //calculate total velocity of the projectile
        totalProjectileVelocity = myTankVelocity + projectileVelocityRelativeToTank;

        //show the target
        trajectoryLine.ShowTrajectoryLine(transform.position, totalProjectileVelocity);
        
        //when the left mouse button is pressed a projectile is shot
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && inCutScene == false)
        {
            SpawnObject();
        }
    }

    //spawn a projectile
    void SpawnObject()
    {
        if (canFire)
        {
            //Instantiate the bullet
            spawnPosition = transform.position;
            spawnRotation = Quaternion.identity;
            GameObject newObject = Instantiate(amo, spawnPosition, spawnRotation);

            //apply velocity
            Rigidbody rb = newObject.GetComponent<Rigidbody>();
            rb.velocity = totalProjectileVelocity;

            //shake the camera
            StartCoroutine(cameraShake.Shake(0.2f, 0.25f));

            canFire = false;
            readyStateDisplayAmo.SetActive(false);

            //wait a certain time to shoot another bullet
            StartCoroutine(FireTimer());
        }
    }

    //wait before letting the player fire
    IEnumerator FireTimer()
    {
        yield return new WaitForSeconds(delayBetweenShots);
        canFire = true;
        readyStateDisplayAmo.SetActive(true);
    }
}
