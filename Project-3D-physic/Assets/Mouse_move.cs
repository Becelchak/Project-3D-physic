using UnityEngine;

public class Mouse_move : MonoBehaviour
{
    public Camera player;
    private Iteractable previousInteIteractable;

    void Update()
    {
        var ray = player.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, 100)) return;
        var interacteble = hit.collider.GetComponent<Iteractable>();
        if (interacteble != null)
        {
            interacteble.OnHoverEnter();
            var anim = player.GetComponent<Animator>();
            anim.enabled = true;

            switch (!interacteble.GetZoomMode() && interacteble.needZoom)
            {
                case true when Input.GetMouseButton(1):
                    interacteble.Zoom(anim,interacteble.name);
                    break;
                case false when Input.GetMouseButton(0):
                    interacteble.UnZoom(anim, interacteble.name);
                    break;
            }
            // Вращение объекта
            if ((interacteble != this || interacteble != previousInteIteractable) && Input.GetMouseButton(0) && interacteble.needRotate)
            {
                var randomAngle = Random.Range(0.2f, 0.5f);
                interacteble.transform.Rotate(0, 0, randomAngle);
            }
            // Вращение кнопки
            if (interacteble.isButton && Input.GetMouseButton(0)) 
                interacteble.ChangeUseMode();

            if (interacteble.GetUseMode())
            {
                if(Input.GetButton("D"))
                {
                    //interacteble.Rotate(Side.Right);
                    print("Rotate");
                    interacteble.transform.Rotate(0.001f, 0, 0);
                }
                else if (Input.GetButton("A"))
                {
                    //interacteble.Rotate(Side.Left);
                    print("Unrotate");
                    interacteble.transform.Rotate(-0.001f, 0, 0);
                }

            }


            previousInteIteractable = interacteble;
        }
        else if (previousInteIteractable != null && !previousInteIteractable.GetUseMode())
        {
            previousInteIteractable.OnHoverExit();
            previousInteIteractable = null;
        }
    }
    void MouseMove()
    {
        //xRot += Input.GetAxis("Mouse X") * sensivity;
        //yRot += Input.GetAxis("Mouse Y") * sensivity;

        //player.transform.rotation = Quaternion.Euler(-yRot,xRot,0f);
        //playerGameObject.transform.rotation = Quaternion.Euler(0f,xRot,0f);
    }
}
