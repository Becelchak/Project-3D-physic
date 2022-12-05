using UnityEngine;
using UnityEngine.UI;

public class UI_scripts : MonoBehaviour
{
    public Camera camera = new Camera();
    private string NowLook = "Table";
    private CanvasGroup rightCanvasGroup;
    private CanvasGroup leftCanvasGroup;

    void Start()
    {
        leftCanvasGroup = GetComponentsInChildren<CanvasGroup>()[0];
        rightCanvasGroup = GetComponentsInChildren<CanvasGroup>()[1];
    }

    public void LookBoard()
    {
        if (NowLook != "Table") return;
        var anim = camera.GetComponent<Animator>();
        anim.enabled = true;

        HideComponent(leftCanvasGroup);
        ShowComponent(rightCanvasGroup);

        print("Left");
        anim.StopPlayback();
        anim.Play("Board");
        NowLook = "Board";

    }

    public void LookTable()
    {
        if(NowLook != "Board") return;
        var anim = camera.GetComponent<Animator>();
        anim.enabled = true;

        HideComponent(rightCanvasGroup);
        ShowComponent(leftCanvasGroup);

        print("Right");
        anim.StopPlayback();
        anim.Play("Table");
        NowLook = "Table";
    }

    private void HideComponent(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;

    }

    private void ShowComponent(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }
}
