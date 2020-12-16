using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneChanger : MonoBehaviour
{
    public Animator animator;
    public GameObject sceneSequenceManager;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) {
            sceneSequenceManager.GetComponent<SceneSequence>().Play();

        }
    }

    public void FadeToScene() {
        //animator.SetTrigger("FadeOut");
        sceneSequenceManager.GetComponent<SceneSequence>().Play();     
    }
    
    public void OnFadeComplete() {
        SceneManager.LoadScene("MainScene");
    }
}
