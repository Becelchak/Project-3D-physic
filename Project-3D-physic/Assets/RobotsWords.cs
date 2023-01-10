using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotsWords : MonoBehaviour
{
    public AudioClip[] words;
    public AudioSource speaker;

    private void Start()
    {
        PlayerPrefs.SetInt("curWord", 0);
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
