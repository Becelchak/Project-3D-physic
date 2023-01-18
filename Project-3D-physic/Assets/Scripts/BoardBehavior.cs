using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehavior : MonoBehaviour
{
    public Camera main;
    private void OnMouseDown()
    {
        PlayerPrefs.SetString("curZoom", name + "Zoom");
        PlayerPrefs.SetString("curUnZoom", name + "UnZoom");
        main.GetComponent<Animation>().Play(PlayerPrefs.GetString("curZoom"));
        GetComponent<Outline>().enabled = false;
        GetComponent<OutlineBehavior>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
