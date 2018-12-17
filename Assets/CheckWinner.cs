using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckWinner : MonoBehaviour {

    public GameControl game;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if ((game.deadPlayers.Contains(0) && game.deadPlayers.Contains(1)))
            gameObject.GetComponent<Text>().text = "Purple Team Win";
        else if (game.deadPlayers.Contains(2) && game.deadPlayers.Contains(3))
            gameObject.GetComponent<Text>().text = "Gold Team Win";
	}
}
