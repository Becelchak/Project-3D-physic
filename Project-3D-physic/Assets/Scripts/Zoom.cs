using UnityEngine;

public class Zoom : MonoBehaviour
{
    public void ZoomAnim()
    {
        var anim = PlayerPrefs.GetString("curZoom");
        if (anim is not null) GetComponent<Animation>().Play(anim);
    }

    public void UnZoomAnim()
    {
        var anim = PlayerPrefs.GetString("curUnZoom");
        if (anim is not null) GetComponent<Animation>().Play(anim);
    }
}
