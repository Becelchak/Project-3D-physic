using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public void Toggle()
    {
        if (!TryGetComponent<CanvasGroup>(out var elem)) return;
        elem.interactable = !elem.interactable;
        elem.alpha = elem.alpha == 1 ? 0 : 1;
        elem.blocksRaycasts = !elem.blocksRaycasts;
    }

    public void TurnOff()
    {
        if (!TryGetComponent<CanvasGroup>(out var elem)) return;
        elem.interactable = false;
        elem.alpha = 0;
        elem.blocksRaycasts = false;
    }
}
