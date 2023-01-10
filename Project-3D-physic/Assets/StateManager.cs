using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class StateManager : MonoBehaviour
{
    public GameObject[] relatedObjects;
    private string position = "Table";
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    public void Toggle(string objName)
    {
        var item = relatedObjects.Where(x => x.name == objName).FirstOrDefault();
        item.SetActive(!item.activeSelf);
    }

    public void ToggleInteractableCanvas(string objName)
    {
        var item = relatedObjects.Where(x => x.name == objName).FirstOrDefault();
        if (item.TryGetComponent<CanvasGroup>(out var elem))
        {
            elem.interactable = !elem.interactable;
        }
    }

    public void PlayAnimation(string nameObj_nameAnim)
    {
        var elems = nameObj_nameAnim.Split(';');
        string objName = elems[0], animName = elems[1];
        var obj = relatedObjects.Where(x => x.name == objName).FirstOrDefault();
        obj.TryGetComponent<Animation>(out var anim);
        
        if (obj == gameObject)
        {
            switch (position)
            {
                case "Table":
                    if (animName == "FrontToBoard")
                    {
                        anim.Play(animName);
                        position = "Board";
                    }
                    break;
                case "Board":
                    if (animName == "FrontToTable")
                    {
                        anim.Play(animName);
                        position = "Table";
                    }
                    break;
            }
        }
        else anim.Play(animName);
    }
}
