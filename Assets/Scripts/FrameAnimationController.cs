using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameAnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    public Transform pos;
    public GameObject child;

    public void Start() {
        anim = GetComponent<Animator>();
        pos = GetComponent<Transform>();
        child = pos.Find("bells").gameObject;

    }

    public void PlayFrameAnim() {
        float wallAngleY = pos.rotation.eulerAngles.y;
        int rounded = Mathf.RoundToInt(wallAngleY);
        string animName = "none";
        switch(rounded) {
            case 0:
                animName = "Painting_Shake_2";
                anim.Play(animName);
                break;
            case 180:
                animName = "Painting_Shake";
                anim.Play(animName);
                break;
            case 90:
                animName = "Painting_Shake_4";
                anim.Play(animName);
                break;
            case 270:
                animName = "Painting_Shake_3";
                anim.Play(animName);
                break;
        }
        PlayPaintingSounds();

        
    }

    private void PlayPaintingSounds() {
        // bells
        child.GetComponent<AudioSource>().Play();

        

    }
}