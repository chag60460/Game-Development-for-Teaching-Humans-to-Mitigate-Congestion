using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Acceleration : MonoBehaviour
{
    //Acceleration
    public float ForceSpeed = 12.0f;
    Rigidbody myRigidbodyComponent;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxis("vertical") > 0)
        {
            myRigidbodyComponent.AddForce(Vector3.right * ForceSpeed,ForceMode.Acceleration);
            Console.WriteLine("Acceleration: " + Vector3.right * ForceSpeed); // for debugging
        }
            
    }
}