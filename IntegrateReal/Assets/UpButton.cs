using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpButton : MonoBehaviour
{
    public enum OVERRIDE_STATE {NONE, ROOMBA1, ROOMBA2};
    public enum DIRECTION { FORWARD, BACK, LEFT, RIGHT, STOP};

    private OVERRIDE_STATE overideState;
    private DIRECTION roomba1Direction;
    private DIRECTION roomba2Direction;

    public Button m_UpButton, m_DownButton, m_LeftButton, m_RightButton, m_StopButton, m_OverrideButton;
    public GameObject rmba;
    public GameObject roomba1check;
    public GameObject roomba2check;
    public BluetoothControlScript btControls;

    private float speed = 2.0f;
       

    void Start()
    {
        overideState = OVERRIDE_STATE.NONE;
        m_UpButton.onClick.AddListener(TaskUp);
        m_DownButton.onClick.AddListener(TaskDown);
        m_LeftButton.onClick.AddListener(TaskLeft);
        m_RightButton.onClick.AddListener(TaskRight);
        m_StopButton.onClick.AddListener(TaskStop);
        m_OverrideButton.onClick.AddListener(TaskOverride);
        Debug.Log("Starting Roomba 1 Connection" + btControls.connectRoombaOne());
        //Debug.Log("Starting Roomba 2 Connection" + btControls.connectRoombaTwo());
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

                break;
        }
    }

    void TaskOverride()
    {
        Debug.Log("Override clicked.");
        if (toggleChoice(roomba1check.GetComponent<Toggle>().isOn))
        {
            overideState = OVERRIDE_STATE.ROOMBA1;
        }
        else if (toggleChoice(roomba2check.GetComponent<Toggle>().isOn))
        {
            overideState = OVERRIDE_STATE.ROOMBA2;
        }
        else
        {
            overideState = OVERRIDE_STATE.NONE;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        //if ((goUp) && (!goDown) && (!goRight) && (!goLeft) && (!goStop) && (toggleChoice(roomba1check.GetComponent<Toggle>().isOn)) && (overrideStatus))
        //{
        //    transform.Translate(0.5f * Time.deltaTime * speed, 0f, 0f);
        //}
        
        //else if((goDown) && (!goUp) && (!goRight) && (!goLeft) && (!goStop) && (toggleChoice(roomba1check.GetComponent<Toggle>().isOn)) && (overrideStatus))
        //{
        //    transform.Translate(-0.5f * Time.deltaTime * speed, 0f, 0f);
        //}

        //else if((!goDown) && (!goUp) && (!goRight) && (goLeft) && (!goStop) && (toggleChoice(roomba1check.GetComponent<Toggle>().isOn)) && (overrideStatus))
        //{
        //    transform.Translate(0f, -0.5f * Time.deltaTime * speed, 0f);
        //}

        //else if((!goDown) && (!goUp) && (goRight) && (!goLeft) && (!goStop) && (toggleChoice(roomba1check.GetComponent<Toggle>().isOn)) && (overrideStatus))
        //{
        //    transform.Translate(0f, 0.5f * Time.deltaTime * speed, 0f);
        //}

        //else if ((!goDown) && (!goUp) && (!goRight) && (!goLeft) && (goStop) && (toggleChoice(roomba1check.GetComponent<Toggle>().isOn)) && (overrideStatus))
        //{
        //    transform.Translate(0f * Time.deltaTime * speed, 0f * Time.deltaTime * speed, 0);
        //}
    }

    public bool toggleChoice(bool togs)
    {
        return togs;
    }

}
