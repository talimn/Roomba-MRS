using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCapsule1 : MonoBehaviour
{
    public GameObject capsule; //references capsule1
    private Vector3 offset = new Vector3(-4, 5, 7); //camera position offset
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = capsule.transform.position + offset;
    }
}
