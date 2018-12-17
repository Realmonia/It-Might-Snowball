using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class SearchSnowball : MonoBehaviour {
	public Shooting shootingScript;
	//Controllers
	private int player_num;
	InputDevice inputDevice;
	// Use this for initialization
	void Awake(){
		shootingScript = GetComponent<Shooting> ();
	}

	void Start () {
		player_num = GetComponent<Movement> () != null ? GetComponent<Movement> ().player_num : -1;
	}
	
	// Update is called once per frame
	void Update () {
		inputDevice = (InputManager.Devices.Count > player_num) ? InputManager.Devices[player_num] : null;
		if (inputDevice != null) {
			if (inputDevice.Action4.IsPressed) {
				//Debug.Log ("Y button pressed");
			}
		}
	}
}
