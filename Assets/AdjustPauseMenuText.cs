using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustPauseMenuText : MonoBehaviour {

    Text text;

    public GameControl game;

	// Use this for initialization
	void Start () {
        text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (game.inTutorial) {
            text.text = "Press [MENU] to Resume\nPress [X] to Skip Tutorial";
        } else {
            text.text = "Press [MENU] to Resume\nPress [X] to Exit";
        }
    }
}
