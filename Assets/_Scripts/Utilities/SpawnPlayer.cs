using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnPlayer : MonoBehaviour {

    public GameObject Snowman;
    public GameObject Normal;

    public GameObject spawnedP1;
    public GameObject spawnedP2;

    public GameObject P1Info;
    public GameObject P2Info;

    public GameObject P1Panel;
    public GameObject P2Panel;

    public int spawnerId;

    public GameControl game;

    private void Awake() {
        // For milestone: force one player one snowman
        PlayerSelection.Selection1 = 0;
        PlayerSelection.Selection2 = 1;
        PlayerSelection.Selection3 = 0;
        PlayerSelection.Selection4 = 1;
        int p1Type, p2Type;
        if (spawnerId == 0) {
            if (PlayerSelection.Selection1 == 0) {
                spawnedP1 = Instantiate(Normal, transform.position + new Vector3(-3, 0 ,0), Quaternion.identity);
                p1Type = 0;
            } else {
                spawnedP1 = Instantiate(Snowman, transform.position + new Vector3(-3, 0, 0), Quaternion.identity);
                p1Type = 1;
            }
            if (PlayerSelection.Selection2 == 0) {
                spawnedP2 = Instantiate(Normal, transform.position + new Vector3(3, 0, 0), Quaternion.identity);
                p2Type = 0;
            } else {
                spawnedP2 = Instantiate(Snowman, transform.position + new Vector3(3, 0, 0), Quaternion.identity);
                p2Type = 1;
            }
        } else{
            if (PlayerSelection.Selection3 == 0) {
                spawnedP1 = Instantiate(Normal, transform.position + new Vector3(-3, 0, 0), Quaternion.identity);
                p1Type = 0;
            } else {
                spawnedP1 = Instantiate(Snowman, transform.position + new Vector3(-3, 0, 0), Quaternion.identity);
                p1Type = 1;
            }
            if (PlayerSelection.Selection4 == 0) {
                spawnedP2 = Instantiate(Normal, transform.position + new Vector3(3, 0, 0), Quaternion.identity);
                p2Type = 0;
            } else {
                spawnedP2 = Instantiate(Snowman, transform.position + new Vector3(3, 0, 0), Quaternion.identity);
                p2Type = 1;
            }
        }

        if (p1Type == 0) { // is a normal player
            spawnedP1.GetComponent<Movement>().player_num = spawnerId * 2;
            spawnedP1.GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0f + 0.5f * spawnerId, 0.5f), new Vector2(0.5f, 0.5f));
            P1Info.GetComponentInChildren<PlayerInfoUpdate>().player = spawnedP1;
            spawnedP1.GetComponent<Health>().game = game;
            spawnedP1.GetComponent<Health>().playerInfo = P1Info;
            spawnedP1.GetComponent<AttachPanel>().panel = P1Panel;
			if (SceneManager.GetActiveScene().name == "Tutorial") {
				P1Info.transform.GetChild (1).gameObject.GetComponent<TextBlink> ().stream =
                "                            Try to shoot dummies!";
                P1Info.transform.GetChild (2).gameObject.GetComponent<Text> ().text =
                "[RB] to aim and shoot";
            }
            spawnedP1.GetComponent<HealSnowman>().HealText = spawnedP1.transform.GetChild(2).GetChild(3).gameObject;
            //spawnedP1.GetComponent<HealSnowman>().HealingUI = spawnedP1.transform.GetChild(2).GetChild(2).GetChild(1).gameObject.GetComponent<Image>();

        } else { // is a snowman
            spawnedP1.GetComponent<Movement>().player_num = spawnerId * 2;
            spawnedP1.GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0f + 0.5f * spawnerId, 0.5f), new Vector2(0.5f, 0.5f));
            P1Info.GetComponentInChildren<PlayerInfoUpdate>().player = spawnedP1;
            spawnedP1.GetComponent<Health>().game = game;
            spawnedP1.GetComponent<Health>().playerInfo = P1Info;
            spawnedP1.GetComponent<AttachPanel>().panel = P1Panel;
			if (SceneManager.GetActiveScene().name == "Tutorial") {
				P1Info.transform.GetChild (1).gameObject.GetComponent<TextBlink> ().stream =
                "                            Try to melee attack dummies!";
                P1Info.transform.GetChild (2).gameObject.GetComponent<Text> ().text =
                "[RB] to melee attack";
			}
        }

        if (p2Type == 0) {
            spawnedP2.GetComponent<Movement>().player_num = spawnerId * 2 + 1;
            spawnedP2.GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0f + 0.5f * spawnerId, 0f), new Vector2(0.5f, 0.5f));
            P2Info.GetComponentInChildren<PlayerInfoUpdate>().player = spawnedP2;
            spawnedP2.GetComponent<Health>().game = game;
            spawnedP2.GetComponent<Health>().playerInfo = P2Info;
            spawnedP2.GetComponent<AttachPanel>().panel = P2Panel;
			if (SceneManager.GetActiveScene().name == "Tutorial") {
				P2Info.transform.GetChild (1).gameObject.GetComponent<TextBlink> ().stream =
                "                            Try to shoot dummies!";
                P2Info.transform.GetChild (2).gameObject.GetComponent<Text> ().text =
                "[RB] to aim and shoot";
			}
            spawnedP2.GetComponent<HealSnowman>().HealText = spawnedP2.transform.GetChild(2).GetChild(3).gameObject;
            //spawnedP2.GetComponent<HealSnowman>().HealingUI = spawnedP2.transform.GetChild(2).GetChild(2).GetChild(1).gameObject.GetComponent<Image>();

        } else {
            spawnedP2.GetComponent<Movement>().player_num = spawnerId * 2 + 1;
            spawnedP2.GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0f + 0.5f * spawnerId, 0f), new Vector2(0.5f, 0.5f));
            P2Info.GetComponentInChildren<PlayerInfoUpdate>().player = spawnedP2;
            spawnedP2.GetComponent<Health>().game = game;
            spawnedP2.GetComponent<Health>().playerInfo = P2Info;
            spawnedP2.GetComponent<AttachPanel>().panel = P2Panel;
			if (SceneManager.GetActiveScene().name == "Tutorial") {
				P2Info.transform.GetChild (1).gameObject.GetComponent<TextBlink> ().stream =
                "                            Try to melee attack dummies!";
                P2Info.transform.GetChild (2).gameObject.GetComponent<Text> ().text =
                "[RB] to melee attack";
			}
        }
        spawnedP1.gameObject.GetComponent<HealSnowman>().snowman = spawnedP2;
    }
}
