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

    public string ExploreTextInfo;
    public string UseTextInfo;

    public int MaxNum = 0;
    private int NowNum = 0;

    private bool NowUsed = false;
    private bool ZoomMode = false;

    private GameObject global_UI;
    private CanvasGroup typeMenuLocal;
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

        typeMenuLocal = typeMenu;

        // Назначаем функции кнопкам
        animator.enabled = true;

        // Назначение для кнопки "Использовать"
        var useButton = typeMenu.gameObject.GetComponentsInChildren<Button>()[0];
        useButton.onClick.AddListener(delegate { UseItem(global_UI); });
        // Назначение для кнопки "Изучить"
        var exploreButton = typeMenu.gameObject.GetComponentsInChildren<Button>()[1];
        exploreButton.onClick.AddListener(delegate { ExploreItem(global_UI);});

        // Назначение для кнопки "Приблизить"
        if (needZoom)
        {
            var buttonMenu = typeMenu.gameObject.GetComponentsInChildren<Button>()[2];
            buttonMenu.onClick.AddListener(delegate { Zoom(animator, name); });
        }

        print("Next");

        // Кнопка закрытия меню взаимодействия
        closeMenuButton = typeMenu.gameObject.GetComponentsInChildren<Button>()[typeMenu.gameObject.GetComponentsInChildren<Button>().Length - 1];
        return typeMenu;
    }

    void Update()
    {
        if (!GetUseMode()) return;
        switch (gameObject.name)
        {
            case "Рупор":
                // Вращение объекта
                if (Input.GetKey(KeyCode.D))
                {
                    var angle = GameObject.Find("AngleText").GetComponent<Text>();
                    var rnd = Random.Range(1,2);

                    var randomAngle = Random.Range(1, 2);
                    transform.Rotate(0, 0, randomAngle);

                    angle.text = int.Parse(angle.text) <= 360 ? (int.Parse(angle.text) + rnd).ToString() : "0";
                }

                break;
            default:
                break;
        }
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
        // Вывод описания объекта
        var explorePanel = ui.GetComponentsInChildren<CanvasGroup>()[6];
        explorePanel.alpha = 1;
        explorePanel.interactable = true;
        explorePanel.blocksRaycasts = true;

        var text = GameObject.Find("TextDescription").GetComponent<Text>();
        text.text = ExploreTextInfo;

        var closeButton = explorePanel.gameObject.GetComponentInChildren<Button>();
        closeButton.onClick.AddListener(delegate { EndExploreItem(ui); });

        ClosePanel();
    }

    void EndExploreItem(GameObject ui)
    {
        // Закрытие панели с описанием объекта
        var explorePanel = ui.GetComponentsInChildren<CanvasGroup>()[6];
        explorePanel.alpha = 0;
        explorePanel.interactable = false;
        explorePanel.blocksRaycasts = false;
    }

    void UseItem(GameObject ui)
    {
        // Вывод меню с подсказкой к действию
        var usePanel = ui.GetComponentsInChildren<CanvasGroup>()[8];
        usePanel.alpha = 1;
        usePanel.interactable = true;
        usePanel.blocksRaycasts = true;

        var text = GameObject.Find("UseText").GetComponent<Text>();
        text.text = UseTextInfo;

        var closeButton = usePanel.gameObject.GetComponentInChildren<Button>();
        closeButton.onClick.AddListener(delegate { EndUseItem(ui); });

        ClosePanel();
        NowUsed = true;
    }

    void EndUseItem(GameObject ui)
    {
        var usePanel = ui.GetComponentsInChildren<CanvasGroup>()[8];
        usePanel.alpha = 0;
        usePanel.interactable = false;
        usePanel.blocksRaycasts = false;

        NowUsed = false;
    }

    public void ClosePanel()
    {
        var ui_menu = typeMenuLocal.transform.parent.gameObject.GetComponentInParent<CanvasGroup>();
        ui_menu.alpha = 0;
        ui_menu.interactable = false;

        typeMenuLocal.alpha = 0;
        typeMenuLocal.blocksRaycasts = false;
        typeMenuLocal.interactable = false;
    }
}

public enum Side
{
    Right = 0,
    Left = 1
}
