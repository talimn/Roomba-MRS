using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpButton : MonoBehaviour
{
    public Button m_UpButton, m_DownButton, m_LeftButton, m_RightButton, m_StopButton, m_OverrideButton;
    public GameObject rmba;
    public GameObject toggler;
    

    private float speed = 5.0f;
    private bool goUp = false;
    private bool goDown = false;
    private bool goLeft = false;
    private bool goRight = false;
    private bool goStop = false;
    private bool overrideStatus = false;
    

    void Start()
    {

        m_UpButton.onClick.AddListener(TaskUp);
        m_DownButton.onClick.AddListener(TaskDown);
        m_LeftButton.onClick.AddListener(TaskLeft);
        m_RightButton.onClick.AddListener(TaskRight);
        m_StopButton.onClick.AddListener(TaskStop);
        m_OverrideButton.onClick.AddListener(TaskOverride);
    }

    void TaskUp()
    {
        Debug.Log("You are definitely, certainly moving up!");
        goUp = true;
        goDown = false;
        goLeft = false;
        goRight = false;
        goStop = false;
        Update();
    }

    void TaskDown()
    {
        Debug.Log("You are moving down!");
        goDown = true;
        goUp = false;
        goLeft = false;
        goRight = false;
        goStop = false;
    }

    void TaskLeft()
    {
        Debug.Log("You are moving left!");
        goLeft = true;
        goUp = false;
        goDown = false;
        goRight = false;
        goStop = false;
    }

    void TaskRight()
    {
        Debug.Log("You are moving right!");
        goRight = true;
        goUp = false;
        goDown = false;
        goLeft = false;
        goStop = false;
    }

    void TaskStop()
    {
        Debug.Log("You have stopped!");
        goStop = true;
        goUp = false;
        goDown = false;
        goLeft = false;
        goRight = false;
    }

    void TaskOverride()
    {
        Debug.Log("Override clicked!");
        overrideStatus = true;
    }

    

    // Update is called once per frame
    void Update()
    {
        if ((goUp) && (!goDown) && (!goRight) && (!goLeft) && (!goStop) && (toggleChoice(toggler.GetComponent<Toggle>().isOn)) && (overrideStatus))
        {
            transform.Translate(0.05f * Time.deltaTime * speed, 0f, 0f);
        }
        
        else if((goDown) && (!goUp) && (!goRight) && (!goLeft) && (!goStop) && (toggleChoice(toggler.GetComponent<Toggle>().isOn)) && (overrideStatus))
        {
            transform.Translate(-0.05f * Time.deltaTime * speed, 0f, 0f);
        }

        else if((!goDown) && (!goUp) && (!goRight) && (goLeft) && (!goStop) && (toggleChoice(toggler.GetComponent<Toggle>().isOn)) && (overrideStatus))
        {
            transform.Translate(0f, -0.5f * Time.deltaTime * speed, 0f);
        }

        else if((!goDown) && (!goUp) && (goRight) && (!goLeft) && (!goStop) && (toggleChoice(toggler.GetComponent<Toggle>().isOn)) && (overrideStatus))
        {
            transform.Translate(0f, 0.5f * Time.deltaTime * speed, 0f);
        }

        else if ((!goDown) && (!goUp) && (!goRight) && (!goLeft) && (goStop) && (toggleChoice(toggler.GetComponent<Toggle>().isOn)) && (overrideStatus))
        {
            transform.Translate(0f * Time.deltaTime * speed, 0f * Time.deltaTime * speed, 0);
        }
    }

    public bool toggleChoice(bool togs)
    {
        return togs;
    }

}
