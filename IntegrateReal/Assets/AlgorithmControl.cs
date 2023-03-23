using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (x > 50)
        {
            // Turn 45 degrees and go forward
            btControls.sendData(1, 'd');
            btControls.sendData(1, 'w');
        }

        else if (x > 100)
        {
            // Turn -90 degrees and go forward
            btControls.sendData(1, 'a');
            btControls.sendData(1, 'a');
            btControls.sendData(1, 'w');
        }

        else if(x > 150)
        {
            // Stop
            btControls.sendData(1, 's');
        }

        x++; // Increment x
    }
}
