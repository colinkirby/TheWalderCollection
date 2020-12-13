using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private string notSelectableTag = "NotSelectable";
    [SerializeField] private string plaqueTag = "Plaque";

    [System.Serializable] public class SelectEvent : UnityEvent<string> {}
    [SerializeField] public SelectEvent selectEvent;

    private Sprite[] spriteArray;

    public Canvas canvas;
    public GameObject instructionLabel;
    public GameObject buttonLabel;
    public Image plaque;
    public Image buttonBackground;

    void Start() {
        spriteArray = Resources.LoadAll<Sprite>("Plaques");
    }
    
    void Update()
    {
        RayCastObject();
    }

    void RayCastObject() {
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3)) {
            var selection = hit.transform;
            
            if(selection.CompareTag(selectableTag)) {
                DestroyPainting(selection);
            } else if(selection.CompareTag(plaqueTag)) {
                TogglePlaque(selection.name);
            } else if(selection.CompareTag(notSelectableTag)) {
                IncorrectPainting();
            } else {
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

    void DestroyPainting(Transform selection) {
        canvas.enabled = true;
        instructionLabel.GetComponent<TMP_Text>().text = "Take Painting";
        buttonLabel.GetComponent<TMP_Text>().text = "E";
        buttonBackground.enabled = true;
        if(Input.GetKeyDown (KeyCode.E)) {
            Destroy(selection.gameObject);
            selectEvent.Invoke(selection.name);
        }
    }

    void IncorrectPainting() {
        canvas.enabled = true;
        instructionLabel.GetComponent<TMP_Text>().text = "Take Painting";
        buttonLabel.GetComponent<TMP_Text>().text = "E";
        buttonBackground.enabled = true;
    }

    void TogglePlaque(string name) {
        print(name);
        if (Input.GetMouseButtonDown(0)) {
            canvas.enabled = true;
            instructionLabel.GetComponent<TMP_Text>().text = "";
            buttonLabel.GetComponent<TMP_Text>().text = "";
            buttonBackground.enabled = false;

            if (Input.GetMouseButtonDown(0)) {
                foreach(var sprite in spriteArray){
                    if (name == sprite.name) {
                        plaque.GetComponent<Image>().sprite = sprite;
                        plaque.enabled = !plaque.enabled;
                    }
                }
            }
        }
    }
}
