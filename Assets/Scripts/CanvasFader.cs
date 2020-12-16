using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFader : MonoBehaviour
{

    private bool faded = false;
    public float duration = 10f;
    

    void Start()
    {
        Fade();
    }

    public void Fade()
    {
        var canvGroup = GetComponent<CanvasGroup>();

        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, faded ? 1 : 0));

        faded = !faded;
    }

    public IEnumerator DoFade(CanvasGroup canvGroup, float start, float end)
    {
        float counter = 0f;
        while(counter < duration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter/duration);

            yield return null;
        }
    }
}
