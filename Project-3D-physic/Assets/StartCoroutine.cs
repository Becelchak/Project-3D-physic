using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCoroutine : MonoBehaviour
{
    public Text textObj;
    public Image imageObj;
    public Sprite[] sprites;
    public string[] phrases;
    public GameObject[] outlinedObjects;
    private bool coroutineStarted = false;
    private bool startOutline = false;
    private bool ended = false;
    private int currentTextIndex = 0;
    private int spriteNum = 0;
    private int outlineNum = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && coroutineStarted && !ended) coroutineStarted = false;
        else if (Input.GetKeyDown(KeyCode.Space) && !coroutineStarted && !ended)
        {
            if (currentTextIndex < phrases.Length - 1)
            {
                if (startOutline)
                {
                    if (outlineNum > 0) outlinedObjects[outlineNum - 1].GetComponent<Outline>().enabled = false;
                    if (outlineNum == outlinedObjects.Length) startOutline = false;
                    else
                    {
                        outlinedObjects[outlineNum].GetComponent<Outline>().enabled = true;
                        outlineNum++;
                    }
                    
                }
                // Изменение картинки робота при проигрывании корутины
                ChangePic();
                // Переход к следующему тексту
                currentTextIndex++;
                Coroutine();
                // Условие начала выделения объектов по рассказу робота
                if (currentTextIndex == 1) startOutline = true;
            }
            else
            {
                // Конец всех текстов в корутине, финальная анимация, начало геймплея
                GetComponent<Animation>().Play("FadeOutGreeting");
                ended = true;
            }
        }
    }

    void ChangePic()
    {
        spriteNum = (spriteNum + 1) % sprites.Length;
        var rightTransform = spriteNum == 0 ? -20f : 0;
        var topTransform = imageObj.rectTransform.offsetMax.y;
        imageObj.rectTransform.offsetMax = new Vector2(rightTransform, topTransform);
        imageObj.sprite = sprites[spriteNum];
    }

    public void Coroutine()
    {
        coroutineStarted = true;
        textObj.text = "";
        StartCoroutine(InvokeCoroutine(phrases[currentTextIndex]));
    }

    IEnumerator InvokeCoroutine(string text)
    {
        foreach (var letter in text)
        {
            if (coroutineStarted)
            {
                textObj.text += letter;
                yield return new WaitForSeconds(0.02f);
            }
            else
            {
                textObj.text = text;
                yield break;
            }
        }
        coroutineStarted = false;
    }

}
