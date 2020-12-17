using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneChanger : MonoBehaviour
{
    public Animator animator;
    public GameObject sceneSequenceManager;
    
    // Shortcut to cut scene
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            sceneSequenceManager.GetComponent<SceneSequence>().Play();
        }
    }

    public void FadeToScene() {
        sceneSequenceManager.GetComponent<SceneSequence>().Play();   
    }
}
