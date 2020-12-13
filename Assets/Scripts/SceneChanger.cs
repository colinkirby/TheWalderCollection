using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;
 

    // Update is called once per frame
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
