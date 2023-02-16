using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

    //THIS IS A SIMPLE SCRIPT FOR THE CAMERA ORBIT


    public Transform lookAt;
    public Transform camTransform;
    public float distance = 10.0f;

    private float X = 0f;
    private float Y = -45f;
    public float sensitivityX = 4.0f;
    public float sensitivityY = 1.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //get the mouse input into the x and y variables
        X = X += Input.GetAxis("Mouse X") * sensitivityX;
        Y = Y += Input.GetAxis("Mouse Y") * sensitivityY;

        //change the camera distance with the mouse scroll wheel input
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            distance += 0.1f;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && distance > 1f)
        {
            distance -= 0.1f;
        }
    }

     private void LateUpdate()
     {
         //create a Vector3 called dir, and set the distance
         Vector3 dir = new Vector3(0, 0, -distance);

         //change the rotation
         Quaternion rotation = Quaternion.Euler(-Y, X, 0f);

         //makes the camera look the target
         camTransform.position = lookAt.position + rotation * dir;
         camTransform.LookAt(lookAt.position);
     }
}