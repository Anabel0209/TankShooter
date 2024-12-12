using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    //values that can be adjusted
    public int maxhealthPoint = 20;
    public int currentHealthPoints = 10;
    public float timeBetweenDamage = 2.0f;
    public int damageMod = 1;
    public GameObject deathScreen;
    public HealthBar myHealthBar;

    //private values
    float damageTimer = 0f;
    private bool inGas = false;
    private bool inTerretRange = false;


    // Start is called before the first frame update
    void Start()
    {
        //set the health to maximum at the begining of the game
        currentHealthPoints = maxhealthPoint;
        myHealthBar.SetMaxHealth(maxhealthPoint);
    }

    // Update is called once per frame
    void Update()
    {
        //if the player is in the gas cloud, it takes damage over time
        if (inGas)
        {
            //update the timer
            damageTimer += Time.deltaTime;

            //if the amount of time set as passed
            if (damageTimer >= timeBetweenDamage)
            {
                //take damage and reset timer
                TakeDamage(1);
                damageTimer = 0f;
            }
        }
    }

    //Manage collision with triggers
    private void OnTriggerEnter(Collider collision)
    {
        //if enter the gas cloud
        if(collision.CompareTag("gasCloud") && inGas == false)
        {
            TakeDamage(2);
            inGas = true;
        }

        //if enter the range of a turret
        if (collision.CompareTag("TerretRange") && inTerretRange == false)
        {
            //the damage delt to the player in the range of a turret will be doubled
            damageMod = 2;
            inTerretRange = true;
        }
    }

    //Manage exit of trigger zone
    private void OnTriggerExit(Collider collision)
    {
        //if exit the gas cloud
        if (collision.CompareTag("gasCloud") )
        {
            inGas = false;
        }

        //if exit the terret range
        if (collision.CompareTag("TerretRange"))
        {
            //the damage delt to the player comes back to normal
            damageMod = 1;
            inTerretRange = false;
        }
    }

    //Method called when the player takes damage
    private void TakeDamage(int damage)
    {
        //if the current health is more than 0
        if(currentHealthPoints > 0)
        {
            //if the current health minus the damage added is smaller than 0 block the current health to 0
            if((currentHealthPoints - damage * damageMod) < 0)
            {
                currentHealthPoints = 0;
            }
            //else apply the damage
            else
            {
                currentHealthPoints -= damage * damageMod;
            }
        }
        //update the health bar
        myHealthBar.SetHealth(currentHealthPoints);

        //if the current health is 0, the player is dead
        if (currentHealthPoints == 0)
        {
            deathScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void GainHealth(int gain)
    {
        //if the current health is lower than the maximum
        if(currentHealthPoints < maxhealthPoint)
        {
            //if the current healt plus the added one is bigger than the max health
            if(currentHealthPoints + maxhealthPoint > maxhealthPoint)
            {
                currentHealthPoints = maxhealthPoint;
            }
            //else apply the gain
            else
            {
                currentHealthPoints += gain;
            }
        }

        //update the health bar
        myHealthBar.SetHealth(currentHealthPoints);
    }

    //Manage collisions that affect health
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("TankTrap"))
        {
            TakeDamage(2);
        }
        if (collision.gameObject.CompareTag("Mortar"))
        {
            TakeDamage(5);
        }
        if (collision.gameObject.CompareTag("EnnemyTank"))
        {
            TakeDamage(5);
        }
        if (collision.gameObject.CompareTag("healthPack"))
        {
            GainHealth(2);
            Destroy(collision.gameObject);
        }
    }
}
