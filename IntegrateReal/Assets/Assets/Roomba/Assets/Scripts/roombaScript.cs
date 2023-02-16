using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class roombaScript : MonoBehaviour
{
    [Tooltip("It s the charger position, maybe the waypoint number 0 or 1")]
    public GameObject stopPosition;
    public List<GameObject> waypoint;
    public int currentWaypoint;
    public int lastWaypoint;

    [Header("SETTINGS")]
    public float batteryPercentage;
    public float lowBatteryPercentage;
    public float maxBatteryPercentage;
    public float batteryConsumption;
    public float batteryChargeSpeed;
    public float defaultBatteryConsumption;

    public float speed;
    public float rotationSpeed;

    [Header("UI ELEMENTS")]
    public Image batteryBar;
    public Text lowBatteryText;
    public Text batteryText;

    [Header("KEYS")]
    [Tooltip("Press this key to start cleaning")]
    public KeyCode cleanKey;
    [Tooltip("Press this key to back")]
    public KeyCode backKey;

    float deltaTime;
    float fixedBatteryConsumption;
    bool cleaning = false;
    bool canCharge = false;

    NavMeshAgent roombaNavMeshAgent;
    Rigidbody rb;
    Vector3 destination;

    void Start()
    {
        fixedBatteryConsumption = batteryConsumption;
        //gets the components
        rb = GetComponent<Rigidbody>();
        roombaNavMeshAgent = GetComponent<NavMeshAgent>();

        deltaTime = Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        //if the roomba is close to a object with "Waypoint" tag
        if (other.CompareTag("Waypoint") && cleaning == true)
        {
            if (other.transform.position == destination)
            {
                currentWaypoint++;
            }
        }

        //if the object is the charger
        if (other.CompareTag("Charger"))
        {
            //roomba can charge
            canCharge = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        //if the roomba is far away of the charger
        if (other.CompareTag("Charger"))
        {
            //it can not charge
            canCharge = false;
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * deltaTime * Input.GetAxis("Vertical"));
        transform.Rotate(Vector3.up * rotationSpeed * deltaTime * Input.GetAxis("Horizontal"));

        //if press C key and is not cleaning
        if (Input.GetKeyDown(cleanKey) && cleaning == false)
        {
            cleaning = true;
            roombaNavMeshAgent.isStopped = false;
        }
        else if (Input.GetKeyDown(cleanKey) && cleaning)
        {
            cleaning = false;
            roombaNavMeshAgent.Stop();
        }

        //if press B key and cleaning is true
        if (Input.GetKeyDown(backKey) && cleaning == true)
        {
            //get back to charger position
            roombaNavMeshAgent.destination = stopPosition.transform.position;
            currentWaypoint = 0;
            cleaning = false;
        }

        //if the roomba is moving
        if (rb.velocity.z != 0f)
        {
            //decrease the battery level
            batteryConsumption = fixedBatteryConsumption;
        }
        else if (rb.velocity.z == 0f)
        {
            batteryConsumption = defaultBatteryConsumption;
        }

        //batteryPercentage minus batteryConsumption
        if (canCharge == false)
        {
            batteryPercentage -= batteryConsumption * Time.deltaTime;
        }
        
        batteryBar.fillAmount = batteryPercentage / 100f;
        batteryText.text = batteryPercentage.ToString("f1") + "%";

        //if canCharge is true and battery is less than max battery
        if (canCharge == true && batteryPercentage < maxBatteryPercentage)
        {
            //charge the battery
            batteryPercentage += batteryChargeSpeed * Time.deltaTime;
        }

        //if the battery is less than low battery percentage and it can not charge
        if (batteryPercentage < lowBatteryPercentage && canCharge == false)
        {
            //lowBatteryText prints "LOW BATTERY"
            lowBatteryText.text = "LOW BATTERY";
        }
        //if not
        else if (batteryPercentage > lowBatteryPercentage || canCharge == true)
        {
            //it not prints
            lowBatteryText.text = "";
        }

        //if battery is empty
        if (batteryPercentage <= 0f)
        {
            lowBatteryText.text = "BATTERY EMPTY";
        }

        if (cleaning == false) return;

        if (batteryPercentage > 0f)
        {
            destination = waypoint[currentWaypoint].transform.position;

            //if it is the last waypoint
            if (currentWaypoint == lastWaypoint)
            {
                //back to charger position
                cleaning = false;
                destination = stopPosition.transform.position;
                currentWaypoint = 0;
            }

            //set the roomba destination to the vector3 destination
            roombaNavMeshAgent.SetDestination(destination);
        }
        else
        {
            //roomba stop
            roombaNavMeshAgent.Stop();
        }
    }

    //ANDROID PART

    //start cleaning method
    public void clean()
    {
        cleaning = true;
    }

    //back to charger method
    public void backToCharger()
    {
        //set the destination to charger, current waypoint is 0
        roombaNavMeshAgent.destination = stopPosition.transform.position;
        currentWaypoint = 0;
        cleaning = false;
    }
}