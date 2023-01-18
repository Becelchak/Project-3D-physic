using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TurningEquipment : MonoBehaviour
{
    public GameObject tumbler;
    public Material turnedOn;
    public GameObject lightOn;
    public TextMeshPro[] indicators;
    public float[] XYZ;
    public float[] AnglesXYZ;
    private Vector3 originalAngles;
    private Vector3 originalXYZ;
    private Material originalMat;

    void Start()
    {
        originalAngles = transform.localRotation.eulerAngles;
        originalXYZ = transform.localPosition;
        originalMat = lightOn.GetComponent<MeshRenderer>().material;
        PlayerPrefs.SetString("genOn", "false");
        PlayerPrefs.SetString("accOn", "false");
        PlayerPrefs.SetString("nullOn", "false");
        foreach (var elem in indicators) elem.enabled = false;
    }
    private void OnMouseDown()
    {
        var status = name.Contains("left") ? "genOn" : name.Contains("right") ? "accOn" : "nullOn";
        if (status == "nullOn" && PlayerPrefs.GetString(status) == "true") TurnOff(status);
        else TurnOn(status);
    }
    private void OnMouseUp()
    {
        if (!name.Contains("null"))
        {
            GetComponent<MeshCollider>().enabled = false;
            GetComponent<OutlineBehavior>().enabled = false;
            GetComponent<Outline>().enabled = false;
        }
    }

    private void TurnOn(string status)
    {
        tumbler.transform.localRotation = Quaternion.Euler(AnglesXYZ[0], AnglesXYZ[1], AnglesXYZ[2]);
        tumbler.transform.localPosition = new Vector3(XYZ[0], XYZ[1], XYZ[2]);
        lightOn.GetComponent<MeshRenderer>().material = turnedOn;

        foreach (var elem in indicators) elem.enabled = true;
        PlayerPrefs.SetString(status, "true");

    }

    private void TurnOff(string status)
    {
        tumbler.transform.localPosition = originalXYZ;
        tumbler.transform.localRotation = Quaternion.Euler(originalAngles);
        lightOn.GetComponent<MeshRenderer>().material = originalMat;
        PlayerPrefs.SetString(status, "false");
    }
}
