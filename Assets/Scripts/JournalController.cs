using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalController : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame

    public Canvas canvas;

    void Update()
    {
        if (Input.GetKeyUp (KeyCode.Q)) {
            canvas.enabled = !canvas.enabled;
        }	
    }
}
