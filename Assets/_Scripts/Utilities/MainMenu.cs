using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerSelection {
    public static bool tutorial { get; set; }
    public static int Selection1 { get; set; }
    public static int Selection2 { get; set; }
    public static int Selection3 { get; set; }
    public static int Selection4 { get; set; }
    public static void Initialization() {
        tutorial = true;
        Selection1 = 0;
        Selection2 = 1;
        Selection3 = 0;
        Selection4 = 1;
    }
}

public class MainMenu : MonoBehaviour {

    private void Awake() {
        Screen.SetResolution(1920, 1080, true);
    }

    public bool skip = false;

    public void setSkipTrue() {
        skip = true;
    }

    public void StartGame() {
        PlayerSelection.tutorial = true;
        SceneManager.LoadScene("GameWithTutorial");
    }

    public void Tutorial() {
        SceneManager.LoadScene("Tutorial");
    }

	public void Credits(){
		SceneManager.LoadScene ("Credits");
	}

    public void Quit() {
        Application.Quit();
    }
}
