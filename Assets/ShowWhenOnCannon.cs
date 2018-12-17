using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowWhenOnCannon : MonoBehaviour {

    public GameControl game;
    public GameObject text;
    public bool touchingCannon;
    public bool usingCannon;
	
	// Update is called once per frame
	void Update () {
        usingCannon = gameObject.GetComponent<ActivateCannon>().inCannon;
        touchingCannon = gameObject.GetComponent<ActivateCannon>().cannon != null;

        if (usingCannon) {
            text.transform.GetChild(0).gameObject.GetComponent<Text>().text = "[RS] to change direction, [RB] to shoot, [B] to get off";
            text.SetActive(true);
        }
        else if (touchingCannon) {
            text.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Press [B] to occupy";
            text.SetActive(true);
        }
        else {
            text.SetActive(false);
        }
    }
}
