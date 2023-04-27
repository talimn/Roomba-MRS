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

    // Variables for added start button
    private bool canStart = false;
    private int startX = 0;

    // Variables for pathfinding purposes
    private int startY = 0;
    private int startZ = 0;
    private int startW = 0;
    private int startV = 0;
    private int startC = 0;
    private float yTime = 0f;
    private float zTime = 0f;
    private float wTime = 0f;
    private int obstacleCounter = 0;
    private float obstacleTime = 0f;
    private float currentTime = 0f;

    // Obstacle Detection
    int sensorNumber;
    int sensorOneCounter = 0;
    int sensorTwoCounter = 0;
    int sensorFourCounter = 0;
    int sensorFiveCounter = 0;
    bool obstacleDetectedStatus = false;
    int counterOne = 0;
    int counterTwo = 0;

    // Localization
    int upCounter = 0;
    int bottomCounter = 0;

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
                overideState = OVERRIDE_STATE.NONE;
                btControls.sendData(1, 's');
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

    bool top = false;
    bool bot = true;

    void Update()
    {
        
        // PATHFINDING
        if (overideState == OVERRIDE_STATE.NONE && canStart)
        {
            //Debug.Log("ON PATH");
            // Obstacle Detection
            /*if (Min() <= 210.0 && btControls.locationArray_3F[1] != 120.0 && btControls.locationArray_3F[1] != 0.0) //CHANGE TO INCHES!
            {
                switch (sensorNumber)
                {
                    case 1:

                        break;
                    case 2:
                        break;
                    case 3:
                        btControls.sendData(1, 'd');
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                }
            }

            else
            {
                startX += 1;
                if (startX == 1)
                {
                    btControls.sendData(1, 'w');
                }

                // If reaching top of square
                else if (btControls.locationArray_3F[1] == 120.0) // CHANGE TO INCHES!
                {
                    btControls.sendData(1, 'd');
                    btControls.sendData(1, '%');
                    btControls.sendData(1, 'd');
                    btControls.sendData(1, 'w');
                }

                // If reaching bottom of square
                else if (btControls.locationArray_3F[1] == 0.0)
                {
                    btControls.sendData(1, 'a');
                    btControls.sendData(1, '%');
                    btControls.sendData(1, 'a');
                    btControls.sendData(1, 'w');
                }
            }*/

            if (btControls.locationArray_3F[1] >= 0)
            {
                startX += 1;
                Min();
                /*if (startX == 1)
                {
                    btControls.sendData(1, 'w');
                }*/
                /*if (obstacleDetectedStatus == true & sensorNumber != 4 & btControls.locationArray_3F[0] <= 40.0)
                {
                    btControls.sendData(1, 's');
                    counterTwo += 1;
                }*/

                //PATHFINDING ONLY
                if (startX == 1)
                {
                    btControls.sendData(1, 'z');
                    
                    btControls.sendData(1, 'w');
                }

                
                /*else if (btControls.locationArray_3F[0] < 50.0 & obstacleDetectedStatus == false & counterOne == 0 & counterTwo > 0)
                {
                    counterOne += 1;
                    btControls.sendData(1, 'w');
                }*/


                else if (btControls.locationArray_3F[0] >= 50.0 & startY == 0)
                {
                    startY += 1;
                    yTime = Time.fixedTime + 5.0f;
                    btControls.sendData(1, 's');
                    btControls.sendData(1, 'z');
                    //btControls.sendData(1, 'w');
                }

                else if (btControls.locationArray_3F[0] >= 50.0 & startZ == 0 & Time.fixedTime >= yTime & yTime > 0f)
                {
                    startZ += 1;
                    zTime = Time.fixedTime + 4.5f;
                    btControls.sendData(1, 'w');
                }

                else if(btControls.locationArray_3F[0] >= 65.0 & startW == 0 & Time.fixedTime >= zTime & zTime > 0f)
                {
                    startW += 1;
                    wTime = Time.fixedTime + 5.0f;
                    btControls.sendData(1, 's');
                    btControls.sendData(1, 'd');
                }

                else if (btControls.locationArray_3F[0] >= 65.0 & startV == 0 & Time.fixedTime >= wTime & wTime > 0f)
                {
                    startV += 1;
                    btControls.sendData(1, 'w');
                }

                else if (btControls.locationArray_3F[1] >= 75.0)
                {
                    btControls.sendData(1, 's');
                }
                
                /*else if (btControls.locationArray_3F[0] >= 60.0 & startZ == 0)
                {
                    startZ += 1;
                    btControls.sendData(1, 's');
                    btControls.sendData(1, 'd');
                    btControls.sendData(1, 'w');
                }*/

                /*else if (btControls.locationArray_3F[1] >= 80.0)
                {
                    btControls.sendData(1, 's');
                }*/
                // rough obstacle detection
                if (Min() <= 210.0)
                {
                    Debug.LogWarning("OBSTACLE DETECTED BY SENSOR " + sensorNumber + "!");
                }




                // CLEANING ONLY
                // If reaching top of square
                /*else if (btControls.locationArray_3F[1] >= 50.0 & !top) // CHANGE TO INCHES!
                {
                    bot = false;
                    top = true;
                    btControls.sendData(1, 's');
                    btControls.sendData(1, 'a');
                    btControls.sendData(1, 's');
                    btControls.sendData(1, '%');
                    btControls.sendData(1, 's');
                    btControls.sendData(1, '%');
                    btControls.sendData(1, 's');
                    btControls.sendData(1, 'a');
                    btControls.sendData(1, 's');
                    btControls.sendData(1, 'w');
                }

                else if (btControls.locationArray_3F[1] >= 50.0 & top)
                {
                    // Keep moving down
                }

                // If reaching bottom of square
                else if (btControls.locationArray_3F[1] <= 20.0 & !bot)
                {
                    bot = true;
                    top = false;
                    btControls.sendData(1, 's');
                    btControls.sendData(1, 'd');
                    btControls.sendData(1, 's');
                    btControls.sendData(1, '%');
                    btControls.sendData(1, 's');
                    btControls.sendData(1, '%');
                    btControls.sendData(1, 's');
                    btControls.sendData(1, 'd');
                    btControls.sendData(1, 's');
                    btControls.sendData(1, 'w');
                }

                else if (btControls.locationArray_3F[1] <= 30.0 & bot)
                {
                    // Keep moving up
                }*/
            }
            
        }

        else if (overideState == OVERRIDE_STATE.ROOMBA1)
        {
            //Debug.Log("OVERRIDE");
            // Obstacle Detection
            if (Min() <= 150.0)
            {
                Debug.LogWarning("Obstacle Detected by Sensor " + sensorNumber);
            }
        }

        // Localization
        switch (btControls.locationArray_3F[1])
        {
            case 3: //CHANGE TO INCHES!
                upCounter += 1;
                break;
            case 0: 
                bottomCounter += 1;
                break;
        }

        if (btControls.dataDirty)
        {
            // bottom left corner of square (x-y axis) is origin
            Debug.Log("Location Data: X:" + btControls.locationArray_3F[0] + " Y:" + btControls.locationArray_3F[1] + " Z:" + btControls.locationArray_3F[2]);
            // sensor values in mm, want distance for obstacle detection to be 210 mm
            Debug.Log("Sensor Data: 1: " + btControls.sensorArray_5F[0] + " 2: " + btControls.sensorArray_5F[1] + " 3: " + btControls.sensorArray_5F[2] + " 4: " + btControls.sensorArray_5F[3] + " 5: " + btControls.sensorArray_5F[4]);
            //Debug.Log("Minimum distance: " + Min());
            
            btControls.dataDirty = false;
        }
    }

    // This is to run a Time.fixedTime timer in the upper left corner of game play screen
    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 100), (Time.fixedTime).ToString());
    }

    // Roomba selected for override
    public bool toggleChoice(bool togs)
    {
        return togs;
    }

    // If obstacle detected in range equal to or less than min, change status to true
    double Min()
    {
        
        double min = btControls.sensorArray_5F[0];
        sensorNumber = -1;

        for (int i = 0; i < 5; i++)
        {
            double number = btControls.sensorArray_5F[i];

            if (number < min)
            {
                sensorNumber = i + 1;
                min = number;
                
            }

            else
            {
                sensorNumber = sensorNumber + 1;
            }
        }
        
        if (min < 300)
        {
            obstacleDetectedStatus = true;
        }

        else
        {
            obstacleDetectedStatus = false;
        }

        return min;
    }

    
}
