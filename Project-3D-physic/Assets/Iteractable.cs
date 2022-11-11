using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class Iteractable : MonoBehaviour
{
    private Outline outline;

    private void OnEnable()
    {
        outline = GetComponent<Outline>();
        outline.OutlineWidth = 0;
    }

    public void OnHoverEnter()
    {
        outline.OutlineWidth = 4;
    }

    public void OnHoverExit()
    {
        outline.OutlineWidth = 0;
    }
}
