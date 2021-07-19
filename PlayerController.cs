using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variables to move the vehicle forward
    public float currentSpeed;
    public float maxSpeed = 120.0f;

    public float forwardInput;

    //variables to move the vehicle sideways
    public float turnSpeed;
    public float horizontalInput;

    //Acceleration
    public float AccelerationMagnitude = 12.0f; // Assume constant for how, since we don't know how hard someone presses the gas pedal without deploying the game first
    Rigidbody myRigidbodyComponent;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbodyComponent = GetComponent<Rigidbody>();
    }

    // FixedUpdate applies force to each fixed frame when Rigidbody is called
    void FixedUpdate()
    {
        // Move the vehicle forward
        forwardInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentSpeed <= maxSpeed)
            {
                currentSpeed += Time.deltaTime * AccelerationMagnitude;
                myRigidbodyComponent.AddForce(Vector3.forward * AccelerationMagnitude, ForceMode.Acceleration);
            }

            else if (currentSpeed >= maxSpeed)
            {
                currentSpeed = maxSpeed;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) // player brakes 
        {
            if (currentSpeed >= 0)
            {
                currentSpeed -= Time.deltaTime * AccelerationMagnitude;
                myRigidbodyComponent.AddForce(Vector3.back * AccelerationMagnitude, ForceMode.Acceleration);
            }

            transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed * forwardInput);
        }

        transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed * forwardInput);

        // Move the vehicle sideways
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
    }
}