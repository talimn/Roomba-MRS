using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    // This variable will keep track of how many frames have passed
    private int x = 0; 
    
    // Will allow us to send commands directly to Roomba
    public BluetoothControlScript btControls;

    // Start is called before the first frame update
    void Start()
    {
        // Go Forward
        btControls.sendData(1, 'w');
    }

    
    // Update is called once per frame
    void Update()
    {
        if (x == 50)
        {
            // Turn +45 degrees and go forward
            btControls.sendData(1, 'd');
            btControls.sendData(1, 'w');
        }

        else if (x == 100)
        {
            // Turn -90 degrees and go forward
            btControls.sendData(1, 'a');
            btControls.sendData(1, 'a');
            btControls.sendData(1, 'w');
        }

        else if(x == 150)
        {
            // Stop movement
            btControls.sendData(1, 's');
        }

        else if (x == 200)
        {
            // Turn -135 degrees and go forward (backward basically)
            btControls.sendData(1, 'a');
            btControls.sendData(1, 'a');
            btControls.sendData(1, 'a');
            btControls.sendData(1, 'w');
        }

        else if (x == 250)
        {
            // Turn -45 degrees and go forward
            btControls.sendData(1, 'a');
            btControls.sendData(1, 'w');
        }

        else if (x == 300)
        {
            // Turn +90 degrees and go forward
            btControls.sendData(1, 'd');
            btControls.sendData(1, 'd');
            btControls.sendData(1, 'w');
        }

        else if (x == 350)
        {
            // Turn -45 degrees and stop movement
            btControls.sendData(1, 'a');
            btControls.sendData(1, 's');
        }


        // This will be the count used for timing the commands
        if (x < 400)
        {
            x++; // Increment x a specified number of times
        }
        
        
    }
}
