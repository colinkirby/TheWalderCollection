using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LanternController : MonoBehaviour
{
	Light lantern;
	private bool turnedOnLantern = false;
	public UnityEvent turnOnLanternEvent;
	
	void Start () {
		lantern = GetComponent<Light>();
		lantern.enabled = false;
	}

	void Update () {
		// Toggle light on/off when F key is pressed.
		if (Input.GetKeyUp (KeyCode.F)) {
			lantern.enabled = !lantern.enabled;
			if (!turnedOnLantern) {
				turnOnLanternEvent.Invoke();
				turnedOnLantern = true;
			}
		}
	}
 
}
