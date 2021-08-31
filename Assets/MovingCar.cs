using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovingCar : MonoBehaviour
{
    public float speed;
    public float forwardInput;
    //public Vector2 Acceleration = new Vector2(2f,0f);

    ////IDM Variables
    //float desiredTime = 1f;
    //float maxAcceleration = 2f;
    //float brakeDecceleration = 1.5f;
    //float exponent = 4;
    //float minimumSpacing = 1;
    //float maxVelocity = 25;

    ////IDM Model
    //static float ComputeGap(GameObject trafficControlCar, GameObject leadCar)// Compute the gap between ego vehicle and lead vehicle
    //{
    //    Vector3 selfPosition = trafficControlCar.transform.position;
    //    Vector3 leadVehiclePosition = leadCar.transform.position;

    //    return Vector3.Distance(selfPosition, leadVehiclePosition);
    //}

    //void Next_Step(float dt)
    //{
    //    // # Setting velocity of the vehicle according to the Euler numerical integration scheme.
    //    Vector2 v = new Vector2();
    //    v.x = Acceleration.x * dt;
    //    speed += v.x;

    //    //We make sure that vehicles do not have negative velocities (they do not move backwards)?
    //    if (speed < 0)
    //    {
    //        v = Vector2.zero;
    //        speed = v.x;
    //    }
  
    //}

    //void IDM_Model(GameObject trafficControlcar, GameObject leadCar)
    //{
    //    //This is the net distance between the two vehicles(self and vehicle_lead)
    //    float currentGap = ComputeGap(trafficControlcar, leadCar);

    //    // IDM model
    //    float v_f = maxVelocity;

    //    float S = (float)(minimumSpacing + Math.Max(0, speed * desiredTime + (speed * (speed - speed)) / (2 * Math.Sqrt(maxAcceleration * brakeDecceleration)))); // is this necessary (e.g. account for a positive velocity)?

    //    // Setting the vehicle's new acceleration here by setting 'self.acceleration' parameter
    //    Acceleration.x = (float)(maxAcceleration * (1 - Math.Pow((speed / v_f), exponent) - Math.Pow((S / currentGap), 2)));

    //    Next_Step((float)0.1);
    //}

    ////Custom IDM Model

    //void Safe_Next_Step(float dt, float current_lead_gap)
    //{
    //    // # Setting velocity of the vehicle according to the Euler numerical integration scheme.
    //    Vector2 v = new Vector2();
    //    v.x = Acceleration.x * dt;
    //    speed += v.x;

    //    //Not sure what this is doing here?
    //    if (v.x < -10)
    //    {
    //        v = Vector2.zero;
    //        v.x = (current_lead_gap / 2 * dt);
    //        speed = v.x;
    //    }

    //    else
    //    {
    //        speed += v.x;
    //    }

    //    if (speed < 0)
    //    {
    //        v = Vector2.zero;
    //        speed = v.x;
    //    }

    //}

    //void Custom_model(GameObject trafficControlCar, float dt, GameObject leadCar, GameObject followingCar)
    //{
    //    float current_lead_gap = ComputeGap(trafficControlCar, leadCar);
    //    float current_follow_gap = ComputeGap(trafficControlCar, followingCar);

    //    //IDM variant model
    //    float v_f = maxVelocity;

    //    float S = (float) (minimumSpacing + Math.Max(0, speed * desiredTime + (speed * (speed - speed) / (2 * Math.Sqrt(maxAcceleration * brakeDecceleration)))));

    //    Acceleration.x = (float)(maxAcceleration * (1 - Math.Pow((speed / v_f), exponent) - Math.Pow((S / current_lead_gap), 2)));

    //    if (current_follow_gap < (1.5 * minimumSpacing))
    //    {
    //        Safe_Next_Step(dt, ComputeGap(trafficControlCar, leadCar));
    //    }
    //    else
    //    {
    //        Next_Step(dt);
    //    }

    //}

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Calling Custom Model
        //Custom_model(GameObject.Find("Traffic Control Car (1)"), (float)0.1, GameObject.Find("Traffic Control Car (2)"), GameObject.Find("Traffic Control Car"));
        //Custom_model(GameObject.Find("Traffic Control Car (2)"), (float)0.1, GameObject.Find("Traffic Control Car (3)"), GameObject.Find("Traffic Control Car (1)"));
        //Custom_model(GameObject.Find("Traffic Control Car (3)"), (float)0.1, GameObject.Find("Traffic Control Car (4)"), GameObject.Find("Traffic Control Car (2)"));
        //Custom_model(GameObject.Find("Traffic Control Car (4)"), (float)0.1, GameObject.Find("Traffic Control Car (5)"), GameObject.Find("Traffic Control Car (3)"));
        //Custom_model(GameObject.Find("Traffic Control Car (5)"), (float)0.1, GameObject.Find("Traffic Control Car (6)"), GameObject.Find("Traffic Control Car (4)"));

        //Custom_model(GameObject.Find("Traffic Control Car (6)"), (float)0.1, GameObject.Find("Traffic Control Car (7)"), GameObject.Find("Traffic Control Car (5)"));
        //Custom_model(GameObject.Find("Traffic Control Car (7)"), (float)0.1, GameObject.Find("Traffic Control Car (8)"), GameObject.Find("Traffic Control Car (6)"));
        //Custom_model(GameObject.Find("Traffic Control Car (8)"), (float)0.1, GameObject.Find("Traffic Control Car (9)"), GameObject.Find("Traffic Control Car (7)"));
        //Custom_model(GameObject.Find("Traffic Control Car (9)"), (float)0.1, GameObject.Find("Traffic Control Car (10)"), GameObject.Find("Traffic Control Car (8)"));
        //Custom_model(GameObject.Find("Traffic Control Car (10)"), (float)0.1, GameObject.Find("Traffic Control Car (11)"), GameObject.Find("Traffic Control Car (9)"));

        //Custom_model(GameObject.Find("Traffic Control Car (11)"), (float)0.1, GameObject.Find("Traffic Control Car (12)"), GameObject.Find("Traffic Control Car (10)"));
        //Custom_model(GameObject.Find("Traffic Control Car (12)"), (float)0.1, GameObject.Find("Traffic Control Car (13)"), GameObject.Find("Traffic Control Car (11)"));
        //Custom_model(GameObject.Find("Traffic Control Car (13)"), (float)0.1, GameObject.Find("Traffic Control Car (14)"), GameObject.Find("Traffic Control Car (12)"));
        //Custom_model(GameObject.Find("Traffic Control Car (14)"), (float)0.1, GameObject.Find("Traffic Control Car (15)"), GameObject.Find("Traffic Control Car (13)"));
        //Custom_model(GameObject.Find("Traffic Control Car (15)"), (float)0.1, GameObject.Find("Traffic Control Car (16)"), GameObject.Find("Traffic Control Car (14)"));

        //Custom_model(GameObject.Find("Traffic Control Car (16)"), (float)0.1, GameObject.Find("Traffic Control Car (17)"), GameObject.Find("Traffic Control Car (15)"));
        //Custom_model(GameObject.Find("Traffic Control Car (17)"), (float)0.1, GameObject.Find("Traffic Control Car (18)"), GameObject.Find("Traffic Control Car (16)"));
        //Custom_model(GameObject.Find("Traffic Control Car (18)"), (float)0.1, GameObject.Find("Traffic Control Car (19)"), GameObject.Find("Traffic Control Car (17)"));
        //Custom_model(GameObject.Find("Traffic Control Car (19)"), (float)0.1, GameObject.Find("Traffic Control Car (20)"), GameObject.Find("Traffic Control Car (18)"));
        //Custom_model(GameObject.Find("Traffic Control Car (20)"), (float)0.1, GameObject.Find("Traffic Control Car (21)"), GameObject.Find("Traffic Control Car (19)"));

        //Custom_model(GameObject.Find("Traffic Control Car (21)"), (float)0.1, GameObject.Find("Traffic Control Car (22)"), GameObject.Find("Traffic Control Car (20)"));
        //Custom_model(GameObject.Find("Traffic Control Car (22)"), (float)0.1, GameObject.Find("Traffic Control Car (23)"), GameObject.Find("Traffic Control Car (21)"));
        //Custom_model(GameObject.Find("Traffic Control Car (23)"), (float)0.1, GameObject.Find("Traffic Control Car (24)"), GameObject.Find("Traffic Control Car (22)"));
        //Custom_model(GameObject.Find("Traffic Control Car (24)"), (float)0.1, GameObject.Find("Traffic Control Car (25)"), GameObject.Find("Traffic Control Car (23)"));
        //Custom_model(GameObject.Find("Traffic Control Car (25)"), (float)0.1, GameObject.Find("car 1203 yellow"), GameObject.Find("Traffic Control Car (24)"));

        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}