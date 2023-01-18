using System;
using TMPro;
using UnityEngine;

public class ChangeGeneratorValue : MonoBehaviour
{
    public TextMeshPro Indicator;
    public GameObject accIndicator;

    void Start()
    {
        var value = float.Parse(Indicator.text);
        PlayerPrefs.SetFloat("frequency", value);
        PlayerPrefs.SetFloat("levelGen", value);
    }

    void OnMouseDown()
    {
        if (PlayerPrefs.GetString("genOn") == "false") return;
        var value = float.Parse(Indicator.text);
        switch (name)
        {
            case "���������, �������":
                if (value - 50 < 0) break;
                else
                {
                    value -= 50;
                    Indicator.text = $"{Math.Round(value, 0)}";
                }
                break;
            case "���������, �������":
                if (value + 50 > 10000) break;
                else
                {
                    value += 50;
                    Indicator.text = $"{Math.Round(value, 0)}";
                }
                break;
            case "���������, �������":
                if (value - 0.1< 0) break;
                else
                {
                    value -= 0.1f;
                    Indicator.text = $"{Math.Round(value, 1)}";
                }
                break;
            case "���������, �������":
                if (value + 0.1 > 1) break;
                else
                {
                    value += 0.1f;
                    Indicator.text = $"{Math.Round(value, 1)}";
                }
                break;
        }
        if (name.Contains("�������")) PlayerPrefs.SetFloat("frequency", value);
        else if (name.Contains("�������")) PlayerPrefs.SetFloat("levelGen", value);
        accIndicator.GetComponent<IndicatorBehavior>().ChangeAccValue();
    }
}
