using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalController : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame

    public Canvas canvas;

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
        if(currentPage < 3) { currentPage++; }
        else { currentPage = 1; }

        switch( currentPage ) {
            case 1:
                page3.enabled = false;
                page1.enabled = true;
                break;
            case 2:
                page1.enabled = false;
                page2.enabled = true;
                break;
            case 3: 
                page2.enabled = false;
                page3.enabled = true;
                break;
        }
    }
}
