using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject blackOutSquare;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            StartCoroutine(FadeBlackOutSquare());
        }
        else if (Input.GetKeyDown(KeyCode.L)) {
            StartCoroutine(FadeBlackOutSquare(false));
        }
    }

    IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed = 5) {
        print("coroutine called");
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack) {
            print("fading to black");
            while (blackOutSquare.GetComponent<Image>().color.a < 1) {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor; 
                yield return null; 
            }
        } else {
            print("fading from black");
            while (blackOutSquare.GetComponent<Image>().color.a > 0) {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
    }
}
