using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpButton : MonoBehaviour
{
    public Button m_UpButton, m_DownButton, m_LeftButton, m_RightButton, m_StopButton;
    
    void Start()
    {

        m_UpButton.onClick.AddListener(TaskUp);
        m_DownButton.onClick.AddListener(TaskDown);
        m_LeftButton.onClick.AddListener(TaskLeft);
        m_RightButton.onClick.AddListener(TaskRight);
        m_StopButton.onClick.AddListener(TaskStop);
    }

    void TaskUp()
    {
        Debug.Log("You are moving up!");
    }

    void TaskDown()
    {
        Debug.Log("You are moving down!");
    }

    void TaskLeft()
    {
        Debug.Log("You are moving left!");
    }

    void TaskRight()
    {
        Debug.Log("You are moving right!");
    }

    void TaskStop()
    {
        Debug.Log("You have stopped!");
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
}
