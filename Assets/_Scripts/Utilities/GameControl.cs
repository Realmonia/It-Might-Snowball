using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using InControl;

public class GameControl : MonoBehaviour {
    static public GameControl instance;

    public GameObject pauseMenu;
    public GameObject gameSummary;
    public InputDevice[] inputDeviceArray;
    private bool isPaused = false;
    private bool gameOver = false;
    public bool inGameOverCutScene = false;
    private GameObject LastCamera;
    private bool okToGo = false;
	public List<int> deadPlayers = new List<int>();
    public bool inTutorial = true;
    public GameObject tut1;
    public GameObject tut2;
	public GameObject winText1;
	public GameObject winText2;
    public GameObject winText3;
    public GameObject winText4;
    public GameObject allReadyToSkip;

    public GameObject GameEndCamera;

    void Awake() {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    void Start() {
		inputDeviceArray = new InputDevice[InputManager.Devices.Count];
        for (int i = 0; i < InputManager.Devices.Count; ++i) {
            inputDeviceArray[i] = InputManager.Devices[i];
        }
    }

    private void Update() {

        if (!PlayerSelection.tutorial) {
            tut1.GetComponent<TutorialEventChecker>().StartGameWhatever();
            tut2.GetComponent<TutorialEventChecker>().StartGameWhatever();
            for (int i = 0; i<4; ++i)
                allReadyToSkip.GetComponent<BothReadyMonitor>().ready[i] = true;
            PlayerSelection.tutorial = true;
            inTutorial = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            GameObject gold_snowman = GameObject.Find("Snowman_Gold");
            GameObject gold_player = GameObject.Find("Player_Gold");
            gold_snowman.GetComponent<Health>().health = 0;
            gold_player.GetComponent<Health>().health = 0;
        }
        // KeyCode Space only for testing
        if ((InputManager.Devices[0].MenuWasPressed)) {
            Debug.Log("pause status change");            
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);
            Time.timeScale = isPaused ? 0.0f : 1.0f;
        }

        if (isPaused) {
            if (InputManager.Devices[0].Action3.IsPressed) {
                if (inTutorial) {
                    // TODO: Start GameStartSequence
                    tut1.GetComponent<TutorialEventChecker>().playerGoal1CheckboxUnchecked.SetActive(false);
                    tut1.GetComponent<TutorialEventChecker>().playerGoal1CheckboxChecked.SetActive(false);
                    tut1.GetComponent<TutorialEventChecker>().playerGoal1.SetActive(false);
                    tut1.GetComponent<TutorialEventChecker>().playerGoal2CheckboxUnchecked.SetActive(false);
                    tut1.GetComponent<TutorialEventChecker>().playerGoal2CheckboxChecked.SetActive(false);
                    tut1.GetComponent<TutorialEventChecker>().playerGoal2.SetActive(false);
                    tut1.GetComponent<TutorialEventChecker>().playerGoal3CheckboxUnchecked.SetActive(false);
                    tut1.GetComponent<TutorialEventChecker>().playerGoal3CheckboxChecked.SetActive(false);
                    tut1.GetComponent<TutorialEventChecker>().playerGoal3.SetActive(false);
                    tut1.GetComponent<TutorialEventChecker>().snowmanGoal1CheckboxUnchecked.SetActive(false);
                    tut1.GetComponent<TutorialEventChecker>().snowmanGoal1CheckboxChecked.SetActive(false);
                    tut1.GetComponent<TutorialEventChecker>().snowmanGoal1.SetActive(false);
                    tut1.GetComponent<TutorialEventChecker>().snowmanGoal2CheckboxUnchecked.SetActive(false);
                    tut1.GetComponent<TutorialEventChecker>().snowmanGoal2CheckboxChecked.SetActive(false);
                    tut1.GetComponent<TutorialEventChecker>().snowmanGoal2.SetActive(false);
                    tut2.GetComponent<TutorialEventChecker>().playerGoal1CheckboxUnchecked.SetActive(false);
                    tut2.GetComponent<TutorialEventChecker>().playerGoal1CheckboxChecked.SetActive(false);
                    tut2.GetComponent<TutorialEventChecker>().playerGoal1.SetActive(false);
                    tut2.GetComponent<TutorialEventChecker>().playerGoal2CheckboxUnchecked.SetActive(false);
                    tut2.GetComponent<TutorialEventChecker>().playerGoal2CheckboxChecked.SetActive(false);
                    tut2.GetComponent<TutorialEventChecker>().playerGoal2.SetActive(false);
                    tut2.GetComponent<TutorialEventChecker>().playerGoal3CheckboxUnchecked.SetActive(false);
                    tut2.GetComponent<TutorialEventChecker>().playerGoal3CheckboxChecked.SetActive(false);
                    tut2.GetComponent<TutorialEventChecker>().playerGoal3.SetActive(false);
                    tut2.GetComponent<TutorialEventChecker>().snowmanGoal1CheckboxUnchecked.SetActive(false);
                    tut2.GetComponent<TutorialEventChecker>().snowmanGoal1CheckboxChecked.SetActive(false);
                    tut2.GetComponent<TutorialEventChecker>().snowmanGoal1.SetActive(false);
                    tut2.GetComponent<TutorialEventChecker>().snowmanGoal2CheckboxUnchecked.SetActive(false);
                    tut2.GetComponent<TutorialEventChecker>().snowmanGoal2CheckboxChecked.SetActive(false);
                    tut2.GetComponent<TutorialEventChecker>().snowmanGoal2.SetActive(false);
                    tut1.GetComponent<TutorialEventChecker>().show = false;
                    tut2.GetComponent<TutorialEventChecker>().show = false;
                    tut1.GetComponent<TutorialEventChecker>().StartGameWhatever();
                    tut2.GetComponent<TutorialEventChecker>().StartGameWhatever();
                    pauseMenu.SetActive(false);
                    isPaused = !isPaused;
                    for (int i = 0; i < allReadyToSkip.GetComponent<BothReadyMonitor>().ready.Length; ++i)
                        allReadyToSkip.GetComponent<BothReadyMonitor>().ready[i] = true;
                    Time.timeScale = 1.0f;
                } else {
                    Time.timeScale = 1.0f;
                    SceneManager.LoadScene("Menu");
                }
            }
        }

        // KeyCode Esc only for testing
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver) {
            GameOverSequence();
        }

