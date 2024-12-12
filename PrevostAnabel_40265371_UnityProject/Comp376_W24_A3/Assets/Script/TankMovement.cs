using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;


public class TankMovement : MonoBehaviour
{
    public float maxSpeed = 5.0f;
    public float rotationSpeed = 120.0f;
    public float acceleration = 5f;
    public float deceleration = 5f;
    public float strafeSpeed = 3f;
    float strafeInput = 0f;

    private float moveInput;
    private float rotationInput;

    private float currentSpeed;

    private Rigidbody rb;
    public bool isMoving = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //maxSpeed = -maxSpeed;
        currentSpeed = -currentSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        strafeInput = 0f;
        moveInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.E))
        {
            strafeInput = -1f;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            strafeInput = 1f;
        }
        if(moveInput == 0 && rotationInput == 0 && strafeInput == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

    }

    private void FixedUpdate()
    {
        MoveTank(moveInput, strafeInput);
        RotateTank(rotationInput);
    }
    void MoveTank(float moveInput, float strafeInput)
    {
        Vector3 strafe = transform.right * strafeInput * strafeSpeed * Time.deltaTime;
        if (moveInput != 0)
        {
            currentSpeed += moveInput * -acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
            
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
        }
        Vector3 moveDirection = transform.forward * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + moveDirection + strafe);
    }

    void RotateTank(float input)
    {
        float rotation = input * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    
}
