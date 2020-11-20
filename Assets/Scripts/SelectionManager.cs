using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    // Update is called once per frame

    [SerializeField] private Material highlightMaterial;
    [SerializeField] private string selectableTag = "Selectable";

    
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            var selection = hit.transform;
            print(this.gameObject);
            
            if(selection.CompareTag(selectableTag)) {
                print("Looking at art");
                if(Input.GetKeyDown (KeyCode.E)) {
                    // var selectionRenderer = selection.GetComponent<Renderer>();
                    // if (selectionRenderer != null) {
                    //     selectionRenderer.material = highlightMaterial;
                    // }
                    Destroy(gameObject);
                }

            } else {
                print("Not looking at art");
            }
            
        }

    }
}
