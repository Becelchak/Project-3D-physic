using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotsWords : MonoBehaviour
{
    public AudioClip[] words;
    public AudioSource speaker;
    private bool task1 = false;

    void Start()
    {
        PlayerPrefs.SetInt("curWord", 0);
    }

    void Update()
    {
        if (!task1 && PlayerPrefs.GetString("genOn") == "true" && PlayerPrefs.GetString("accOn") == "true")
        {
            ChangeTask();
            task1 = true;
        }
    }

    public void ChangeTask()
    {
        SetNextWord();
        PlayWord();
    }

    public void PlayWord()
    {
        speaker.clip = words[PlayerPrefs.GetInt("curWord")];
        speaker.Play();
    }

    public void SetNextWord()
    {
        int nextrWord = PlayerPrefs.GetInt("curWord") + 1;
        if (nextrWord == words.Length) return;
        PlayerPrefs.SetInt("curWord", nextrWord);
    }
}
