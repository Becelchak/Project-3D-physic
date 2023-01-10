using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.Progress;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Outline))]
public class Iteractable : MonoBehaviour
{
    private Outline outline;

    public bool needZoom  = false;
    public bool needRotate = false;
    public bool isButton = false;

    public string ExploreTextInfo;
    public string UseTextInfo;

    public int minNum;

    private string UseObjectName = "";

    private bool NowUsed = false;
    private bool ZoomMode = false;

    // Костыль для диапозона частот усилителя
    private int step = 0;
    private List<string> numbers = new List<string>() {"200-1000","250-930","880-3000","2900-10000" };

    private GameObject global_UI;
    private CanvasGroup typeMenuLocal;
    private Button closeMenuButton;

    //Костыль для кнопок
    private Vector3 oldPosItem;

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
        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(delegate { UseItem(global_UI); });

        // Назначение для кнопки "Изучить"
        var exploreButton = typeMenu.gameObject.GetComponentsInChildren<Button>()[1];
        exploreButton.onClick.RemoveAllListeners();
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
        if (UseObjectName == "")
            UseObjectName = gameObject.name;
        var IntAmp = GameObject.Find("IntText").GetComponent<Text>();
        var item = GameObject.Find("Cylinder_right22");
        switch (UseObjectName)
        {
            case "Рупор":
                // Вращение рупора
                if (Input.GetKey(KeyCode.D))
                {
                    var angle = GameObject.Find("AngleText").GetComponent<Text>();
                    var rnd = Random.Range(1,2);

                    var randomAngle = Random.Range(1, 2);
                    transform.Rotate(0, 0, randomAngle);

                    angle.text = int.Parse(angle.text) <= 360 ? (int.Parse(angle.text) + rnd).ToString() : "0";
                }
                break;
            case "Усиление":
                oldPosItem = item.transform.position;
                if (Input.GetKey(KeyCode.D))
                {
                    var rnd = Random.Range(0.45f, 0.51f);

                    //item.transform.eulerAngles += new Vector3(1, 0, 0) * Time.deltaTime;
                    //item.transform.rotation = Quaternion.AngleAxis(0.001f, Vector3.right);
                    item.transform.RotateAround(oldPosItem, Vector3.right,0.1f);
                    IntAmp.text = Math.Round(int.Parse(IntAmp.text) + rnd) > 100 ? "100" : Math.Round(int.Parse(IntAmp.text) + rnd).ToString();
                }
                else if(Input.GetKey(KeyCode.A))
                {
                    var rnd = Random.Range(0.45f, 0.51f);

                    //item.transform.rotation = Quaternion.AngleAxis(-0.001f, Vector3.right);
                    //item.transform.eulerAngles -= new Vector3(1, 0, 0) * Time.deltaTime;
                    item.transform.RotateAround(oldPosItem, Vector3.right, -0.1f);

                    IntAmp.text = Math.Round(int.Parse(IntAmp.text) - rnd) < 20 ? "20" : Math.Round(int.Parse(IntAmp.text) - rnd).ToString();
                }
                break;
            case "Напряжение":
                if (Input.GetKey(KeyCode.D))
                {

                    var rnd = Random.Range(1, 2);

                    IntAmp.text = (int.Parse(IntAmp.text) + rnd) > 1000 ? "1000" : (int.Parse(IntAmp.text) + rnd).ToString();
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    var rnd = Random.Range(1, 2);

                    IntAmp.text = (int.Parse(IntAmp.text) - rnd) < 1 ? "1" : (int.Parse(IntAmp.text) - rnd).ToString();
                }
                break;
            case "Регулировка частоты":
                if (Input.GetKey(KeyCode.D))
                {

                    var rnd = Random.Range(0.45f, 0.51f);

                    IntAmp.text = Math.Round(int.Parse(IntAmp.text) + rnd) > 100 ? "100" : Math.Round(int.Parse(IntAmp.text) + rnd).ToString();
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    var rnd = Random.Range(0.45f, 0.51f);

                    IntAmp.text = Math.Round(int.Parse(IntAmp.text) - rnd) < 0 ? "0" : Math.Round(int.Parse(IntAmp.text) - rnd).ToString();
                }
                break;
            case "Диапозон частот":
                if (Input.GetKeyDown(KeyCode.D))
                {
                    step = step < 3 ? ++step : step;

                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    step = step > 0 ? --step : step;
                }

                IntAmp.text = numbers[step];
                break;
            default:
                break;
        }
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

