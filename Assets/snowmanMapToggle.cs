using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class snowmanMapToggle : MonoBehaviour {

	private InputDevice inputDevice_gold;
	private InputDevice inputDevice_purple;
	private GameObject minimapBorder_gold;
	private GameObject minimapBorder_purple;

	private bool mapToggleGold = true;
	private bool mapTogglePurple = true;
	// Use this for initialization
	void Start () {
		minimapBorder_gold = GameObject.Find ("MinimapBorder_snowman_gold");
		minimapBorder_purple = GameObject.Find ("MinimapBorder_snowman_purple");
	}

	void Update () {
		inputDevice_gold = (InputManager.Devices.Count >= 2) ? InputManager.Devices[1] : null;
		inputDevice_purple = (InputManager.Devices.Count >= 4) ? InputManager.Devices[3] : null;
		if (inputDevice_gold != null) {
			if (inputDevice_gold.Action4.WasReleased) {
				mapToggleGold = !mapToggleGold;
				minimapBorder_gold.SetActive (mapToggleGold);
			}
		}
		if (inputDevice_purple != null) {
			if (inputDevice_purple.Action4.WasReleased) {
				mapTogglePurple = !mapTogglePurple;
				minimapBorder_purple.SetActive (mapTogglePurple);
			}
		}
	}
}
