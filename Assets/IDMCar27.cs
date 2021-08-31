using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IDMCar27 : MonoBehaviour
{
    public float speed = 25.0f;

    //IDM Variables
    public float Acceleration = 2.0f;

    float desiredTime = 1f;
    float maxAcceleration = 2f;
    float brakeDecceleration = 1.5f;
    float delta = 4;
    float minimumSpacing = 1; // Increase minimumSpacing to see
    float maxVelocity = 25;

    //IDM Model
    static float ComputeGap(GameObject trafficControlCar, GameObject leadCar)// Compute the gap between ego vehicle and lead vehicle
    {
        return leadCar.transform.position.z - trafficControlCar.transform.position.z;
    }

    void Next_Step(float dt)
    {
        // # Setting velocity of the vehicle according to the Euler numerical integration scheme.
        float v = Acceleration * dt;
        this.speed += v;

        //We make sure that vehicles do not have negative velocities (they do not move backwards)?
        if (speed < 0)
        {
            speed = 0;
        }

        //Debug.Log("v" + v);
        //Debug.Log("Velocity" + speed);

    }

    void IDM_Model(GameObject trafficControlcar, float dt, GameObject leadCar)
    {
        float currentGap;

        //Hardcode for the first car
        if (trafficControlcar.name == "Traffic Control Car (27)")
        {
            currentGap = (float)5;
        }

        else
        {
            //This is the net distance between the two vehicles(self and vehicle_lead)
            currentGap = ComputeGap(trafficControlcar, leadCar);
        }

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


        Debug.Log("Car " + trafficControlcar.name);
        Debug.Log("current_lead_gap between " + trafficControlcar.name + " and " + leadCar.name + "is " + currentGap);
        Debug.Log("Acceleration " + Acceleration);
        Debug.Log(trafficControlcar.transform.position);

        Next_Step(dt);

        Debug.Log("Speed " + speed);
        Debug.Log("desiredTime" + desiredTime);

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public Vector3 getPosition(string trafficControlcar)
    {
        Debug.Log(trafficControlcar + " position after transform");
        Debug.Log(GameObject.Find(trafficControlcar).transform.position);
        Vector3 position = GameObject.Find(trafficControlcar).transform.position;
        return position;
    }

    // Update is called once per frame
    void Update()
    {
        //Calling IDM
        //IDM_Model(GameObject.Find("Traffic Control Car (20)"), (float)0.1, GameObject.Find("Traffic Control Car (21)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (21)"), (float)0.1, GameObject.Find("Traffic Control Car (22)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (22)"), (float)0.1, GameObject.Find("Traffic Control Car (23)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (23)"), (float)0.1, GameObject.Find("Traffic Control Car (24)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (24)"), (float)0.1, GameObject.Find("Traffic Control Car (25)"));

        //IDM_Model(GameObject.Find("Traffic Control Car (25)"), (float)0.1, GameObject.Find("Traffic Control Car (26)"));
        //IDM_Model(GameObject.Find("Traffic Control Car (26)"), (float)0.1, GameObject.Find("Traffic Control Car (27)"));

        IDM_Model(GameObject.Find("Traffic Control Car (27)"), (float)0.1, GameObject.Find("Traffic Control Car (27)"));

        //Debug.Log("Before Translate");
        //getPosition("Traffic Control Car (20)");
        //getPosition("Traffic Control Car (21)");
        //getPosition("Traffic Control Car (22)");
        //getPosition("Traffic Control Car (23)");
        //getPosition("Traffic Control Car (24)");
        //getPosition("Traffic Control Car (25)");
        //Vector3 position26 = getPosition("Traffic Control Car (26)");

        //Vector3 position27 = getPosition("Traffic Control Car (27)");

        this.transform.Translate(0, 0, speed * Time.deltaTime);


        //Debug.Log("After Translate");
        //getPosition("Traffic Control Car (20)");
        //getPosition("Traffic Control Car (21)");
        //getPosition("Traffic Control Car (22)");
        //getPosition("Traffic Control Car (23)");
        //getPosition("Traffic Control Car (24)");
        //getPosition("Traffic Control Car (25)");
        //Vector3 position26After = getPosition("Traffic Control Car (26)");
        //Vector3 position27After = getPosition("Traffic Control Car (27)");

        //if (position26 != position26After)
        //{
        //    throw new NullReferenceException();
        //}

        //if (position27 != position27After)
        //{
        //    throw new NullReferenceException();
        //}


    }
}
