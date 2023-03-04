using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocDataListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMessageArrived(string msg)
    {
        Debug.Log("Location data: " + msg);
    }

    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device Connected.": "Device Disconnected");
    }
}
