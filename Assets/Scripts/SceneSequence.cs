using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSequence : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;

    public Camera mainCamera;

    public GameObject car;
    public GameObject canvas1;
    public GameObject canvas2;

    public GameObject titleCanvas;

   

    public GameObject canvas3;
    private Transform pos;

    private Animator cam1Anim;

    private Animator cam2Anim;

    private Animator carAnim;

    private Animator fader;



    void Start() {
        cam1 = cam1.GetComponent<Camera>();
        cam2 = cam2.GetComponent<Camera>();
        mainCamera = mainCamera.GetComponent<Camera>();
        pos = GetComponent<Transform>();
        

        cam1Anim = pos.Find("SceneCamera1").GetComponent<Animator>();
        cam2Anim = pos.Find("SceneCamera2").GetComponent<Animator>();
        fader = canvas3.GetComponent<Animator>();
        carAnim = car.GetComponent<Animator>();

    }
    public void Play()
    {
        canvas1.SetActive(false);


        StartCoroutine ( Sequence());
    }

    // Update is called once per frame
    IEnumerator Sequence() {
        // yield return new WaitForSeconds(2);
        canvas2.SetActive(false);
        cam1.enabled = true;
        mainCamera.enabled = false;
        fader.Play("FadeIn2");
        cam1Anim.Play("cam1");
        carAnim.Play("car");
        yield return new WaitForSeconds(8);

        titleCanvas.SetActive(false);
        cam2.enabled = true;
        cam1.enabled = false;
        cam2Anim.Play("cam2");
        yield return new WaitForSeconds(8);
        fader.Play("FadeOut2");
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("MainScene");
        
        yield break;
    }
}
