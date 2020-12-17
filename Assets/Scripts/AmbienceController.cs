using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceController : MonoBehaviour
{
    private AudioSource audioSource;

    public GameObject player;
    public GameObject monster;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        float dist = GetDistance();
        AdjustVolume(dist);
    }

    private float GetDistance() {
        float dist = Vector3.Distance(player.transform.position, monster.transform.position);
        return dist;
    }

    private void AdjustVolume(float distance) {
        if (distance > 100f) { 
            distance = 100f; 
        }

        float val = (100f - distance) / 100f;
        val = (val * val) / 2;
        audioSource.volume = val;
    }
}
