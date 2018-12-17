using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyInfomation : MonoBehaviour {

    public Text text;
    public GameControlMenu menu;
    public GameObject mainMenu;

    public bool allReady = false;

    public bool acceptReady = false;

    private void Start() {
        StartCoroutine(Blink());
    }

    IEnumerator Blink() {
        yield return new WaitForSeconds(1f);
        acceptReady = true;

        while (!allReady) {
            yield return new WaitForSeconds(0.1f);
        }
        text.text = "Game will start in 3s";
        yield return new WaitForSeconds(0.5f);
        text.enabled = true;
        yield return new WaitForSeconds(0.5f);
        text.enabled = false;
        text.text = "Game will start in 2s";
        yield return new WaitForSeconds(0.5f);
        text.enabled = true;
        yield return new WaitForSeconds(0.5f);
        text.enabled = false;
        text.text = "Game will start in 1s";
        yield return new WaitForSeconds(0.5f);
        text.enabled = true;
        yield return new WaitForSeconds(0.5f);
        text.enabled = false;
        if (!PlayerSelection.tutorial) {
            mainMenu.GetComponent<MainMenu>().StartGame();
        } else {
            mainMenu.GetComponent<MainMenu>().Tutorial();
        }

    }

    void Update() {
        bool[] arr = menu.ready;
        for (int i = 0; i < arr.Length; ++i) {
            if (!arr[i]) {
                return;
            }
        }
        allReady = true;
    }

}
