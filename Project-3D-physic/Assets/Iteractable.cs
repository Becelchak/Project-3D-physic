using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class Iteractable : MonoBehaviour
{
    private Outline outline;

    public bool needZoom  = false;
    public bool needRotate = false;
    public bool isButton = false;

    public int MaxNum = 0;
    private int NowNum = 0;

    private bool NowUsed = false;
    private bool ZoomMode = false;

    private GameObject global_UI;
    private Button closeMenuButton;

    // Находим тип меню для определенного интерактивного предмета
    public CanvasGroup GetTypeMenu(CanvasGroup ui_menu, Animator animator, GameObject Global_UI)
    {
        var typeMenu = new CanvasGroup();
        global_UI = Global_UI;

        // Полное меню (Использовать, изучить, приблизить)
        if (needZoom && !isButton)
        {
            typeMenu  = ui_menu.gameObject.GetComponentsInChildren<CanvasGroup>()[2];
        }
        // Урезанное меню без приближения
        else if (!needZoom || isButton)
        {
            typeMenu = ui_menu.gameObject.GetComponentsInChildren<CanvasGroup>()[1];
        }

        // Назначаем функции кнопкам
        animator.enabled = true;

        var exploreButton = typeMenu.gameObject.GetComponentsInChildren<Button>()[1];
        exploreButton.onClick.AddListener(delegate { ExploreItem(global_UI);});

        if (needZoom)
        {
            var buttonMenu = typeMenu.gameObject.GetComponentsInChildren<Button>()[2];
            buttonMenu.onClick.AddListener(delegate { Zoom(animator, name); });
        }

        print("Next");

        closeMenuButton = typeMenu.gameObject.GetComponentsInChildren<Button>()[typeMenu.gameObject.GetComponentsInChildren<Button>().Length - 1];
        return typeMenu;
    }

    private void OnEnable()
    {
        outline = GetComponent<Outline>();
        outline.OutlineWidth = 0;
    }

    public void OnHoverEnter()
    {
        if(!ZoomMode)
            outline.OutlineWidth = 4;
    }

    public void OnHoverExit()
    {
        outline.OutlineWidth = 0;
    }

    public void Zoom(Animator anim,string name)
    {
        ZoomMode = true;

        ChangeUIForZoom(global_UI, anim);
        closeMenuButton.onClick.Invoke();

        var animName = string.Format("{0}Zoom", name);
        anim.StopPlayback();
        anim.Play(animName);

        print("OK");
    }

    public void UnZoom(Animator anim,string name, CanvasGroup exitButtonCanvasGroup)
    {
        ZoomMode = false;

        ChangeUIForUnZoom(global_UI);

        var animName = string.Format("{0}UnZoom", name);
        anim.StopPlayback();
        anim.Play(animName);
    }

    public void Rotate(Side side)
    {
        if (side == Side.Right)
        {
            if(NowNum++ < MaxNum)
            {
                NowNum++;
                this.transform.Rotate(20f, 0, 0);
            }
            else
            {
                NowNum = MaxNum;
            }
        }
        else if (side == Side.Left)
        {
            if(NowNum-- > 0)
            {
                NowNum--;
                this.transform.Rotate(-20f, 0, 0);
            }
            else
            {
                NowNum = 0;
            }
        }
    }

    public void ChangeUseMode()
    {
        NowUsed = !NowUsed;
    }

    public bool GetUseMode()
    {
        return NowUsed;
    }

    public bool GetZoomMode()
    {
        return ZoomMode;
    }

    void ChangeUIForZoom(GameObject ui, Animator animator)
    {
        // Добавляем кнопку выхода из Zoom
        var exit = ui.GetComponentsInChildren<CanvasGroup>()[5];
        exit.alpha = 1;
        exit.interactable = true;
        exit.blocksRaycasts = true;

        // Убираем поворот на доску слева
        var toBoardButton = ui.GetComponentsInChildren<CanvasGroup>()[0];
        toBoardButton.alpha = 0;
        toBoardButton.interactable = false;
        toBoardButton.blocksRaycasts = false;

        var exButton = exit.gameObject.GetComponentInParent<Button>();
        exButton.onClick.AddListener(delegate { UnZoom(animator, name, exit); });
    }

    void ChangeUIForUnZoom(GameObject ui)
    {
        // Убираем кнопку выхода из Zoom
        var exit = ui.GetComponentsInChildren<CanvasGroup>()[5];
        exit.alpha = 0;
        exit.interactable = false;
        exit.blocksRaycasts = false;

        // Добавляем поворот на доску слева
        var toBoardButton = ui.GetComponentsInChildren<CanvasGroup>()[0];
        toBoardButton.alpha = 1;
        toBoardButton.interactable = true;
        toBoardButton.blocksRaycasts = true;

    }

    void ExploreItem(GameObject ui)
    {
        var explorePanel = ui.GetComponentsInChildren<CanvasGroup>()[6];
        explorePanel.alpha = 1;
        explorePanel.interactable = true;
        explorePanel.blocksRaycasts = true;

        var closeButton = explorePanel.gameObject.GetComponentInChildren<Button>();
        closeButton.onClick.AddListener(delegate { EndExploreItem(ui); });
    }

    void EndExploreItem(GameObject ui)
    {
        var explorePanel = ui.GetComponentsInChildren<CanvasGroup>()[6];
        explorePanel.alpha = 0;
        explorePanel.interactable = false;
        explorePanel.blocksRaycasts = false;

    }
}

public enum Side
{
    Right = 0,
    Left = 1
}
