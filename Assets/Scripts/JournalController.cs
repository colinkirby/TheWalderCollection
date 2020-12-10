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
    public Image page1, page2, page3, page4, page5, page6;
    public Sprite page1_x, page2_x, page3_x, page4_x, page5_x, page6_x;

    private int currentPage = 1;

    private Dictionary<string, Image> pages;

    private Dictionary<string, Sprite> pages_x;

    void Start() {
        pages = new Dictionary<string, Image>(){
	                {"painting1", page1},
	                {"painting2", page2},
	                {"painting3", page3},
                    {"painting4", page4},
                    {"painting5", page5}, 
                    {"painting6", page6}
        };

        pages_x = new Dictionary<string, Sprite>(){
	                {"painting1", page1_x},
	                {"painting2", page2_x},
	                {"painting3", page3_x},
                    {"painting4", page4_x},
                    {"painting5", page5_x}, 
                    {"painting6", page6_x}
        };
    }

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
        if(currentPage < 7) { currentPage++; }
        else { currentPage = 1; }

        switch( currentPage ) {
            case 1:
                note.enabled = true;
                page1.enabled = false;
                page2.enabled = false;
                page3.enabled = false;
                page4.enabled = false;
                page5.enabled = false;
                page6.enabled = false;
                break;
            case 2:
                note.enabled = false;
                page1.enabled = true;
                page2.enabled = false;
                page3.enabled = false;
                page4.enabled = false;
                page5.enabled = false;
                page6.enabled = false;
                break;
            case 3: 
                note.enabled = false;
                page1.enabled = false;
                page2.enabled = true;
                page3.enabled = false;
                page4.enabled = false;
                page5.enabled = false;
                page6.enabled = false;
                break;
            case 4:
                note.enabled = false;
                page1.enabled = false;
                page2.enabled = false;
                page3.enabled = true;
                page4.enabled = false;
                page5.enabled = false;
                page6.enabled = false;
                break;
            case 5:
                note.enabled = false;
                page1.enabled = false;
                page2.enabled = false;
                page3.enabled = false;
                page4.enabled = true;
                page5.enabled = false;
                page6.enabled = false;
                break;
            case 6:
                note.enabled = false;
                page1.enabled = false;
                page2.enabled = false;
                page3.enabled = false;
                page4.enabled = false;
                page5.enabled = true;
                page6.enabled = false;
                break;
            case 7:
                note.enabled = false;
                page1.enabled = false;
                page2.enabled = false;
                page3.enabled = false;
                page4.enabled = false;
                page5.enabled = false;
                page6.enabled = true;
                break;
        }
    }

    public void CrossOffPage(string page) {
        Image currPage = pages[page];
        Sprite newPage = pages_x[page];
        currPage.GetComponent<Image>().sprite = newPage;
    }
}
