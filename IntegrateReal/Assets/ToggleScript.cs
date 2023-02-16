using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour
{
    public GameObject toggler;
    void Start()
    {
        print(toggler.GetComponent<Toggle>().isOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void userToggle(bool tog)
    {
        print("Selected Roomba 1: " + tog);
    }
}
