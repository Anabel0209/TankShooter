using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyHealth : MonoBehaviour
{
    //variables to be set
    public int maxHealthPoints = 5;
    public bool mortar, tank, terret, tankTrap;
    private int currentHealthPoint;
    public HealthBar healthBar;
   
    //When an ennemy is destroyed it updates the score
    private ScoreManagement scoreManagement;

    // Start is called before the first frame update
    void Start()
    {
        //find the Score Class dynamically
        GameObject player = GameObject.Find("Player");
        scoreManagement = player.GetComponent<ScoreManagement>();

        //set the initial health bar
        healthBar.SetMaxHealth(maxHealthPoints);

        currentHealthPoint = maxHealthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        //update the Score with the appropriate amount of points when an ennemy die and delets the object
        if (currentHealthPoint == 0)
        {
            Destroy(gameObject);

            if(mortar)
            {
                scoreManagement.UpdateScore(40);
            }
            else if(tank)
            {
                scoreManagement.UpdateScore(20);
            }
            else if(terret)
            {
                scoreManagement.UpdateScore(60);
            }
            else if (tankTrap)
            {
                scoreManagement.UpdateScore(10);
            }
        }
    }

    //Method called when the ennemy takes damage
    private void TakeDamage(int damage)
    {
        //if the amount of health is bigger than 0
        if (currentHealthPoint > 0)
        {
            //if the current health points minus the damage to be taken is smaller than 0 cap the current health points to 0
            if(currentHealthPoint - damage < 0)
            {
                currentHealthPoint = 0;
            }
            //else reduce the health
            else
            {
                currentHealthPoint = currentHealthPoint - damage;
            }

            Debug.Log("maxHealth: " + maxHealthPoints + "/ current: " + currentHealthPoint);
        }

        //update the health bar
        healthBar.SetHealth(currentHealthPoint);
    }

    //Update the health points of the ennemy when a projectile collide with it
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("MainCanon"))
        {
            TakeDamage(10);
        }
        else if(collision.gameObject.CompareTag("Missiles"))
        {
            TakeDamage(4);
        }
        else if(collision.gameObject.CompareTag("MachineGun"))
        {
            TakeDamage(1);
        }
    }
}
