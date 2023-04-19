using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArduinoBluetoothAPI;

public class BluetoothControlScript : MonoBehaviour
{
    BluetoothHelper btRoomba1;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            btRoomba1 = BluetoothHelper.GetInstance("4.0James_ESP32");
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
            Debug.Log(btRoomba1.Read());
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
