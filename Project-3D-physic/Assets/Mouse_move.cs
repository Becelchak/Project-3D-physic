using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Mouse_move : MonoBehaviour
{
    public Camera player;

    private Iteractable previousInteIteractable;

    void Update()
    {
        Ray ray = player.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, 100)) return;
        var interacteble = hit.collider.GetComponent<Iteractable>();
        if (interacteble != null)
        {
            if ((interacteble == this || interacteble == previousInteIteractable) && !Input.GetMouseButton(0)) return;
            print("Enter");
            interacteble.OnHoverEnter();
            interacteble.transform.Rotate(0, 0, 1f);

            //interacteble.transform.RotateAround(interacteble.transform.position, transform.forward,Time.deltaTime * 20f);

            //var lookPos = interacteble.target.transform.position - interacteble.transform.position;
            //Quaternion lookRot = Quaternion.LookRotation(lookPos);
            //lookRot.eulerAngles = new Vector3(lookRot.eulerAngles.x, interacteble.transform.rotation.eulerAngles.y, interacteble.transform.rotation.eulerAngles.z);
            //interacteble.transform.rotation = Quaternion.Slerp(interacteble.transform.rotation, lookRot, Time.deltaTime * 1.2f);

            previousInteIteractable = interacteble;
        }
        else if (previousInteIteractable != null)
        {
            print("Exit");
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
