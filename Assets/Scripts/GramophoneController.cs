using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GramophoneController : MonoBehaviour
{
    AudioSource audio;

    private bool isPlaying = false;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Play() {
        audio.Play();
        Invoke("EnableDisabling", 5);

    }

    public void Stop() {
        audio.Stop();
        isPlaying = false;
    }

    private void EnableDisabling() {
        isPlaying = true;
    }

    public bool IsPlaying() {
        return isPlaying;
    }
}
