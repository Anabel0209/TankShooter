using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateMap : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform bottomRightCorner;
    public Transform topLeftCorner;
    private float xPos;
    private float zPos;
    public Mortar mortarClass;
    public TankEnnemy enemyTankClass;
    public GasCloudFollowing myGasCloud;

    //private int towerCount;
    public int nbTankWanted = 15;
    public int nbMortarWanted = 15;
    public int nbTankTrapWanted= 15;
    public int nbHealthPackWanted = 15;
    public int nbBonusPointsWanted = 15;
    public int nbBonusAmmoRefill = 15;

    private float xPosMin;
    private float xPosMax;
    private float zPosMin;
    private float zPosMax;

    // Start is called before the first frame update
    void Start()
    {
        xPosMin = bottomRightCorner.position.x;
        xPosMax = topLeftCorner.position.x;
        zPosMin = topLeftCorner.position.z;
        zPosMax = bottomRightCorner.position.z;

        //generate TankTraps
        GenerateItems(nbTankTrapWanted, 2, Quaternion.identity, true);

        //generate mortars and towers
        GenerateSomethingAndTowers(nbMortarWanted, 1, 3);

        //generate tank and towers
        GenerateSomethingAndTowers(nbTankWanted, 0, 3);

        //generate health packs
        GenerateItems(nbHealthPackWanted, 4, Quaternion.Euler(90f, 0, 0), false);

        //generate bonus points
        GenerateItems(nbBonusPointsWanted, 5, Quaternion.Euler(90f, 0, 0), false);

        //generate ammoRefill
        GenerateItems(nbBonusAmmoRefill, 6, Quaternion.Euler(90f, 0, 0), false);

        //change difficulty modifiers every 15 seconds
        InvokeRepeating("DifficultyScale", 15f, 15f);
    }

    //method that generate simple items that can be spawned in clusters
    private void GenerateItems(int maxItemCount, int indexNb, Quaternion spawnAngle, bool cluster)
    {
        int itemCount = 0;

        while(itemCount < maxItemCount)
        {
            //Calculate random x and z position
            xPos = Random.Range(xPosMin, xPosMax);
            zPos = Random.Range(zPosMin, zPosMax);

            if(cluster == false)
            {
                //Spawn the object
                Instantiate(enemies[indexNb], new Vector3(xPos, 0.65f, zPos), spawnAngle);

                //increment the itemCount
                itemCount++;
            }
            else
            {
                //generate a random number thats gonna be the size of our cluster
                int randomNumber = Random.Range(2, 6);

                //repetitively spawn objects
                for (int i = 0; i < randomNumber; i++)
                {
                    Instantiate(enemies[indexNb], new Vector3(xPos + i, 0, zPos), spawnAngle);
                    itemCount++;
                }
            }
        }
    }

    //method that generate a certain item accompanied by terrets
    private void GenerateSomethingAndTowers(int maxStructureCount, int indexNb, int frequenceTowers)
    {
        int structureCount = 0; 

        while (structureCount < maxStructureCount)
        {
            //Calculate a random position
            xPos = Random.Range(xPosMin, xPosMax);
            zPos = Random.Range(zPosMin, zPosMax);

            //instantiate the ennemies
            Instantiate(enemies[indexNb], new Vector3(xPos, 0, zPos), Quaternion.identity);

            //update the count
            structureCount ++;

            //for every 4 tank, spawn a terret beside it
            if (structureCount % frequenceTowers == 0)
            {
                Instantiate(enemies[3], new Vector3(xPos + 5, 0, zPos + 5), Quaternion.identity);
            }
        }
    }

    //method that call a difficulty modifier every 15 seconds
    private void DifficultyScale()
    {
        int randomEventNumber = Random.Range(0, 5);

        //decrement mortar fireRate
        if(randomEventNumber == 0)
        {
            mortarClass.fireInterval -= 0.5f;
            Debug.Log("decremented mortar fire rate");
        }
        //decrement Enemy tank fire interval
        else if(randomEventNumber == 1)
        {
            enemyTankClass.fireInterval -= 0.5f;
            Debug.Log("decremented enemy fire rate");
        }
        //increment gas speed
        else if (randomEventNumber == 2)
        {
            myGasCloud.speed += 0.2f;
            Debug.Log("dincrement gas speed");
        }
        //spawn more obstacles
        else
        {
            //generate TankTraps
            GenerateItems(5, 2, Quaternion.identity, true);
            Debug.Log("spawned more traps");
        }
    }
}
