using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using static SceneVariables;
using System.Threading;

public class TutorialManager : MonoBehaviour
{
    private bool seenJournal = false;
    private bool toggledJournal = false;
    private bool putAwayJournal = false;
    private bool usedLantern = false;

    public Canvas canvas;
    public GameObject instructionLabel;
    public GameObject buttonLabel;
    public Image buttonBackground;

    public UnityEvent endTutorialEvent;
    [System.Serializable] public class EnableMovementEvent : UnityEvent<bool> {}
    [SerializeField] public EnableMovementEvent enableMovementEvent;

    void Update() 
    {
        if (SceneVariables.firstRun) {
            if (seenJournal && toggledJournal && putAwayJournal && usedLantern) {
                enableMovementEvent.Invoke(true);
                endTutorialEvent.Invoke();
            } 
            else if (seenJournal && toggledJournal && putAwayJournal) {
                enableMovementEvent.Invoke(false);
                instructionLabel.GetComponent<TMP_Text>().text = "Lantern";
                buttonLabel.GetComponent<TMP_Text>().text = "F";
            }
            else if (seenJournal && toggledJournal) {
                enableMovementEvent.Invoke(false);
                instructionLabel.GetComponent<TMP_Text>().text = "Put Away";
                buttonLabel.GetComponent<TMP_Text>().text = "Q";
            }
            else if (seenJournal) {
                enableMovementEvent.Invoke(false);
                instructionLabel.GetComponent<TMP_Text>().text = "Flip page";
                buttonLabel.GetComponent<TMP_Text>().text = "Tab";
            } 
            else {
                enableMovementEvent.Invoke(false);
                canvas.enabled = true;
                instructionLabel.GetComponent<TMP_Text>().text = "Read Journal";
                buttonLabel.GetComponent<TMP_Text>().text = "Q";
                buttonBackground.enabled = true;
            }
        }
    }

    public void LookAtJournal() {
        seenJournal = true;
    }

    public void FlipThroughJournal() {
        toggledJournal = true;
    }

    public void PutAwayJournal() {
        putAwayJournal = true;
    }

    public void UseLantern() {
        usedLantern = true;
    }
}
