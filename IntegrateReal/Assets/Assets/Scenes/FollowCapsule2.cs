using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCapsule2 : MonoBehaviour
{
    public GameObject capsule2; //references capsule1
    private Vector3 offset1 = new Vector3(40, 5, -1); //camera position offset
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = capsule2.transform.position + offset1;
    }
}
