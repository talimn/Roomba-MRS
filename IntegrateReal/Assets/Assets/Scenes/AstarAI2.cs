using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using Pathfinding;



public class AstarAI2 : MonoBehaviour
{
    // The point to which the capsule will move
    // Will be performed in A*
    // The targetPosition variable can be changed in inspector to be any GameObject

    public Vector3 offs;
    private Vector3 targetPosition = new Vector3(-2.0f, 0.0f, -1.287f);

    private Vector3 newTarg = new Vector3(0.0f, 0.0f, 0.0f);
    private int x = 0;
    private int y = 0;
    private int z = 0;
    private int w = 0;
    private int r = 0;
    private float sum = 0.0f;


    private Seeker seeker;
    private CharacterController controller;

    //the calculated path
    public Path path;

    //capsule's speed in meters per second
    public float speed = 2;

    //max distance from capsule to waypoint for it to continue to next waypoint
    public float nextWaypointDistance = 3;

    //waypoint we are currently moving towards
    private int currentWaypoint = 0;

    //How often to recalculate path in seconds
    public float repathRate = 0.5f;
    private float lastRepath = -9999;

    // Start is called before the first frame update
    public void Start()
    {
        //get a reference to the seeker component
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("Path error?" + p.error);
        if (!p.error)
        {
            path = p;
            // reset the waypoint counter so that we move towards first point in path
            currentWaypoint = 0;


        }
    }

    public float AbsVal(float a, float b)
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

    // Update is called once per frame
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
            // No path to follow yet so don't do anything
            return;
        }

        if (currentWaypoint > path.vectorPath.Count) return;

        if (currentWaypoint == path.vectorPath.Count)
        {
            Debug.Log("Path complete.");
            currentWaypoint++;
            return;
        }

        // Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed;

        // Note that SimpleMove takes a velocity in meters/second, so we should not multiply by Time.deltaTime
        controller.SimpleMove(dir);

        if ((transform.position - path.vectorPath[currentWaypoint]).sqrMagnitude < nextWaypointDistance * nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }

        // Go to next point
        //POINT 1
        if ((AbsVal(transform.position.x, targetPosition.x) < 2.0f) && (x < 2))
        {
            newTarg = new Vector3(-10.0f, 0.0f, -10.0f);
            targetPosition = newTarg;
            //put variable here to take value assigned to targetPosition
            currentWaypoint = 0;
            x++;
            Update();
        }
        //POINT 2
        else if ((AbsVal(transform.position.x, targetPosition.x) < 2.0f) && (y < 2))
        {
            //newTarg = new Vector3(-1.7f, 0.0f, 1.0f);
            //targetPosition = newTarg;
            //currentWaypoint = 0;
            //y++;

            newTarg = new Vector3(12.0f, 0.0f, 12.0f);
            targetPosition = newTarg;
            currentWaypoint = 0;
            y++;
            Update();


        }
        //POINT 3
        /*else if ((AbsVal(transform.position.x, targetPosition.x) < 0.5f) && (z < 2))
        {

            newTarg = new Vector3(-3.77f, 0.0f, 0.0f);
            targetPosition = newTarg;
            currentWaypoint = 0;
            z++;
            Update();


        }*/

        // POINT 4
        else if ((AbsVal(transform.position.x, targetPosition.x) < 2.0f) && (w < 2))
        {

            newTarg = new Vector3(11.0f, 0.0f, -11.0f);
            targetPosition = newTarg;
            currentWaypoint = 0;
            w++;
            Update();


        }

        // POINT 5

        else if ((AbsVal(transform.position.x, targetPosition.x) < 2.0f) && (r < 2))
        {

            newTarg = new Vector3(-10.0f, 0.0f, 9.0f);
            targetPosition = newTarg;
            currentWaypoint = 0;
            r++;
            Update();


        }








    }

}    