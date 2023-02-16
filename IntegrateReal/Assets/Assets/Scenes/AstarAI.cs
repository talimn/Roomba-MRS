using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using Pathfinding;



public class AstarAI : MonoBehaviour
{
    // The point to which the capsule will move
    // Will be performed in A*
    // The targetPosition variable here is a set of coordinates in the x-y-z plane
    // This can also be set to a specific game object

    public Vector3 offs; //offset value that will be added to each new targetPosition
    private Vector3 targetPosition = new Vector3(-2.0f, 0.0f, -1.287f); //coordinates of first target point
    private Vector3 newTarg = new Vector3(0.0f, 0.0f, 0.0f); //coordinates of next target point

    //Iterative values used to go from point to point
    private int x = 0; 
    private int y = 0;
    private int z = 0;
    private int w = 0;

    //Initializing value for absolute value function
    private float sum = 0.0f;

    // Roomba has a seeker component and charactercontroller component
    private Seeker seeker;
    private CharacterController controller;


    // These are the public variables that can be changed.
    // The calculated path that is the most optimal
    public Path path;

    // Roomba speed : m/s
    public float speed = 2;

    // The maximum value for the distance between the current location of the Roomba and the next waypoint
    public float nextWaypointDistance = 3;

    // The waypoint to which the Roomba is traveling
    private int currentWaypoint = 0; 

    // The public value of repathRate is how long it takes to recalculate based on interference in the path
    public float repathRate = 0.5f;
    private float lastRepath = -9999;


    // Start called before first update
    public void Start()
    {
        //Here we are referencing the seeker and charactercontroller components
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
    }


    /*Once a path has been calculated, we need to know the status.
    An error message will display during game mode if the path calculation failed.
    Otherwise, the currentWaypoint will be reset.*/
    public void OnPathComplete (Path p)
    {
        Debug.Log("Path error?" + p.error);
        if(!p.error)
        {
            path = p;
            // currentWaypoint is reset to 0 so that Roomba can began moving to the first point once path has been calculated completely
            currentWaypoint = 0;
            
            
        }
    }

    // Absolute value function
    // Simply performs the mathematical method of converting positive or negative values into their absolute values
    // This will be used to evaluate the distance between the current position of the Roomba and the target point
    public float AbsVal (float a, float b) 
    {
        sum = a - b;
        if (sum < 0.0f)
        {
            sum = sum * -1.0f;
            return sum;
        }
        else
        {
            return sum;
        }
    }

    // Update called only once per frame
    public void Update()
    {
       
            
            if (Time.time - lastRepath > repathRate && seeker.IsDone())
            {
                lastRepath = Time.time + Random.value * repathRate * 0.5f;

                // Start a new path to the targetPosition, call the OnPathComplete function
                // When path has been calculated
                seeker.StartPath(transform.position, targetPosition, OnPathComplete);

            }


            if (path == null)
            {
                // Nothing happens because no path has been calculated
                return;
            }

            if (currentWaypoint > path.vectorPath.Count) return; // if waypoint is out of range then Roomba will not continue

            if (currentWaypoint == path.vectorPath.Count) // Calculation of path has been successful!!!!
            {
                Debug.Log("Path complete.");
                currentWaypoint++;
                return;
            }

            // Normalizing to direct Roomba to next waypoint
            Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            dir *= speed;

            // Using the character controller component to start moving the Roomba on the path
            // velocity is in m/s and is determined by the user as a public variable
            controller.SimpleMove(dir);

            if ((transform.position - path.vectorPath[currentWaypoint]).sqrMagnitude < nextWaypointDistance * nextWaypointDistance)
            {
                currentWaypoint++; // currentWaypoint is incrementing so that the Roomba can move to the next point on the path
                return;
            }

            // Go to next point
            //POINT 1
            if ((AbsVal(transform.position.x, targetPosition.x) < 0.4f) && (x < 2))
            {
                    newTarg = new Vector3(2.1f, 0.0f, -1.0f);
                    targetPosition = newTarg;
                    //put variable here to take value assigned to targetPosition
                    currentWaypoint = 0;
                    x++;
                    Update();
            }
            //POINT 2
            else if ((AbsVal(transform.position.x, targetPosition.x) < 0.4f) && (y < 2))
            {
                //newTarg = new Vector3(-1.7f, 0.0f, 1.0f);
                //targetPosition = newTarg;
                //currentWaypoint = 0;
                //y++;
                
                    newTarg = new Vector3(-1.65f, 0.0f, 1.0f);
                    targetPosition = newTarg;
                    currentWaypoint = 0;
                    y++;
                    Update();
                
                
            }
            //POINT 3
            /*else if ((AbsVal(transform.position.x, targetPosition.x) < 0.4f) && (z < 2))
            {
                
                    newTarg = new Vector3(-3.77f, 0.0f, 0.0f);
                    targetPosition = newTarg;
                    currentWaypoint = 0;
                    z++;
                    Update();
                
                
            }*/

            // POINT 4
            // Return to start
            else if ((AbsVal(transform.position.x, targetPosition.x) < 0.4f) && (w < 2))
            {
                    //4.09, 0.0, -1.59
                    newTarg = new Vector3(4.09f, 0.0f, -1.59f);
                    targetPosition = newTarg;
                    currentWaypoint = 0;
                    w++;
                    Update();


            }

    }
}
