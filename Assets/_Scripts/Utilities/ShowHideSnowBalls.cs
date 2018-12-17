using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideSnowBalls : MonoBehaviour {
	public GameObject[] balls;

	void Start() {
		for (int i = 0; i < balls.Length; ++i) {
			balls [i].SetActive (false);
		}
	}

	public void updateBalls(int amount) {
		for (int i = 0; i < amount; ++i) {
			balls [i].SetActive (true);
		}

		for (int i = amount; i < balls.Length; ++i) {
			balls [i].SetActive (false);
		}
	}
}
