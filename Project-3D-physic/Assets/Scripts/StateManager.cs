using System.Linq;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public GameObject[] relatedObjects;

    void Start()
    {
        PlayerPrefs.SetString("position", "Table");
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    public void Toggle(string objName)
    {
        var item = relatedObjects.Where(x => x.name == objName).FirstOrDefault();
        item.SetActive(!item.activeSelf);
    }

    public void PlayAnimation(string nameObj_nameAnim)
    {
        var elems = nameObj_nameAnim.Split(';');
        string objName = elems[0], animName = elems[1];
        var obj = relatedObjects.Where(x => x.name == objName).FirstOrDefault();
        obj.TryGetComponent<Animation>(out var anim);
        anim.Play(animName);
    }

    public void SetPos(string pos)
    {
        PlayerPrefs.SetString("position", pos);
    }
}
