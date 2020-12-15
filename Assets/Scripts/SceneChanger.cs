using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;
    public GameObject sceneSequenceManager;

    // Update is called once per frame
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) {
            sceneSequenceManager.GetComponent<SceneSequence>().Play();
        }
    }

    public void FadeToScene() {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete() {
        SceneManager.LoadScene("MainScene");
    }
}
