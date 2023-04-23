using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpButton : MonoBehaviour
{
    public enum OVERRIDE_STATE {NONE, ROOMBA1, ROOMBA2};
    public enum DIRECTION { FORWARD, BACK, LEFT, RIGHT, STOP};

    private int overrideCounter = 0;
    private OVERRIDE_STATE overideState;
    private DIRECTION roomba1Direction;
    private DIRECTION roomba2Direction;

    public Button m_StartButton, m_UpButton, m_DownButton, m_LeftButton, m_RightButton, m_StopButton, m_OverrideButton;
    public GameObject rmba;
    public GameObject roomba1check;
    public GameObject roomba2check;
    public BluetoothControlScript btControls;

    private float speed = 2.0f;

    //Variables for added start button
    private bool canStart = false;
    private float currentTime = 0f;
       

    void Start()
    {
        overideState = OVERRIDE_STATE.NONE;
        m_StartButton.onClick.AddListener(TaskStart);
        m_UpButton.onClick.AddListener(TaskUp);
        m_DownButton.onClick.AddListener(TaskDown);
        m_LeftButton.onClick.AddListener(TaskLeft);
        m_RightButton.onClick.AddListener(TaskRight);
        m_StopButton.onClick.AddListener(TaskStop);
        m_OverrideButton.onClick.AddListener(TaskOverride);
        Debug.Log("Starting Roomba 1 Connection" + btControls.connectRoombaOne());
        //Debug.Log("Starting Roomba 2 Connection" + btControls.connectRoombaTwo());
    }

    void TaskStart()
    {
        canStart = true;
    }

    void TaskUp()
    {
        Debug.Log("You are moving forward.");
        
        switch (overideState)
        {
            case OVERRIDE_STATE.ROOMBA1:
                roomba1Direction = DIRECTION.FORWARD;
                btControls.sendData(1, 'w');
                // Send a fwd command to Roomba 1
                break;
            case OVERRIDE_STATE.ROOMBA2:
                roomba2Direction = DIRECTION.FORWARD;
                btControls.sendData(2, 'w');
                // Send a fwd command to Roomba 2
                break;
            case OVERRIDE_STATE.NONE:

                break;
        }
    }

    void TaskDown()
    {
        Debug.Log("You are moving backwards.");

        switch (overideState)
        {
            case OVERRIDE_STATE.ROOMBA1:
                roomba1Direction = DIRECTION.BACK;
                btControls.sendData(1, 'd');
                btControls.sendData(1, 'd');
                btControls.sendData(1, 'w');
                // Send a back command to Roomba 1
                break;
            case OVERRIDE_STATE.ROOMBA2:
                roomba2Direction = DIRECTION.BACK;
                btControls.sendData(2, 'd');
                btControls.sendData(2, 'd');
                btControls.sendData(1, 'w');
                // Send a back command to Roomba 2
                break;
            case OVERRIDE_STATE.NONE:

                break;
        }
    }

    void TaskLeft()
    {
        Debug.Log("You are moving left.");
        switch (overideState)
        {
            case OVERRIDE_STATE.ROOMBA1:
                roomba1Direction = DIRECTION.LEFT;
                btControls.sendData(1, 'a');
                // Send a left command to Roomba 1
                break;
            case OVERRIDE_STATE.ROOMBA2:
                roomba2Direction = DIRECTION.LEFT;
                btControls.sendData(2, 'a');
                // Send a left command to Roomba 2
                break;
            case OVERRIDE_STATE.NONE:

                break;
        }
    }

    void TaskRight()
    {
        Debug.Log("You are moving right.");
        switch (overideState)
        {
            case OVERRIDE_STATE.ROOMBA1:
                roomba1Direction = DIRECTION.RIGHT;
                btControls.sendData(1, 'd');
                // Send a right command to Roomba 1
                break;
            case OVERRIDE_STATE.ROOMBA2:
                roomba2Direction = DIRECTION.RIGHT;
                btControls.sendData(1, 'd');
                // Send a right command to Roomba 2
                break;
            case OVERRIDE_STATE.NONE:

                break;
        }
    }

    void TaskStop()
    {
        Debug.Log("You have stopped.");
        switch (overideState)
        {
            case OVERRIDE_STATE.ROOMBA1:
                roomba1Direction = DIRECTION.STOP;
                btControls.sendData(1, 's');
                // Send a stop command to Roomba 1
                break;
            case OVERRIDE_STATE.ROOMBA2:
                roomba2Direction = DIRECTION.STOP;
                btControls.sendData(2, 's');
                // Send a stop command to Roomba 2
                break;
            case OVERRIDE_STATE.NONE:
                roomba1Direction = DIRECTION.STOP;
                btControls.sendData(1, 's');
                break;
        }
    }

    void TaskOverride()
    {
        overrideCounter = overrideCounter + 1;
        Debug.Log("Override clicked.");
        if (toggleChoice(roomba1check.GetComponent<Toggle>().isOn))
        {
            //overideState = OVERRIDE_STATE.ROOMBA1;
            if (overrideCounter % 2 == 1)
            {
                overideState = OVERRIDE_STATE.ROOMBA1;
            }
            else
            {
                btControls.sendData(1, 's');
                overideState = OVERRIDE_STATE.NONE;
                
            }
        }
        else if (toggleChoice(roomba2check.GetComponent<Toggle>().isOn))
        {
            overideState = OVERRIDE_STATE.ROOMBA2;
            /*if ((GameObject.Find("Override Indicator").GetComponent<Image>().color).ToString() == "RGBA(0.000, 255.000, 0.000, 1.000)")
            {
                overideState = OVERRIDE_STATE.ROOMBA2;
            }
            else
            {
                overideState = OVERRIDE_STATE.NONE;
            }*/
        }
        else
        {
            overideState = OVERRIDE_STATE.NONE;
        }
    }

    

    // Update is called once per frame
    /*
     Here is where I am currently running the pathfinding script. The command execution is based on time right now.

    */
    void Update()
    {
        if (overideState == OVERRIDE_STATE.NONE && canStart)
        {
            currentTime = Time.fixedTime;
            if (Time.fixedTime == currentTime + 1)
            {

                btControls.sendData(1, 'w');

            }

            /*else if (Time.fixedTime == 10.0f)
            {
                btControls.sendData(1, 'a');
                btControls.sendData(1, '%');
                btControls.sendData(1, 'a');
                btControls.sendData(1, 'w');
                
            }

            

            else if (Time.fixedTime == 20.0f)
            {
                btControls.sendData(1, 'd');
                btControls.sendData(1, '%');
                btControls.sendData(1, 'd');
                btControls.sendData(1, 'w');
            }

            else if (Time.fixedTime == 30.0f)
            {
                btControls.sendData(1, 'a');
                btControls.sendData(1, '%');
                btControls.sendData(1, 'a');
                btControls.sendData(1, 'w');
            }

            else if (Time.fixedTime == 40.0f)
            {
                btControls.sendData(1, 'd');
                btControls.sendData(1, '%');
                btControls.sendData(1, 'd');
                btControls.sendData(1, 'w');
            }

            else if (Time.fixedTime >= 50.0f)
            {
                btControls.sendData(1, 's');
            }*/
        }

        if (btControls.dataDirty)
        {
            Debug.Log("Location Data: X:" + btControls.locationArray_3F[0] + " Y:" + btControls.locationArray_3F[1] + " Z:" + btControls.locationArray_3F[2]);
            Debug.Log("Sensor Data: 1: " + btControls.sensorArray_5F[0] + " 2:" + btControls.sensorArray_5F[1] + " 3:" + btControls.sensorArray_5F[2] + " 4:" + btControls.sensorArray_5F[3] + " 5:" + btControls.sensorArray_5F[4]);
            btControls.dataDirty = false;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 100), (Time.fixedTime).ToString());
    }

    public bool toggleChoice(bool togs)
    {
        return togs;
    }

}
