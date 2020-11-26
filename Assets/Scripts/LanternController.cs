using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour
{

	Light lantern;

	void Start () {
		lantern = GetComponent<Light>();
		lantern.enabled = false;
	}

	void Update () {
		// Toggle light on/off when F key is pressed.
		if (Input.GetKeyUp (KeyCode.F)) {
			lantern.enabled = !lantern.enabled;
		}
	}
 
}
