using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public GameObject[] wayPoints; //array of points

    int currentPoint = 0; // integer to keep track of which point we are on

    public float carSpeed = 20.0f;

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

        this.transform.LookAt(wayPoints[currentPoint].transform);
        this.transform.Translate(0, 0, carSpeed * Time.deltaTime);
    }
}
