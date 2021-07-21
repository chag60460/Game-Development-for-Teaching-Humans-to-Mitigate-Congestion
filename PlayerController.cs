using System.Collections;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    //variables to move the vehicle forward
    public float currentSpeed;
    public float maxSpeed = 60.0f;

    public float forwardInput;

    //variables to move the vehicle sideways
    public float turnSpeed;
    public float horizontalInput;

    //Acceleration
    public float AccelerationMagnitude = 12.0f; // Assume constant for how, since we don't know how hard someone presses the gas pedal without deploying the game first

    // Start is called before the first frame update
    void Start()
    {
    }

    // FixedUpdate applies force to each fixed frame when Rigidbody is called
    void FixedUpdate()
    {
        // Acceleration
        if (Input.GetKey(KeyCode.UpArrow)) // need to reflect hold time, use GetKey
        {
            if (currentSpeed <= maxSpeed)
            {
                currentSpeed += Time.deltaTime * AccelerationMagnitude;
            }

            else if (currentSpeed >= maxSpeed)
            {
                currentSpeed = maxSpeed;
            }

            transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed * forwardInput);
        }

        // Deceleration
        else if (!Input.GetKey(KeyCode.UpArrow))
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= Time.deltaTime * AccelerationMagnitude;
            }

            else if (currentSpeed <= 0)
            {
                currentSpeed = 0;
            }
        }


        // Braking
        if (Input.GetKey(KeyCode.Space))
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= Time.deltaTime * AccelerationMagnitude;
            }

            else currentSpeed = 0;
        }


        // Move the vehicle sideways
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
    }
}