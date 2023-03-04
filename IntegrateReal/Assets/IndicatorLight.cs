using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorLight : MonoBehaviour
{
    private Color green = new Color(0f, 255f, 0f);
    private Color darkRed = new Color(80f/255, 5f/255, 5f/255);
    public Button overButton;
    private bool overStatus = false;
    // Start is called before the first frame update
    void Start()
    {
        overButton.onClick.AddListener(OnLight);
    }

    void OnLight()
    {
        overStatus = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (overStatus)
        {
            GetComponent<Image>().color = green;
            
        }
        else
        {
            GetComponent<Image>().color = darkRed;
        }
    }
}
