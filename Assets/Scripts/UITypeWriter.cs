﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System;
using Random=UnityEngine.Random;

public class UITypeWriter: MonoBehaviour
{
    public Text text;
    public bool playOnAwake = true;
    public float delayToStart;
    public float delayBetweenChars = 0.0125f;
    public float delayAfterPunctuation = 0.4f;

    private string story;
    private float originDelayBetweenChars;
    private bool lastCharPunctuation = false;

    private char lastCharacter;

    private bool endOfSentence = false;
    private char charComma;
    private char charPeriod;

    private char charNewLine;
    public AudioClip typing;
    public AudioClip ding;
    public AudioClip[] typeOneShots;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        text = GetComponent<Text>();
        originDelayBetweenChars = delayBetweenChars;

        charComma = Convert.ToChar(44);
        charPeriod = Convert.ToChar(46);
        charNewLine = Convert.ToChar(10);

        if (playOnAwake)
        {
            ChangeText(text.text, delayToStart);
        }
     }

    //Update text and start typewriter effect
    public void ChangeText(string textContent, float delayBetweenChars = 0f)
    {
        StopCoroutine(PlayText()); //stop Coroutine if exist
        story = textContent;
        text.text = ""; //clean text
        Invoke("Start_PlayText", delayBetweenChars); //Invoke effect
    }

    void Start_PlayText()
    {
        StartCoroutine(PlayText());
    }

    void PlayRandom() {
         audioSource.clip = typeOneShots[Random.Range(0, typeOneShots.Length)];
         audioSource.Play();
    }

    IEnumerator PlayText()
    {

        //audioClip.Play();

        foreach (char c in story)
        {
            delayBetweenChars = originDelayBetweenChars;

            if (lastCharPunctuation)  //If previous character was a comma/period, pause typing
            {
                //audioClip.Stop();
                //audioClip.clip = typing;
                yield return new WaitForSeconds(delayBetweenChars = delayAfterPunctuation);
                lastCharPunctuation = false;
                //audioClip.Play();
            }

            if (endOfSentence)
            {
                //audioClip.Stop();
                //audioClip.clip = ding;
                yield return new WaitUntil(() => Input.anyKeyDown);
                //audioClip.Play();
                audioSource.clip = ding;
                audioSource.Play();
                yield return new WaitWhile (()=> audioSource.isPlaying);

                endOfSentence = false;
            }

            if (c == charComma || c == charPeriod)
            {
                lastCharPunctuation = true;
            }

            if (c == charNewLine)
            {
                if (lastCharacter != charNewLine)
                    endOfSentence = true;
            }

            text.text += c;
            lastCharacter = c;
            if(lastCharacter != charNewLine) {
                PlayRandom();
                yield return new WaitWhile (()=> audioSource.isPlaying);
            }

            //yield return new WaitForSeconds(delayBetweenChars);
        }
    }
}