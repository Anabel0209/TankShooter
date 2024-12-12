using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasCloudFollowing : MonoBehaviour
{
    public GameObject target;
    public TankMovement tankMov;
    public float speed = 1.0f;
    public float speedBoostWhenTargetStopped= 0.2f;
    private Vector3 targetDirection;
    public GameObject dangerIcon;
    public float distanceTooClose = 10f;

    // Update is called once per frame
    void Update()
    {
        //display image if the player is too close to the cloud
        AlertCloudTooClose();
        float currentSpeed = tankMov.isMoving ? speed : speed + speedBoostWhenTargetStopped;

        if(tankMov.isMoving)
        {

            targetDirection = Vector3.MoveTowards(transform.position, target.transform.position, currentSpeed * Time.deltaTime);
            
        }
        else
        {
            targetDirection = Vector3.MoveTowards(transform.position, target.transform.position, currentSpeed * Time.deltaTime);

        }
        transform.position = targetDirection;
    }

    //checks if the player is too close to the cloud and if so display a danger image
    private void AlertCloudTooClose()
    {
        //calculate the distance between the player and the cloud
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if(distance < distanceTooClose)
        {
            dangerIcon.SetActive(true);
        }
        else
        {
            dangerIcon.SetActive(false);
        }
    }
}
