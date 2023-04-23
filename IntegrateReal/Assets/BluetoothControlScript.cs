using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArduinoBluetoothAPI;

public class BluetoothControlScript : MonoBehaviour
{
    BluetoothHelper btRoomba1;

    public double[] sensorArray_5F = { 0.0, 0.0, 0.0, 0.0, 0.0 };
    public double[] locationArray_3F = { 0.0, 0.0, 0.0 };
    public bool dataDirty = true;

    string input;
    string[] sensors;
    string[] locations;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            btRoomba1 = BluetoothHelper.GetInstance("5.0James_ESP32");
            //btRoomba2 = BluetoothHelper.GetInstance("4.0James_ESP32_2");

            btRoomba1.setTerminatorBasedStream("\n");
            //btRoomba2.setTerminatorBasedStream("\n");

            btRoomba1.OnDataReceived += DataRecieved;
            btRoomba1.OnConnected += WhenConnected;

            if (btRoomba1.isDevicePaired())
            {
                btRoomba1.Connect();
                if (btRoomba1.isConnected())
                {
                    btRoomba1.StartListening();
                }
            }

            //if (btRoomba2.isDevicePaired())
            //{
            //    btRoomba2.Connect();
            //    if (btRoomba2.isConnected())
            //    {
            //        btRoomba2.StartListening();
            //    }
            //}
        }
        catch (BluetoothHelper.BlueToothNotEnabledException ex)
        {
            Debug.LogWarning("BT Not Enabled Ex");
        }
        catch(BluetoothHelper.BlueToothNotReadyException ex)
        {
            Debug.LogWarning("BT Not Ready Ex");
        }
        catch (BluetoothHelper.BlueToothNotSupportedException ex)
        {
            Debug.LogWarning("BT Not Supported Ex");
        }
    }

    // Update is called once per frame
    void Update()
    {
        dataDirty = false;
    }

    public int connectRoombaOne()
    {
        Debug.Log("Attempting to connect Roomba 1");
        if (btRoomba1.isConnected())
        {
            return 1;
        }

        btRoomba1.Connect();
        btRoomba1.StartListening();

        if (btRoomba1.isConnected())
        {
            return 1;
        }
        else if (btRoomba1.isDevicePaired())
        {
            return 2;
        }
        
        return -1;
    }

    public bool isRoombaOneReady()
    {
        return btRoomba1.isConnected();
    }

    public int sendData(int RoombaNum, char command)
    {
        switch(RoombaNum)
        {
            case 1:
                if (!isRoombaOneReady())
                {
                    connectRoombaOne(); // If not ready, try and make it ready.  
                    btRoomba1.StartListening();
                }
                if (isRoombaOneReady())
                {
                    btRoomba1.SendData(command + "\n");
                    return 1;
                    // Send the Command
                }
                Debug.LogWarning("Roomba 1 not Ready. Cannot send Command.");
                break;
        }
        return 0;
    }

    void DataRecieved()
    {
        if (btRoomba1.Available)
        {
            dataDirty = true;
            input = btRoomba1.Read();

            switch(input.Substring(0,4))
            {
                case "POS:":
                    Debug.Log("Roomba 1: Retrieved Location Data.");
                    input = input.Remove(0, 4);
                    locations = input.Split(',');
                    if (locations[0].Equals("N/A"))
                        break;
                    locationArray_3F[0] = double.Parse(locations[0]);
                    locationArray_3F[1] = double.Parse(locations[1]);
                    locationArray_3F[2] = double.Parse(locations[2]);
                    break;

                case "Sens":
                    Debug.Log("Roomba 1: Retrieved Sensor Data.");
                    input = input.Remove(0, 9);
                    sensors = input.Split(',');
                    sensorArray_5F[0] = double.Parse(sensors[0]);
                    sensorArray_5F[1] = double.Parse(sensors[1]);
                    sensorArray_5F[2] = double.Parse(sensors[2]);
                    sensorArray_5F[3] = double.Parse(sensors[3]);
                    sensorArray_5F[4] = double.Parse(sensors[4].Trim('.'));
                    break;

                case "Succ":
                    Debug.Log("Roomba 1: " + input);
                    break;
            }
        }
    }

    void WhenConnected()
    {
        btRoomba1.StartListening();
    }

    void OnDestroy()
    {
        btRoomba1.Disconnect();
    }
}
