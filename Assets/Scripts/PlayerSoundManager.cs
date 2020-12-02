using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerController player;
    private bool isWalking;
    private bool isRunning;
    private bool isSneaking;

    public AudioClip walking;
    public AudioClip running;
    public AudioClip sneaking;

    private AudioSource audioClip;

    void Start()
    {
        player = this.transform.parent.GetComponent<PlayerController>();
        audioClip = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetState();
        
        if((isWalking || isRunning || isSneaking) && !audioClip.isPlaying) {
            PlayAudio();
        }
    }

    void GetState() {
        if((Input.GetAxisRaw("Horizontal") != 0f) || Input.GetAxisRaw("Vertical") != 0f) {
            isWalking = true;
            if(player.speed == 6f) {
                isWalking = false;
                isSneaking = false;
                isRunning = true;
            } else if(player.speed == 1.5f) {
                isWalking = false;
                isRunning = false;
                isSneaking = true;
            }
        } else {
            audioClip.Stop();
            isWalking = false;
            isRunning = false;
            isSneaking = false;
        }
    }

    void PlayAudio() {
        if(isWalking) {
            audioClip.clip = walking;
            audioClip.Play();
        } else if(isRunning) {
            audioClip.clip = running;
            audioClip.Play();
        } else if(isSneaking) {
            audioClip.clip = sneaking;
            audioClip.Play();
        }
    }
}
