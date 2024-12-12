using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineGun : MonoBehaviour
{
    //Variables to set
    public float launchSpeed = 40.0f;
    public float maxEnergy = 100f;
    public float fireCost = 25f; //per second of firing 
    public float chargeSpeed = 10f;
    public GameObject amo;
    public HealthBar energyBar;
    public float spawnRate = 0.05f;

    public float energy = 100f;
    private bool isFiring = false;
    private float spawnCooldown = 0f;
    private Coroutine recharge;

    private void Start()
    {
        //set the energy bar to its max value
        energyBar.SetMaxHealth(maxEnergy);
    }

    // Update is called once per frame
    void Update()
    {
        //if space is held pressed
        if (Input.GetKey("space") && energy>0)
        {
            if(!isFiring)
            {
                StartFiring();
            }

            //reduce energy over time and update the energy bar
            energy -= fireCost * Time.deltaTime;
            energyBar.SetHealth(energy);
            if (energy <= 0)
            {
                energy = 0;
                energyBar.SetHealth(energy);
                StopFiring();
            }

            //spawn projectile at a fix rate
            spawnCooldown -= Time.deltaTime;
            if(spawnCooldown <= 0 && energy > 0)
            {
                SpawnObject();
                spawnCooldown = spawnRate;
            }
        }
        else
        {
            if(isFiring)
            {
                StopFiring();
            }
        }

        //recharge energy when not firing
        if(energy < maxEnergy && !isFiring)
        {
            if(recharge == null)
            {
                recharge = StartCoroutine(RechargeEnergy());
            }
        }
    }

    //set firing to true
    void StartFiring()
    {
        isFiring = true;
    }

    //set firing to false and start the recharge coroutine
    void StopFiring()
    {
        isFiring = false;
        if(recharge == null)
        {
            recharge = StartCoroutine(RechargeEnergy());
        }
    }

    //spawn the ammo
    void SpawnObject()
    {
        //calculate the spawn position
        Vector3 spawnPosition = transform.position;

        //calculate the spawn rotation
        Quaternion spawnRotation = Quaternion.identity;

        //calculate the forward vector
        Vector3 forward = -transform.forward;

        //calculate velocity
        Vector3 velocity = forward * launchSpeed;

        //instantiate the ammo and apply the velocity
        GameObject newObject = Instantiate(amo, spawnPosition, spawnRotation);
        Rigidbody rb = newObject.GetComponent<Rigidbody>();
        rb.velocity = velocity;
    }

    //coroutine to recharge ennergy
    private IEnumerator RechargeEnergy()
    {
        yield return new WaitForSeconds(1f);
        while(energy < maxEnergy)
        {
            energy += chargeSpeed * Time.deltaTime;

            if (energy > maxEnergy)
            {
                energy = maxEnergy;
                
            }
            energyBar.SetHealth(energy);
            yield return null;
        }
        recharge = null;
    }
}
