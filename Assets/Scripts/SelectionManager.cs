﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    // Update is called once per frame

    [SerializeField] private Material highlightMaterial;
    [SerializeField] private string selectableTag = "Selectable";
    public GameObject selectableUI;

    void Start() {
        selectableUI.SetActive(false);
    }

    
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2)) {
            var selection = hit.transform;
            
            if(selection.CompareTag(selectableTag)) {
                selectableUI.SetActive(true);
                if(Input.GetKeyDown (KeyCode.E)) {
                    Destroy(hit.transform.gameObject);
                }
            } else {
                if(selectableUI.activeSelf) {
                    selectableUI.SetActive(false);
                }
            }  
        }



    }
}
