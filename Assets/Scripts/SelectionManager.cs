using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    // Update is called once per frame

    [SerializeField] private Material highlightMaterial;
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private string notSelectableTag = "NotSelectable";

    public Canvas canvas;
    
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
            }  else if(selection.CompareTag(notSelectableTag)) {
                IncorrectPainting(selection);
            }
            else {
                if(canvas.enabled) {
                    canvas.enabled = false;
                }
            }
        } else {
            if(canvas.enabled) {
                canvas.enabled = false;
            }
        }
    }

    void DestroyPainting(Transform selection) {
        canvas.enabled = true;
        if(Input.GetKeyDown (KeyCode.E)) {
            Destroy(selection.gameObject);
        }
    }

    void IncorrectPainting(Transform selection) {
        canvas.enabled = true;
        if(Input.GetKeyDown (KeyCode.E)) {
            selection.gameObject.GetComponent<Animator>().enabled = true;
        }
    }
}
