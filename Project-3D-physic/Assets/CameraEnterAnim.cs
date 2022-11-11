using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEnterAnim : MonoBehaviour
{
    //Stop anim
    void StopThis()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<Animator>().StopPlayback();
    }
}
