using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class AttackTrigger : MonoBehaviour
{
    public GameObject attackSequence;
    private PlayableDirector director;
    public GameObject blackOutSquare;

    void Start()
    {
        director = attackSequence.GetComponent<PlayableDirector>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            director.Play();
            StartCoroutine(FadeBlackOutSquare());
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, float fadeSpeed = .5f) {
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack) {
            while (blackOutSquare.GetComponent<Image>().color.a < 1) {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor; 
                yield return null; 
            }
        } else {
            while (blackOutSquare.GetComponent<Image>().color.a > 0) {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
    }
}
