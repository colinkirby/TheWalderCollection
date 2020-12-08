using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System;

//this script provided by ChazM on GitHub. Posted December 9, 2019

public class TypeWriterEffect : MonoBehaviour
{
public TextMeshProUGUI text;
public bool playOnAwake = true;
public float delayToStart;
public float delayBetweenChars = 0.05f;
public float delayAfterPunctuation = 0.4f;
private string story;
private float originDelayBetweenChars;
private bool lastCharPunctuation = false;
private char charComma;
private char charPeriod;
private char charEmpty;
public GameObject AudioTyping;
private AudioSource TyppingFX;

void Awake()
{
    //TypingFX = GetComponent<AudioSource>();
    //TypingFX.clip = AudioTyping.GetComponent<AudioSource>().clip;

    text = GetComponent<TextMeshProUGUI>();
    originDelayBetweenChars = delayBetweenChars;

    charComma = Convert.ToChar(44);
    charPeriod = Convert.ToChar(46);
    charEmpty = Convert.ToChar(" ");//Convert.ToChar(255);

    if (playOnAwake)
    {
        ChangeText(text.text, delayToStart);
    }
}

//Update text and start typewriter effect
public void ChangeText(string textContent, float delayBetweenChars = 0f)
{
    StopCoroutine(PlayText()); //stop Coroutime if exist
    story = textContent;
    text.text = ""; //clean text
    Invoke("Start_PlayText", delayBetweenChars); //Invoke effect
}

void Start_PlayText()
{
    StartCoroutine(PlayText());
}

IEnumerator PlayText()
{

    foreach (char c in story)
    {
        delayBetweenChars = originDelayBetweenChars;

        if (lastCharPunctuation)  //If previous character was a comma/period, pause typing
        {
            TyppingFX.Pause();
            yield return new WaitForSeconds(delayBetweenChars = delayAfterPunctuation);
            lastCharPunctuation = false;
        }

        if ( c == charEmpty || c == charComma || c == charPeriod  )
        {
          TyppingFX.Pause();
          lastCharPunctuation = true;
        }

        TyppingFX.PlayOneShot(TyppingFX.clip,0.3f);
        text.text += c;
        yield return new WaitForSeconds(delayBetweenChars);
    }
}
}
