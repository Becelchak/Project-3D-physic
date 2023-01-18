using Random = System.Random;
using UnityEngine;

public class NullTumblerBehavior : MonoBehaviour
{
    private int state = 0;
    private int nullPoint = new Random().Next(0, 4);
    public Vector3[] positions;
    public GameObject indicator;

    private void OnMouseDown()
    {
        if (PlayerPrefs.GetString("accOn") == "true" && PlayerPrefs.GetString("nullOn") == "true")
        {
            state = (state + 1) % 4;
            transform.localPosition = positions[state];
            if (state == 0) transform.Rotate(new Vector3(0, -180, 0));
            else transform.Rotate(new Vector3(0, 60, 0));
            if (nullPoint == state) PlayerPrefs.SetFloat("nullValue", 0);
            else PlayerPrefs.SetFloat("nullValue", new Random().Next(1, 3));
            indicator.GetComponent<IndicatorBehavior>().ChangeAccValue();
        }
    }
}
