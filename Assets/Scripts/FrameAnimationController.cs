using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameAnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    public void DisableAnimator() {
        print(this.GetComponent<Animator>().enabled);
        this.GetComponent<Animator>().enabled = false;
        print(this.GetComponent<Animator>().enabled);

    }
}
