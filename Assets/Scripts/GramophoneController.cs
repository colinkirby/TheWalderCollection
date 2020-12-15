using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GramophoneController : MonoBehaviour
{
    AudioSource gramAudio;

    private bool isPlaying = false;
    void Start()
    {
        gramAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Play() {
        gramAudio.Play();
        Invoke("EnableDisabling", 5);

    }

    public void Stop() {
        gramAudio.Stop();
        isPlaying = false;
    }

    private void EnableDisabling() {
        isPlaying = true;
    }

    public bool IsPlaying() {
        return isPlaying;
    }
}
