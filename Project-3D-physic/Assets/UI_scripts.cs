using UnityEngine;

public class UI_scripts : MonoBehaviour
{
    public Camera camera;
    private string position = "Table";

    public void ChangePosition()
    {
        var anim = camera.GetComponent<Animator>();
        anim.enabled = true;
        switch (position)
        {
            case "Table":
                print("Left");
                anim.StopPlayback();
                anim.Play("FrontToBoard");

                position = "Board";
                break;
            case "Board":
                print("Right");
                anim.StopPlayback();
                anim.Play("FrontToTable");

                position = "Table";
                break;
        }
    }
}
