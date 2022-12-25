using UnityEditor;
using UnityEngine;

public class Mouse_move : MonoBehaviour
{
    public Camera player;
    private Iteractable previousInteIteractable;
    public GameObject UI_player;

    private CanvasGroup UI_menu;
    private CanvasGroup nowUseMenu;
    
    //private bool needMove = true;

    private Animator anim;

    void Start()
    {
        UI_menu = UI_player.GetComponentsInChildren<CanvasGroup>()[2];
        anim = player.GetComponent<Animator>();
        anim.enabled = true;
    }
    void Update()
    {
        //Следование меню за курсором
        //MovePanel();

        var ray = player.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, 90)) return;
        var interacteble = hit.collider.GetComponent<Iteractable>();
        if (interacteble != null)
        {
            //UpdateUseMode(interacteble);

            interacteble.OnHoverEnter();

            if ((interacteble != this || interacteble != previousInteIteractable) && Input.GetMouseButton(1))
            {
                nowUseMenu = interacteble.GetTypeMenu(UI_menu, anim, UI_player);

                UI_menu.alpha = 1;
                UI_menu.interactable = true;

                nowUseMenu.alpha = 1;
                nowUseMenu.blocksRaycasts = true;
                nowUseMenu.interactable = true;

                UI_menu.transform.position = Input.mousePosition;

            }

            //var anim = player.GetComponent<Animator>();
            //anim.enabled = true;

            //switch (!interacteble.GetZoomMode() && interacteble.needZoom)
            //{
            //    case true when Input.GetMouseButton(1):
            //        interacteble.Zoom(anim,interacteble.name);
            //        break;
            //    case false when Input.GetMouseButton(0):
            //        interacteble.UnZoom(anim, interacteble.name);
            //        break;
            //}
            //// Вращение объекта
            //if ((interacteble != this || interacteble != previousInteIteractable) && Input.GetMouseButton(0) && interacteble.needRotate)
            //{
            //    var randomAngle = Random.Range(0.2f, 0.5f);
            //    interacteble.transform.Rotate(0, 0, randomAngle);
            //}
            //// Вращение кнопки
            //if (interacteble.isButton && Input.GetMouseButton(0)) 
            //    interacteble.ChangeUseMode();

            //if (interacteble.GetUseMode())
            //{
            //    if(Input.GetButton("D"))
            //    {
            //        //interacteble.Rotate(Side.Right);
            //        print("Rotate");
            //        interacteble.transform.Rotate(0.001f, 0, 0);
            //    }
            //    else if (Input.GetButton("A"))
            //    {
            //        //interacteble.Rotate(Side.Left);
            //        print("Unrotate");
            //        interacteble.transform.Rotate(-0.001f, 0, 0);
            //    }

            //}


            previousInteIteractable = interacteble;
        }
        else if (previousInteIteractable != null)
        {
            previousInteIteractable.OnHoverExit();
            previousInteIteractable = null;
        }
    }

    //private void UpdateUseMode(Iteractable interacteble)
    //{
    //    if (!interacteble.GetUseMode()) return;
    //    switch (interacteble.gameObject.name)
    //    {
    //        case "Рупор":
    //            // Вращение объекта
    //            if (Input.GetMouseButton(0) &&
    //                interacteble.needRotate)
    //            {
    //                var randomAngle = Random.Range(0.2f, 0.5f);
    //                interacteble.transform.Rotate(0, 0, randomAngle);
    //            }

    //            break;
    //        default:
    //            break;
    //    }
    //}

    void MouseMove()
    {
        //xRot += Input.GetAxis("Mouse X") * sensivity;
        //yRot += Input.GetAxis("Mouse Y") * sensivity;

        //player.transform.rotation = Quaternion.Euler(-yRot,xRot,0f);
        //playerGameObject.transform.rotation = Quaternion.Euler(0f,xRot,0f);
    }

    //void MovePanel()
    //{
    //    if(needMove)
    //        UI_menu.transform.position = Input.mousePosition;
    //}

    public void ClosePanel()
    {
        UI_menu.alpha = 0;
        UI_menu.interactable = false;

        nowUseMenu.alpha = 0;
        nowUseMenu.blocksRaycasts = false;
        nowUseMenu.interactable = false;

        //needMove = true;
    }

}
