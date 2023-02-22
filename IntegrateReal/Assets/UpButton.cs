using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpButton : MonoBehaviour
{
    public Button m_UpButton, m_DownButton, m_LeftButton, m_RightButton, m_StopButton;
    public GameObject rmba;
    private float speed = 5.0f;
    public string inputID;
    private float horizontalInput;
    private float forwardInput;

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
        Debug.Log("You are definitely, certainly moving up!");
        transform.Translate(0.05f, 0.0f, 0f);
    }

    void TaskDown()
    {
        Debug.Log("You are moving down!");
        transform.Translate(-0.05f, 0f, 0f);
    }

    void TaskLeft()
    {
        Debug.Log("You are moving left!");
        transform.Translate(0f, -0.5f, 0f);
    }

    void TaskRight()
    {
        Debug.Log("You are moving right!");
        transform.Translate(0f, 0.5f, 0f);
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
