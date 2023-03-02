using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArduinoBluetoothAPI;

public class BluetoothControlScript : MonoBehaviour
{
    BluetoothHelper btRoomba1;
    BluetoothHelper btRoomba2;

    // Start is called before the first frame update
    void Start()
    {
        btRoomba1 = BluetoothHelper.GetInstance();
        btRoomba1 = BluetoothHelper.GetInstance();

        btRoomba1.setDeviceName("4.0James_ESP32");
        btRoomba1.setTerminatorBasedStream("\n");

        btRoomba2.setDeviceName("4.0James_ESP32_2");
        btRoomba2.setTerminatorBasedStream("\n");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int connectRoombaOne()
    {
        Debug.Log("Attempting to connect Roomba 1");
        btRoomba1.Connect();
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

    public int connectRoombaTwo()
    {
        Debug.Log("Attempting to connect Roomba 2");
        btRoomba2.Connect();
        if (btRoomba2.isConnected())
        {
            return 1;
        }
        else if (btRoomba2.isDevicePaired())
        {
            return 2;
        }
        return -1;
    }

    public bool isRoombaOneReady()
    {
        return btRoomba1.isConnected();
    }
    public bool isRoombaTwoReady()
    {
        return btRoomba2.isConnected();
    }

    public int sendData(int RoombaNum, char command)
    {
        switch(RoombaNum)
        {
            case 1:
                if (!isRoombaOneReady())
                {
                    connectRoombaOne(); // If not ready, try and make it ready.  
                }
                if (isRoombaOneReady())
                {
                    btRoomba1.SendData(command + "\n");
                    return 1;
                    // Send the Command
                }
                break;
            case 2:
                if (!isRoombaTwoReady())
                {
                    connectRoombaTwo(); // If not ready, try and make it ready.  
                }
                if (isRoombaTwoReady())
                {
                    btRoomba1.SendData(command + "\n");
                    return 1;
                    // Send the Command
                }
                break;
        }
        return 0;
    }
}
