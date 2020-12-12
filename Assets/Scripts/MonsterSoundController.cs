using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSoundController : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioClip death;

    private AudioSource audioSource;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Step() {
        audioSource.Stop();
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    private void Death() {
        audioSource.Stop();
        audioSource.PlayOneShot(death);
    }

    private AudioClip GetRandomClip() {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }
}
