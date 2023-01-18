using UnityEngine;
using UnityEngine.UI;

public class LabFunctioning : MonoBehaviour
{
    public Text angleIndicator;
    public Text acceleratorIndicator;
    public GameObject indicator;
    public RobotsWords tasks;

    private void Start()
    {
        PlayerPrefs.SetInt("ruporAngle", 0);
    }
    void Update()
    {
        var value = int.Parse(angleIndicator.text);
        if (PlayerPrefs.GetString("genOn") == "true" 
            && PlayerPrefs.GetString("accOn") == "true"
            && PlayerPrefs.GetString("position") == "Рупор" 
            && Input.GetKey(KeyCode.D))
        {
            value = value < 359 ? value + 1 : 0;
            transform.Rotate(0, 0, 1);
            angleIndicator.text = value.ToString();
            PlayerPrefs.SetInt("ruporAngle", value);
            indicator.GetComponent<IndicatorBehavior>().ChangeAccValue();
            
        }
        else if (PlayerPrefs.GetString("genOn") == "true"
            && PlayerPrefs.GetString("accOn") == "true"
            && PlayerPrefs.GetString("position") == "Рупор"
            && Input.GetKey(KeyCode.A))
        {
            value = value > 0 ? value - 1 : 359;
            transform.Rotate(0, 0, -1);
            angleIndicator.text = value.ToString();
            PlayerPrefs.SetInt("ruporAngle", value);
            indicator.GetComponent<IndicatorBehavior>().ChangeAccValue();
        }
        else if (PlayerPrefs.GetString("position") == "Рупор" && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A)))
        {
            tasks.PlayWord();
        }
    }
}
