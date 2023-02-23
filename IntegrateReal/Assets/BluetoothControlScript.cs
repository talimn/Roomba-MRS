using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArduinoBluetoothAPI;

public class BluetoothControlScript : MonoBehaviour
{
    BluetoothHelper helper;
    // Start is called before the first frame update
    void Start()
    {
        helper = BluetoothHelper.GetInstance();
        helper.setDeviceName("4.0James_ESP32");
        helper.setTerminatorBasedStream("\n");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Attempting to connect");
        helper.Connect();
        Debug.Log("Device Paired: " + helper.isDevicePaired());
        Debug.Log("Device Connected: " + helper.isConnected());

        helper.SendData("j\n");
    }
}
