using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalController : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame

    public Canvas canvas;

    public Image note;
    public Image page1;
    public Image page2;
    public Image page3;

    private int currentPage = 1;

    void Update()
    {
        if (Input.GetKeyUp (KeyCode.Q)) {
            canvas.enabled = !canvas.enabled;
        }

        if(Input.GetKeyUp (KeyCode.Tab) && canvas.enabled) {
            NextPage();
        }
    }

    void NextPage() {
        if(currentPage < 4) { currentPage++; }
        else { currentPage = 1; }

        switch( currentPage ) {
            case 1:
                note.enabled = true;
                page1.enabled = false;
                page2.enabled = false;
                page3.enabled = false;
                break;
            case 2:
                note.enabled = false;
                page1.enabled = true;
                page2.enabled = false;
                page3.enabled = false;
                break;
            case 3: 
                note.enabled = false;
                page1.enabled = false;
                page2.enabled = true;
                page3.enabled = false;
                break;
            case 4:
                note.enabled = false;
                page1.enabled = false;
                page2.enabled = false;
                page3.enabled = true;
                break;
        }
    }
}
