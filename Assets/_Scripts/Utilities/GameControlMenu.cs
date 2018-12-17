using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using InControl;

public class GameControlMenu : MonoBehaviour {

    static public GameControlMenu instance;

    public GameObject[] characterSelectionArray;
    public GameObject readyInfo;
	AudioSource audioSource;
    public InputDevice[] inputDeviceArray;
    private bool isPaused = false;
    private bool gameOver = false;
    public bool[] ready;

    public float tth = 0.5f;

    float delaytime1 = 0f;
    float delaytime2 = 0f;
    float delaytime3 = 0f;
    float delaytime4 = 0f;

    void Awake() {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    void Start() {
        inputDeviceArray = new InputDevice[InputManager.Devices.Count];
        ready = new bool[InputManager.Devices.Count];
        for (int i = 0; i < InputManager.Devices.Count; ++i) {
            inputDeviceArray[i] = InputManager.Devices[i];
            ready[i] = false;
        }
		audioSource = GetComponent<AudioSource> ();
    }

    private void Update() {
        if (inputDeviceArray.Length > 0 && inputDeviceArray[0].LeftStick.X != 0 && delaytime1 > tth) {
            Debug.Log("p1switch");
            PlayerSelection.Selection1 = (PlayerSelection.Selection1 + 1) % 2;
            delaytime1 = 0f;
			audioSource.Play ();
        }
        if (inputDeviceArray.Length > 1 && inputDeviceArray[1].LeftStick.X != 0 && delaytime2 > tth) {
            Debug.Log("p2switch");
            PlayerSelection.Selection2 = (PlayerSelection.Selection2 + 1) % 2;
            delaytime2 = 0f;
			audioSource.Play ();
        }
        if (inputDeviceArray.Length > 2 && inputDeviceArray[2].LeftStick.X != 0 && delaytime3 > tth) {
            Debug.Log("p3switch");
            PlayerSelection.Selection3 = (PlayerSelection.Selection3 + 1) % 2;
            delaytime3 = 0f;
			audioSource.Play ();
        }
        if (inputDeviceArray.Length > 3 && inputDeviceArray[3].LeftStick.X != 0 && delaytime4 > tth) {
            Debug.Log("p4switch");
            PlayerSelection.Selection4 = (PlayerSelection.Selection4 + 1) % 2;
            delaytime4 = 0f;
			audioSource.Play ();
        }
        delaytime1 += Time.deltaTime;
        delaytime2 += Time.deltaTime;
        delaytime3 += Time.deltaTime;
        delaytime4 += Time.deltaTime;
        if (readyInfo.GetComponent<ReadyInfomation>().acceptReady) {
            if (inputDeviceArray.Length > 0 && inputDeviceArray[0].Action1) {
                ready[0] = true;
                characterSelectionArray[0].GetComponent<CharacterSelectionUpdate>().selectionEnabled = false;
            }
            if (inputDeviceArray.Length > 1 && inputDeviceArray[1].Action1) {
                ready[1] = true;
                characterSelectionArray[1].GetComponent<CharacterSelectionUpdate>().selectionEnabled = false;
            }
            if (inputDeviceArray.Length > 2 && inputDeviceArray[2].Action1) {
                ready[2] = true;
                characterSelectionArray[2].GetComponent<CharacterSelectionUpdate>().selectionEnabled = false;
            }
            if (inputDeviceArray.Length > 3 && inputDeviceArray[3].Action1) {
                ready[3] = true;
                characterSelectionArray[3].GetComponent<CharacterSelectionUpdate>().selectionEnabled = false;
            }
        }
    }
}
