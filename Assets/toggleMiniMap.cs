using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class toggleMiniMap : MonoBehaviour {
	private int player_num;
	private InputDevice inputDevice_gold;
	private InputDevice inputDevice_purple;
	private GameObject minimapBorder_Gold;
	private GameObject minimapBorder_Purple;
	private bool mapToggleGold = true;
	private bool mapTogglePurple = true;

	// Use this for initialization
	void Start () {
		player_num = GetComponentInParent<Movement>() != null ? GetComponentInParent<Movement>().player_num : -1;
		minimapBorder_Gold = GameObject.Find ("MinimapBorder_Gold");
		minimapBorder_Purple = GameObject.Find ("MinimapBorder_Purple");
	}
	
	void Update () {
		inputDevice_gold = (InputManager.Devices.Count >= 1) ? InputManager.Devices[0] : null;
		inputDevice_purple = (InputManager.Devices.Count >= 3) ? InputManager.Devices[2] : null;
		if (inputDevice_gold != null) {
			if (inputDevice_gold.Action4.WasReleased) {
				mapToggleGold = !mapToggleGold;
				minimapBorder_Gold.SetActive (mapToggleGold);
			}
		}
		if (inputDevice_purple != null) {
			if (inputDevice_purple.Action4.WasReleased) {
				mapTogglePurple = !mapTogglePurple;
				minimapBorder_Purple.SetActive (mapTogglePurple);
			}
		}
	}
}
