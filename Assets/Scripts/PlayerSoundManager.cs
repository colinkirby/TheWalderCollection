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

    public enum MovementType {
        NotMoving,
        Walking,
        Running,
        Sneaking
    }

    private MovementType currentMovementType;

    void Start()
    {
        player = this.transform.parent.GetComponent<PlayerController>();
        audioClip = gameObject.GetComponent<AudioSource>();
    }

    // Enum
    void Update()
    {
        GetState();
        
        if(currentMovementType != MovementType.NotMoving && !audioClip.isPlaying) {
            PlayAudio();
        }
    }

    void GetState() {
        bool isMovingHorizontal = Input.GetAxisRaw("Horizontal") != 0f;
        bool isMovingVertical = Input.GetAxisRaw("Vertical") != 0f;

        if(isMovingHorizontal || isMovingVertical) {
            if(player.speed == 6f) {
                currentMovementType = MovementType.Running;
            } else if(player.speed == 3f) {
                currentMovementType = MovementType.Walking;
            } else {
                currentMovementType = MovementType.Sneaking;
            }
        } else {
            audioClip.Stop();
            currentMovementType = MovementType.NotMoving;
        }
    }

    void PlayAudio() {

        if(currentMovementType == MovementType.Walking) {
            audioClip.clip = walking;
        } else if(currentMovementType == MovementType.Running) {
            audioClip.clip = running;
        } else if(currentMovementType == MovementType.Sneaking) {
            audioClip.clip = sneaking;
        }
        audioClip.Play();

    }
}
