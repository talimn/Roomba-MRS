using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*Summary of script functionality:

Below is some information on how to edit and use this script for path testing
on the Roomba.

Quadrant Representation:
    4 | 1
   -------
    3 | 2

The Roomba is heading straight at an angle of 0 degrees at the line
between quadrants 1 and 4. To turn directly right, the Roomba must turn +90
degrees by sending the right turn command twice. 
It's ending rotation will be at the line between quadrants 1 and 2.

 */


public class AlgorithmControl : MonoBehaviour
{
    //OVERRIDE
    public Button overrideControl;
    private int overridePressCounter = 0;

    // Will allow us to send commands directly to Roomba
    public BluetoothControlScript btControls;

    // Start is called before the first frame update
    void Start()
    {
        overrideControl.onClick.AddListener(TaskOver);

        // Go Forward
        btControls.sendData(1, 'w');
    }

    void TaskOver()
    {
        overridePressCounter += 1;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 100), (1.0f / Time.smoothDeltaTime).ToString());
    }

    // Update is called once per frame
    void Update()
    {

        if (overridePressCounter % 2 == 0)
        {
            if (Time.fixedTime == 5.0f)
            {

                btControls.sendData(1, 'w');
                
            }

            else if (Time.fixedTime == 10.0f)
            {
                btControls.sendData(1, 's');
            }

            else if (Time.fixedTime == 20.0f)
            {
                btControls.sendData(1, 'a');
                btControls.sendData(1, 'd');
                btControls.sendData(1, 'd');
                btControls.sendData(1, 'd');
                btControls.sendData(1, 'w');
            }

            else if (Time.fixedTime > 30.0f)
            {
                btControls.sendData(1, 's');
            }
        }

        else
        {
            btControls.sendData(1, 's');
        }
        
    }
}
