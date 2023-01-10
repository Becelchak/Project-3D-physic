using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class OutlineBehavior : MonoBehaviour
{
    Outline outline;
    void Start()
    {
        outline = GetComponent<Outline>();
        outline.OutlineWidth = 6;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        outline.enabled = true;
    }

    void OnMouseExit()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        outline.enabled = false;
    }
}
