using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectsBehavior : MonoBehaviour
{
    public GameObject motionPanel;
    public Text infoText;
    public string info;
    private bool isOn = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (isOn)
            {
                PlayerPrefs.SetString("curZoom", name + "Zoom");
                PlayerPrefs.SetString("curUnZoom", name + "UnZoom");
                GetComponent<Outline>().enabled = false;
                motionPanel.transform.position = Input.mousePosition;
                motionPanel.GetComponent<TogglePanel>().Toggle();
                if (infoText is not null) infoText.text = info;
            }
            else
            {
                motionPanel.GetComponent<TogglePanel>().TurnOff();
            }
        }
    }

    private void OnMouseEnter()
    {
        isOn = true;
    }

    private void OnMouseExit()
    {
        isOn = false;
    }
}
