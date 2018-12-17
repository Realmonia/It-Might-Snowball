using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammobar : MonoBehaviour {
	GameObject[] ammos;
	Shooting shootingScript;
	int current_amount;
	// Use this for initialization
	void Start () {
		Image[] temp = GetComponentsInChildren<Image> ();
		shootingScript = GetComponentInParent<Shooting> ();
		ammos = new GameObject[temp.Length];
		for (int i = 0; i < temp.Length; i++){
			ammos [i] = temp [i].gameObject;
			if (i < shootingScript.snowAmount - 1) {
				ammos [i].SetActive (true);
			} else {
				ammos [i].SetActive (false);
			}
		}
		current_amount = shootingScript.snowAmount;
	}
	
	public void increase_ammo(int new_amount){
		for (int i = current_amount -1; i < new_amount; i++){
			if (i == -1)
				i = 0;
			ammos [i].SetActive (true);
		}
		current_amount = new_amount;
	}

	public void decrease_ammo(int new_amount){
		for (int i = current_amount-1; i > new_amount -1; i--){
			if (i == -1)
				break;
			ammos [i].SetActive (false);
		}
		current_amount = new_amount;

	}
}