    //public void Rotate(Side side)
    //{
    //    if (side == Side.Right)
    //    {
    //        if(NowNum++ < MaxNum)
    //        {
    //            NowNum++;
    //            this.transform.Rotate(20f, 0, 0);
    //        }
    //        else
    //        {
    //            NowNum = MaxNum;
    //        }
    //    }
    //    else if (side == Side.Left)
    //    {
    //        if(NowNum-- > 0)
    //        {
    //            NowNum--;
    //            this.transform.Rotate(-20f, 0, 0);
    //        }
    //        else
    //        {
    //            NowNum = 0;
    //        }
    //    }
    //}

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
        var usePanel = GetUsePanel();
        usePanel.alpha = 1;
        usePanel.interactable = true;
        usePanel.blocksRaycasts = true;

        var text = usePanel.GetComponentsInChildren<Text>();
        text[1].text = UseTextInfo;

        var closeButton = usePanel.gameObject.GetComponentInChildren<Button>();
        closeButton.onClick.AddListener(delegate { EndUseItem(usePanel); });

        ClosePanel();
        NowUsed = true;
    }

    void EndUseItem(CanvasGroup usePanel)
    {
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

    public CanvasGroup GetUsePanel()
    {
        switch (this.gameObject.name)
        {
            case "Рупор":
                return GameObject.Find("UsePanelRupor").GetComponent<CanvasGroup>();
            case "Усилитель":
                return GameObject.Find("UsePanelAmplifier").GetComponent<CanvasGroup>();
            case "Излучатель":
                return GameObject.Find("UsePanelGeneration").GetComponent<CanvasGroup>();
            default:
                break;
        }

        return new CanvasGroup();
    }

    //public void CloseAllPanels()
    //{
    //    var panels = GameObject.FindObjectsOfType<CanvasGroup>();
    //    foreach (var panel in panels)
    //    {
    //        panel.alpha = 0;
    //        panel.interactable = false;
    //        panel.blocksRaycasts = false;
    //    }

    //    var buttonPanel = GameObject.Find("UsePanelAmpButtons").GetComponent<CanvasGroup>();
    //    buttonPanel.alpha = 1;
    //    buttonPanel.interactable = true;
    //    buttonPanel.blocksRaycasts = true;

    //}

    public void Reinforcement()
    {
        UseObjectName = "Усиление";

        PrepareApiButtomsPanel();
    }
    public void Voltage()
    {
        UseObjectName = "Напряжение";

        PrepareApiButtomsPanel();

    }
    public void FrequencyСontrol()
    {
        UseObjectName = "Регулировка частоты";

        PrepareApiButtomsPanel();

    }
    public void FrequencyRange()
    {
        UseObjectName = "Диапозон частот";

        PrepareApiButtomsPanel();

    }

    void PrepareApiButtomsPanel()
    {
        var previousPanel = GameObject.Find("UsePanelAmplifier").GetComponent<CanvasGroup>();
        previousPanel.alpha = 0;
        previousPanel.interactable = false;
        previousPanel.blocksRaycasts = false;

        var panel = GameObject.Find("UsePanelAmpButtons").GetComponent<CanvasGroup>();
        panel.alpha = 1;
        panel.interactable = true;
        panel.blocksRaycasts = true;

        var exit = panel.gameObject.GetComponentsInChildren<Button>()[0];
        exit.onClick.RemoveAllListeners();
        exit.onClick.AddListener(delegate { EndUseItem(panel); });
    }

}

//public enum Side
//{
//    Right = 0,
//    Left = 1
//}
