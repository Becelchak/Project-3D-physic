using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_move : MonoBehaviour
{
    float xRot;
    float yRot;
    public Camera player;
    public GameObject playerGameObject;
    public float sensivity = 5f;

    // Update is called once per frame
    void Update()
    {
        MouseMove();
    }

    void MouseMove()
    {
        xRot += Input.GetAxis("Mouse X") * sensivity;
        yRot += Input.GetAxis("Mouse Y") * sensivity;

        player.transform.rotation = Quaternion.Euler(-yRot,xRot,0f);
        playerGameObject.transform.rotation = Quaternion.Euler(0f,xRot,0f);
    }
}
