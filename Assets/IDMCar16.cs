using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IDMCar16 : MonoBehaviour
{
    public GameObject[] wayPoints; //array of points

    int currentPoint = 0; // integer to keep track of which point we are on

    public float speed = 25.0f;

    public float rotationSpeed = 10.0f;

    //IDM Variables
    public float Acceleration = 2.0f;

    float desiredTime = 1f;
    float maxAcceleration = 2f;
    float brakeDecceleration = 1.5f;
    float delta = 4;
    float minimumSpacing = 1;
    float maxVelocity = 25;

    //IDM Model
    static float ComputeGap(GameObject trafficControlCar, GameObject leadCar)// Compute the gap between ego vehicle and lead vehicle
    {
        Vector3 selfPosition = trafficControlCar.transform.position;
        Vector3 leadVehiclePosition = leadCar.transform.position;

        return Vector3.Distance(selfPosition, leadVehiclePosition);
    }

    void Next_Step(float dt)
    {
        // # Setting velocity of the vehicle according to the Euler numerical integration scheme.
        float v = Acceleration * dt;
        speed += v;

        //We make sure that vehicles do not have negative velocities (they do not move backwards)?
        if (speed < 0)
        {
            speed = 0;
        }

        //this.transform.Translate(Vector3.forward * speed * Time.deltaTime);

        //Debug.Log("v" + v);
        //Debug.Log("Velocity" + speed);

    }

    void IDM_Model(GameObject trafficControlcar, float dt, GameObject leadCar)
    {
        //This is the net distance between the two vehicles(self and vehicle_lead)
        float currentGap = ComputeGap(trafficControlcar, leadCar);

        // IDM model
        float v_f = maxVelocity;

        float S = (float)(minimumSpacing + Math.Max(0, speed * desiredTime + speed * (speed - speed) / (2 * Math.Sqrt(maxAcceleration * brakeDecceleration))));
        //S = s_min + max(0, self.velocity.x * T + ((self.velocity.x * (self.velocity.x - self.velocity.x)) / (2 * math.sqrt(a_max * b))))
        //S is the same

        // Setting the vehicle's new acceleration here by setting 'self.acceleration' parameter
        Acceleration = (float)(maxAcceleration * (1 - Math.Pow((speed / v_f), delta) - Math.Pow((S / currentGap), 2)));
        //self.acceleration.xy = a_max * (1 - ((self.velocity.x / v_f) ** delta) - ((S / current_gap.x) ** 2)), 0

        //Debugging


        //S
        //Debug.Log("S" + S); 
        //Debug.Log("minimumSpacing" + minimumSpacing);
        //Debug.Log("speed" + speed);
        //Debug.Log("desiredTime" + desiredTime);
        //Debug.Log(speed * (speed - speed) / (2 * Math.Sqrt(maxAcceleration * brakeDecceleration)));
        //Debug.Log("speed * desiredTime" + speed * desiredTime);
        //Debug.Log(minimumSpacing + Math.Max(0, speed * desiredTime + speed * (speed - speed) / (2 * Math.Sqrt(maxAcceleration * brakeDecceleration))));
        //Debug.Log((float)minimumSpacing + Math.Max(0, speed * desiredTime + speed * (speed - speed) / (2 * Math.Sqrt(maxAcceleration * brakeDecceleration))));

        //Acceleration
        //Debug.Log("maxAcceleration" + maxAcceleration);
        //Debug.Log("delta" + delta);
        //Debug.Log("Math.Pow((speed / v_f), delta)" + Math.Pow((speed / v_f), delta));
        //Debug.Log("Math.Pow((S / currentGap), 2)" + Math.Pow((S / currentGap), 2));
        //Debug.Log("Acceleration" + Acceleration);

        //if (trafficControlcar.name == "car 1203 yellow")
        //{
        //    Debug.Log("Car " + trafficControlcar.name);
        //    Debug.Log("current_lead_gap between " + trafficControlcar.name + " and " + leadCar.name + "is " + currentGap);
        //    Debug.Log("Acceleration " + Acceleration);
        //    Debug.Log(trafficControlcar.transform.position);
        //}

        Next_Step(dt);

        //if (trafficControlcar.name == "car 1203 yellow")
        //{
        //    Debug.Log("Speed " + speed);
        //    Debug.Log("desiredTime" + desiredTime);

        //}

    }

    //Custom IDM Model

    void Safe_Next_Step(float dt, float current_lead_gap)
    {
        // # Setting velocity of the vehicle according to the Euler numerical integration scheme.
        float v = Acceleration * dt;
        speed += v;

        //If gap too small (thus acceleration <-10), adjust the speed
        if (v < -10)
        {
            v = current_lead_gap / 2 * dt;
            speed = v;
        }

        else
        {
            speed += v;

            if (speed < 0)
            {
                speed = 0;
            }
        }




    }

    void Custom_model(GameObject trafficControlCar, float dt, GameObject leadCar, GameObject followingCar)
    {
        float current_lead_gap = ComputeGap(trafficControlCar, leadCar);
        float current_follow_gap = ComputeGap(trafficControlCar, followingCar);

        //IDM variant model
        float v_f = maxVelocity;

        float S = (float)(minimumSpacing + Math.Max(0, speed * desiredTime + (speed * (speed - speed) / (2 * Math.Sqrt(maxAcceleration * brakeDecceleration)))));

        Acceleration = (float)(maxAcceleration * (1 - Math.Pow((speed / v_f), delta) - Math.Pow((S / current_lead_gap), 2)));

        if (trafficControlCar.name == "car 1203 yellow")
        {
            Debug.Log("Car " + trafficControlCar.name);
            Debug.Log("current_lead_gap between " + trafficControlCar.name + " and " + leadCar.name + "is " + current_lead_gap);
            Debug.Log("Acceleration " + Acceleration);
        }

        if (current_follow_gap < (1.5 * minimumSpacing))
        {
            Safe_Next_Step(dt, current_lead_gap);
        }
        else
        {
            Next_Step(dt);
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //check to see what where we are in relation to the next wayPoint
        if (Vector3.Distance(this.transform.position, wayPoints[currentPoint].transform.position) < 3)
            currentPoint++;

        //if we've checked the distance for all the way points, set currentPoint to 0
        if (currentPoint >= wayPoints.Length)
            currentPoint = 0;

        //this.transform.LookAt(wayPoints[currentPoint].transform);
        Quaternion lookAtWayPoint = Quaternion.LookRotation(wayPoints[currentPoint].transform.position - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(transform.rotation, lookAtWayPoint, rotationSpeed * Time.deltaTime);

        this.transform.Translate(0, 0, speed * Time.deltaTime);

        //Debug.Log("Is this repetitive?");

        //Calling Custom Model
        //IDM_Model(GameObject.Find("car 1203 yellow"), (float)0.1, GameObject.Find("Traffic Control Car"));
        //IDM_Model(GameObject.Find("Traffic Control Car"), (float)0.1, GameObject.Find("Traffic Control Car (1)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (1)"), (float)0.1, GameObject.Find("Traffic Control Car (2)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (2)"), (float)0.1, GameObject.Find("Traffic Control Car (3)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (3)"), (float)0.1, GameObject.Find("Traffic Control Car (4)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (4)"), (float)0.1, GameObject.Find("Traffic Control Car (5)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (5)"), (float)0.1, GameObject.Find("Traffic Control Car (6)"));

        //IDM_Model(GameObject.Find("Traffic Control Car (6)"), (float)0.1, GameObject.Find("Traffic Control Car (7)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (7)"), (float)0.1, GameObject.Find("Traffic Control Car (8)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (8)"), (float)0.1, GameObject.Find("Traffic Control Car (9)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (9)"), (float)0.1, GameObject.Find("Traffic Control Car (10)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (10)"), (float)0.1, GameObject.Find("Traffic Control Car (11)"));

        //IDM_Model(GameObject.Find("Traffic Control Car (11)"), (float)0.1, GameObject.Find("Traffic Control Car (12)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (12)"), (float)0.1, GameObject.Find("Traffic Control Car (13)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (13)"), (float)0.1, GameObject.Find("Traffic Control Car (14)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (14)"), (float)0.1, GameObject.Find("Traffic Control Car (15)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (15)"), (float)0.1, GameObject.Find("Traffic Control Car (16)"));

        IDM_Model(GameObject.Find("Traffic Control Car (16)"), (float)0.1, GameObject.Find("Traffic Control Car (17)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (17)"), (float)0.1, GameObject.Find("Traffic Control Car (18)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (18)"), (float)0.1, GameObject.Find("Traffic Control Car (19)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (19)"), (float)0.1, GameObject.Find("car 1203 yellow"));
        //IDM_Model(GameObject.Find("Traffic Control Car (20)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (21)"));

        //IDM_Model(GameObject.Find("Traffic Control Car (21)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (22)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (22)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (23)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (23)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (24)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (24)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (25)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (25)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (26)"));

        //IDM_Model(GameObject.Find("Traffic Control Car (26)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (27)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (27)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (28)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (28)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (29)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (29)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (30)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (30)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (31)"));

        //IDM_Model(GameObject.Find("Traffic Control Car (31)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (32)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (32)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (33)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (33)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (34)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (34)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (35)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (35)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (36)"));

        //IDM_Model(GameObject.Find("Traffic Control Car (36)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (37)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (37)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (38)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (38)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (39)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (39)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (40)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (40)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (41)"));

        //IDM_Model(GameObject.Find("Traffic Control Car (41)"), (float)Time.deltaTime, GameObject.Find("Traffic Control Car (42)"));

        //IDM_Model(GameObject.Find("Traffic Control Car (42)"), (float)Time.deltaTime, GameObject.Find("car 1203 yellow"));
    }
}



