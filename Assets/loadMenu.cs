using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadMenu : MonoBehaviour {
	// Update is called once per frame
	private void Awake() {
		Screen.SetResolution(1920, 1080, true);
	}
	public void goBack(){
		SceneManager.LoadScene ("Menu");
	}
}
