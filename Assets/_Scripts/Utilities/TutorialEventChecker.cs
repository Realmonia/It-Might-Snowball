using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEventChecker : MonoBehaviour {

    public GameObject enemyTutorial;
    public GameObject snowman;
    public GameObject player;
    public GameObject dummy;
    public GameObject playerGoal1CheckboxUnchecked;
    public GameObject playerGoal1CheckboxChecked;
    public GameObject playerGoal2CheckboxUnchecked;
    public GameObject playerGoal2CheckboxChecked;
    public GameObject playerGoal3CheckboxUnchecked;
    public GameObject playerGoal3CheckboxChecked;
    public GameObject snowmanGoal1CheckboxUnchecked;
    public GameObject snowmanGoal1CheckboxChecked;
    public GameObject snowmanGoal2CheckboxUnchecked;
    public GameObject snowmanGoal2CheckboxChecked;
    public GameObject playerWelcome;
    public GameObject snowmanWelcome;

    public GameObject playerGoal1;
    public GameObject playerGoal2;
    public GameObject playerGoal3;
    public GameObject snowmanGoal1;
    public GameObject snowmanGoal2;

    public bool snowmanFinish = false;
    public bool playerFinish = false;
    public bool finished = false;

    public bool enemyTutorialStatus = false;

    public bool[] tutorialEvent = new bool[5];

    public bool monitorPG1 = true;
    public bool monitorPG2 = false;
    public bool monitorPG3 = false;
    public bool monitorSG1 = true;
    public bool monitorSG2 = false;

    public GameObject panel1;
    public GameObject panel2;

    public bool show = true;

    GameControl game;

    /* Tutorial Events:
     * 0. PlayerCollectAmmoAndShoot
     * 1. PlayerFixSnowman
     * 2. SnowmanBuildWall
     * 3. SnowmanDashAndAttack
     * 4. PlayerBuildCannon
     */

    // Use this for initialization
    void Start () {
        game = GameObject.Find("GameControl").GetComponent<GameControl>();
        tutorialEvent[0] = false;
        tutorialEvent[1] = false;
        tutorialEvent[2] = false;
        tutorialEvent[3] = false;
        tutorialEvent[4] = false; // build cannon
        playerWelcome.GetComponent<TextBlink>().stream = "    ";
        playerWelcome.GetComponent<TextBlink>().StartShow();
        snowmanWelcome.GetComponent<TextBlink>().stream = "    ";
        snowmanWelcome.GetComponent<TextBlink>().StartShow();
    }

    // Update is called once per frame
    void Update () {
        enemyTutorialStatus = enemyTutorial.GetComponent<TutorialEventChecker>().finished;

        if (snowmanFinish && game.inTutorial) {
            panel2.SetActive(true);
            if (!playerFinish) {
                snowmanWelcome.GetComponent<Text>().text = "Waiting for your teammate to finish tutorial";
                snowmanWelcome.SetActive(true);
            }
            else if (!enemyTutorialStatus) {
                snowmanWelcome.GetComponent<Text>().text = "Waiting for the opponent team to finish tutorial";
                snowmanWelcome.SetActive(true);
            }
        }
        if (playerFinish && game.inTutorial) {
            panel1.SetActive(true);
            if (!snowmanFinish) {
                playerWelcome.GetComponent<Text>().text = "Waiting for your teammate to finish tutorial";
                playerWelcome.SetActive(true);
            } else if (!enemyTutorialStatus) {
                playerWelcome.GetComponent<Text>().text = "Waiting for the opponent team to finish tutorial";
                playerWelcome.SetActive(true);
            }
        }

        if (gameObject.name == "StartWallPurple") {
            if (GameObject.FindGameObjectsWithTag("FortP").Length > 0) {
                playerGoal3.GetComponent<Text>().text = "Hold [b] to build a cannon on\n" +
                "the fort your teammate built";

            } else {
                playerGoal3.GetComponent<Text>().text = "Ask teammate to build a fort";
            }
        }
        if (gameObject.name == "StartWallGold") {
            if (GameObject.FindGameObjectsWithTag("FortG").Length > 0) {
                playerGoal3.GetComponent<Text>().text = "Hold [b] to build a cannon on\n" +
                "the fort your teammate built";

            } else {
                playerGoal3.GetComponent<Text>().text = "Ask teammate to build a fort";
            }
        }

        if (tutorialEvent[0] && monitorPG1 && show) {
            playerGoal1CheckboxUnchecked.SetActive(false);
            playerGoal1CheckboxChecked.SetActive(true);
            playerGoal2.SetActive(true);
            playerGoal2CheckboxUnchecked.SetActive(true);
            monitorPG1 = false;
            monitorPG2 = true;
        }
        if (tutorialEvent[1] && monitorPG2 && show) {
            playerGoal2CheckboxUnchecked.SetActive(false);
            playerGoal2CheckboxChecked.SetActive(true);
            playerGoal3.SetActive(true);
            playerGoal3CheckboxUnchecked.SetActive(true);
            monitorPG2 = false;
            monitorPG3 = true;
        }
        if (tutorialEvent[2] && monitorSG1 && show) {
            snowmanGoal1CheckboxUnchecked.SetActive(false);
            snowmanGoal1CheckboxChecked.SetActive(true);
            snowmanGoal2.SetActive(true);
            snowmanGoal2CheckboxUnchecked.SetActive(true);
            monitorSG1 = false;
            monitorSG2 = true;
        }
        if (tutorialEvent[3] && monitorSG2 && show) {
            snowmanGoal2CheckboxUnchecked.SetActive(false);
            snowmanGoal2CheckboxChecked.SetActive(true);
            monitorSG2 = false;
        }
        if (tutorialEvent[4] && monitorPG3 && show) {
            playerGoal3CheckboxUnchecked.SetActive(false);
            playerGoal3CheckboxChecked.SetActive(true);
            monitorPG3 = false;
        }
        playerFinish = tutorialEvent[0] && tutorialEvent[1] && tutorialEvent[4];
        snowmanFinish = tutorialEvent[2] && tutorialEvent[3];
        finished = playerFinish && snowmanFinish;
    }

    public void StartGameWhatever() {
        tutorialEvent[0] = true;
        tutorialEvent[1] = true;
        tutorialEvent[2] = true;
        tutorialEvent[3] = true;
        tutorialEvent[4] = true;
    }
}
