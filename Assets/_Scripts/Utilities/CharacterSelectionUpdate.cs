using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUpdate : MonoBehaviour {

    public int playerId;

    public bool selectionEnabled = true;

    // Update is called once per frame
    void Update () {
        if (selectionEnabled) {
            if (playerId == 1) {
                if (PlayerSelection.Selection1 == 0) {
                    GetComponentInChildren<Text>().text = "<<  Human >>";
                } else {
                    GetComponentInChildren<Text>().text = "<<  Snowman >>";
                }
            } else if (playerId == 2) {
                if (PlayerSelection.Selection2 == 0) {
                    GetComponentInChildren<Text>().text = "<<  Human >>";
                } else {
                    GetComponentInChildren<Text>().text = "<<  Snowman >>";
                }
            } else if (playerId == 3) {
                if (PlayerSelection.Selection3 == 0) {
                    GetComponentInChildren<Text>().text = "<<  Human >>";
                } else {
                    GetComponentInChildren<Text>().text = "<<  Snowman >>";
                }
            } else if (playerId == 4) {
                if (PlayerSelection.Selection4 == 0) {
                    GetComponentInChildren<Text>().text = "<<  Human >>";
                } else {
                    GetComponentInChildren<Text>().text = "<<  Snowman >>";
                }
            }
        }
    }
}
