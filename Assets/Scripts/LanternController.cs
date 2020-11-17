using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour
{

	Light lantern;

	// Use this for initialization
	void Start () {
		lantern = GetComponent<Light>();
	}

	// Update is called once per frame
	void Update () {
		// Toggle light on/off when L key is pressed.
		if (Input.GetKeyUp (KeyCode.F)) {
			lantern.enabled = !lantern.enabled;
		}
	}
 
}
