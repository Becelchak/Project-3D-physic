using UnityEditor;
using UnityEngine;

public class Mouse_move : MonoBehaviour
{
    public Camera player;
    private Iteractable previousInteIteractable;
    public GameObject UI_player;

    private CanvasGroup UI_menu;
    private CanvasGroup nowUseMenu;

    private Animator anim;

    void Start()
    {
        UI_menu = UI_player.GetComponentsInChildren<CanvasGroup>()[2];
        anim = player.GetComponent<Animator>();
        anim.enabled = true;
    }
    void Update()
    {
        var ray = player.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, 90)) return;
        var interacteble = hit.collider.GetComponent<Iteractable>();
        if (interacteble != null)
        {

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

            previousInteIteractable = interacteble;
        }
        else if (previousInteIteractable != null)
        {
            previousInteIteractable = null;
        }
    }

}
