using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator animator;
 
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            FadeToScene();
        }
    }

    public void FadeToScene() {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete() {
        SceneManager.LoadScene("MainScene");
    }
}
