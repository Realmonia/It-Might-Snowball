using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterGameStart : MonoBehaviour {

    public GameControl game;
    private bool tutorialPeriod = false;

    private void Start() {

        game = GameObject.Find("GameControl").GetComponent<GameControl>();
        if (game.inTutorial) {
            tutorialPeriod = true;
        }
    }

    // Update is called once per frame
    void Update () {
		if (!game.inTutorial && tutorialPeriod) {
            Destroy(gameObject);
        }
	}
}
