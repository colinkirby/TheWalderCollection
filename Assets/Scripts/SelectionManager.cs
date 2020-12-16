using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using static FirstRunDetector;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private string notSelectableTag = "NotSelectable";
    [SerializeField] private string plaqueTag = "Plaque";
    [SerializeField] private string gramophoneTag = "Gramophone";
    [SerializeField] private string gramophoneTriggerTag = "SelectableGramophoneTrigger";

    [System.Serializable] public class SelectEvent : UnityEvent<string> {}
    [SerializeField] public SelectEvent selectEvent;

    [System.Serializable] public class BellsEvent : UnityEvent<Transform> {}
    [SerializeField] public BellsEvent bellsEvent;

    private Sprite[] spriteArray;

    public Canvas canvas;
    public GameObject instructionLabel;
    public GameObject buttonLabel;

    public GameObject gramophone;
    public Image plaque;
    public Image buttonBackground;

    private bool tutorialDone = false;

    void Start() {
        spriteArray = Resources.LoadAll<Sprite>("Plaques");
    }
    
    void Update()
    {
        RayCastObject();
    }

    void RayCastObject() {
        if (tutorialDone || !FirstRunDetector.firstRun) {
            var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 3)) {
                var selection = hit.transform;
                
                if(selection.CompareTag(selectableTag)) {
                    DestroyPainting(selection, false);
                } else if(selection.CompareTag(plaqueTag)) {
                    TogglePlaque(selection.name);
                } else if(selection.CompareTag(notSelectableTag)) {
                    IncorrectPainting(selection);
                } else if(selection.CompareTag(gramophoneTag)) {
                    StopGramophone(selection);
                } else if(selection.CompareTag(gramophoneTriggerTag)) {
                    DestroyPainting(selection, true);
                }
                else {
                    if(canvas.enabled) {
                        canvas.enabled = false;
                        plaque.enabled = false;
                    }
                }
            } else {
                if(canvas.enabled) {
                    canvas.enabled = false;
                    plaque.enabled = false;
                }
            }
        }
    }

    void DestroyPainting(Transform selection, bool gramophoneTrigger) {
        canvas.enabled = true;
        instructionLabel.GetComponent<TMP_Text>().text = "Take Painting";
        buttonLabel.GetComponent<TMP_Text>().text = "E";
        buttonBackground.enabled = true;
        if(Input.GetKeyDown(KeyCode.E)) {
            if(gramophoneTrigger) {
                gramophone.GetComponent<GramophoneController>().Play();
            }
            Destroy(selection.gameObject);
            selectEvent.Invoke(selection.name);
        }
    }

    void IncorrectPainting(Transform selection) {
        canvas.enabled = true;
        instructionLabel.GetComponent<TMP_Text>().text = "Take Painting";
        buttonLabel.GetComponent<TMP_Text>().text = "E";
        buttonBackground.enabled = true;
        if(Input.GetKeyDown(KeyCode.E)) {
            selection.gameObject.GetComponent<FrameAnimationController>().PlayFrameAnim();
            bellsEvent.Invoke(selection);
        }
    }

    void TogglePlaque(string name) {
        if(plaque.enabled == false){
            canvas.enabled = true;
            buttonLabel.GetComponent<TMP_Text>().text = "E";
            buttonBackground.enabled = false;
            instructionLabel.GetComponent<TMP_Text>().text = "Click to Read";
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            instructionLabel.GetComponent<TMP_Text>().text = "";
            buttonLabel.GetComponent<TMP_Text>().text = "";
            buttonBackground.enabled = false;
            foreach(var sprite in spriteArray){
                if (name == sprite.name) {
                    plaque.GetComponent<Image>().sprite = sprite;
                    plaque.enabled = !plaque.enabled;
                }
            }
        }
    }


    void StopGramophone(Transform selection) {
        GramophoneController gramophoneController = gramophone.GetComponent<GramophoneController>();
        if(gramophoneController.IsPlaying()) {
            canvas.enabled = true;
            buttonLabel.GetComponent<TMP_Text>().text = "E";
            buttonBackground.enabled = false;
            instructionLabel.GetComponent<TMP_Text>().text = "Turn Off Music";

            if (Input.GetKeyDown(KeyCode.E)) {
                instructionLabel.GetComponent<TMP_Text>().text = "";
                buttonLabel.GetComponent<TMP_Text>().text = "";
                buttonBackground.enabled = false;
                gramophoneController.Stop();
            }
        }
    }

    public void EndTutorial() {
        tutorialDone = true;
    }
}
