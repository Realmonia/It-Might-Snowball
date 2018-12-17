using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInstructionWhenStart : MonoBehaviour {

    public GameControl game;
	
	// Update is called once per frame
	void Update () {
		if (!game.inTutorial) {
            gameObject.SetActive(false);
        }
	}
}
