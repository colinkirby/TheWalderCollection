using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GramophoneController : MonoBehaviour
{
    private AudioSource gramAudio;
    private bool isPlaying = false;

    [System.Serializable] public class GramEvent : UnityEvent<Transform> {}
    [SerializeField] public GramEvent gramEvent;

    void Start()
    {
        gramAudio = GetComponent<AudioSource>();
    }

    public void Play() {
        gramEvent.Invoke(GetComponent<Transform>());
        gramAudio.Play();
        Invoke("EnableDisabling", 8);
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
