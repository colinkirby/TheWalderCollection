using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSoundController : MonoBehaviour 
{
    public AudioClip[] clips;
    public AudioClip death;
    private bool deathPlayed = false;

    private AudioSource audioSource;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Step() {
        if(!deathPlayed) {
            audioSource.Stop();
            AudioClip clip = GetRandomClip();
            audioSource.PlayOneShot(clip);
        }
    }

    public void Death() {
        if(!deathPlayed) {
            audioSource.Stop();
            audioSource.PlayOneShot(death);
            deathPlayed = true;
        }
    }

    private AudioClip GetRandomClip() {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }
}
