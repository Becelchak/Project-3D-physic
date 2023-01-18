using System;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class IndicatorBehavior : MonoBehaviour
{
    public Text accIndicator;
    private const int scaleGap = 10;
    private const int angleInGap = 18;
    private float initialY;
    private float initialZ;
    private const float indLength = 0.08f;
    private bool accOn = false;
    private int maximum = new Random().Next(85, 95);
    private void Start()
    {
        PlayerPrefs.SetFloat("nullValue", new Random().Next(5, 11));
        PlayerPrefs.SetFloat("accValue", 0);
        initialY = transform.localPosition.y;
        initialZ = transform.localPosition.z;
    }
    private void Update()
    {
        if (!accOn && PlayerPrefs.GetString("accOn") == "true")
        {
            ChangeAccValue();
            accIndicator.text = PlayerPrefs.GetFloat("nullValue").ToString();
            accOn = true;
        } 
    }
    public void ChangeAccValue()
    {
        if (PlayerPrefs.GetString("accOn") == "true")
        {
            var value = (float)(Math.Round(Math.Sin(PlayerPrefs.GetInt("ruporAngle") % 180 * Math.PI / 180) * maximum, 1) + PlayerPrefs.GetFloat("nullValue", 0)) * PlayerPrefs.GetFloat("levelGen");
            var indAngle = value * angleInGap / scaleGap;
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                initialY + (float)Math.Sin(indAngle * Math.PI / 180) * indLength,
                initialZ + indLength - (float)Math.Cos(indAngle * Math.PI / 180) * indLength);
            transform.localRotation = Quaternion.Euler(new Vector3(indAngle, 0, 0));
            PlayerPrefs.SetFloat("accValue", value);
            accIndicator.text = value.ToString();
        }
    }
}