        if (gameOver && okToGo) {
            if (InputManager.ActiveDevice.Action4.IsPressed) {
                Time.timeScale = 1.0f;
                PlayerSelection.tutorial = false;
                Reset();
            } else if (InputManager.ActiveDevice.MenuWasPressed) {
                Time.timeScale = 1.0f;
                SceneManager.LoadScene("Menu");
            }
        }
    }

    public void PlayerDie(GameObject player) {
        Debug.Log(player.name.ToString() + " die!");
		if (player.GetComponent<HealSnowmanProximity>() != null){
			player.GetComponent<Animator> ().SetTrigger ("Dead");
			player.GetComponent<AnimationControl> ().enabled = false;
			player.GetComponent<Animator> ().SetBool ("Aiming", false);
			StartCoroutine (delayHideBodyparts (player.transform.GetChild (1).gameObject));
		}else{
			player.transform.Find("bodyparts").gameObject.SetActive(false);
		}
		if (player.GetComponent<Movement>()) player.GetComponent<Movement>().enabled = false;
		if (player.GetComponent<Shooting>()) player.GetComponent<Shooting>().enabled = false;
		if (player.GetComponent<Aiming>()) player.GetComponent<Aiming>().enabled = false;
		if (player.GetComponent<SearchSnowball>()) player.GetComponent<SearchSnowball>().enabled = false;
        if (player.GetComponent<DisableOtherScripts>()) player.GetComponent<DisableOtherScripts>().disable();
        player.GetComponent<Health>().enabled = false;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		player.transform.GetChild(5).gameObject.SetActive(false);
        player.GetComponent<AttachPanel>().panel.SetActive(true);
		deadPlayers.Add(player.GetComponent<Movement>().player_num);
        LastCamera = player.GetComponent<Movement>().transform.GetChild(0).gameObject;
		if ((deadPlayers.Contains(0) && deadPlayers.Contains(1)) || (deadPlayers.Contains(2) && deadPlayers.Contains(3))) {
			if (deadPlayers.Contains (0) && deadPlayers.Contains (1)) {
                GameEndCamera.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = "Purple Team Win!";
                GameEndCamera.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Outline>().effectColor = new Color32(255, 0, 255, 255);
                winText1.GetComponent<Text>().text = "You Lose";
				winText2.GetComponent<Text>().text = "You Lose";
                winText3.GetComponent<Text>().text = "You Win";
                winText4.GetComponent<Text>().text = "You Win";
            } else {
                GameEndCamera.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = "Gold Team Win!";
                GameEndCamera.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Outline>().effectColor = new Color32(255, 255, 0, 255);
                winText3.GetComponent<Text>().text = "You Lose";
                winText4.GetComponent<Text>().text = "You Lose";
                winText1.GetComponent<Text>().text = "You Win";
                winText2.GetComponent<Text>().text = "You Win";
            }
			StartCoroutine(GameOverSequence());
        }
    }

    IEnumerator GameOverSequence(bool normal = true) {

        gameOver = true;
		GameObject.Find ("BGM").GetComponent<SwitchMusic> ().change ("end");
        inGameOverCutScene = true;
        // Turn off global UI
        GameObject mainCanvas = GameObject.Find("Canvas");
        mainCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        Camera temp = mainCanvas.GetComponent<Canvas>().worldCamera;
        mainCanvas.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        // Turn on Introduction camera
        GameEndCamera.transform.position = LastCamera.transform.position;
        GameEndCamera.transform.rotation = LastCamera.transform.rotation;
        GameEndCamera.GetComponent<Camera>().rect = LastCamera.GetComponent<Camera>().rect;
        GameEndCamera.SetActive(true);
        while (inGameOverCutScene) {
            yield return null;
        }
        mainCanvas.GetComponent<Canvas>().worldCamera = temp;
        mainCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        GameEndCamera.SetActive(false);
        


        gameSummary.SetActive(true);

        yield return new WaitForSeconds(1f);
        okToGo = true;

        Time.timeScale = 0.0f;
    } 

    public void Reset() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	IEnumerator delayHideBodyparts(GameObject bodyparts){
		yield return new WaitForSeconds (3.3f);
		bodyparts.SetActive (false);
	}
}
