using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.UI;

public class Aiming : MonoBehaviour {
	public GameObject crosshair;
    public bool isAiming = false;
	InputDevice inputDevice;
	int player_num;
	GameObject player_camera;
	public Coroutine aiming_coroutine;
	ActivateCannon cannonScript;
    Shooting shootingScript;
    public GameObject ammoNotification;
	public bool canMove = true;
	public void disable() {
		canMove = false;
	}

	public void enable() {
		canMove = true;
	}

	// Use this for initialization
	void Start () {
		player_num = GetComponent<Movement> ().player_num;
		player_camera = GetComponentInChildren<Camera> ().gameObject;
		cannonScript = GetComponent<ActivateCannon> ();
        shootingScript = GetComponent<Shooting>();
	}
	
	// Update is called once per frame
	void Update () {
		inputDevice = (InputManager.Devices.Count > player_num) ? InputManager.Devices[player_num] : null;
		if (!canMove) {
			return;
		}

		if (inputDevice != null){
			if (inputDevice.RightBumper.IsPressed && cannonScript.cannonCoroutine == null){
				if (aiming_coroutine != null){
					StopCoroutine (aiming_coroutine);
				}
				aiming_coroutine = StartCoroutine(EnterAimingMode ());
			}
			if (!inputDevice.RightBumper.IsPressed && cannonScript.cannonCoroutine == null){
					if (aiming_coroutine != null) {
						StopCoroutine (aiming_coroutine);
					}
					aiming_coroutine = StartCoroutine (ExitAimingMode ());
			}
		}
	}

	IEnumerator EnterAimingMode(){
		isAiming = true;
        GetComponent<Movement>().rotationSlowDown();
		Vector3 initPos = player_camera.transform.localPosition;
		float end_y = player_camera.transform.localPosition.y * (-2.2f / player_camera.transform.localPosition.z);
		Vector3 endpoint = new Vector3(player_camera.transform.localPosition.x, end_y, -2.2f);
        if (shootingScript.snowAmount == 0) {
            ammoNotification.SetActive(true);
        }
        else {
            ammoNotification.SetActive(false);
        }
		crosshair.SetActive (true);
		for (float t = 0; t < 0.5f; t += Time.deltaTime){
			player_camera.transform.localPosition = Vector3.Lerp (initPos, endpoint, 0.2f);
			yield return null;
		}

	}

	IEnumerator ExitAimingMode(){
        ammoNotification.SetActive(false);
        isAiming = false;
        GetComponent<Movement>().rotationSpeedUp();
		Vector3 initPos = player_camera.transform.localPosition;
		float end_y = player_camera.transform.localPosition.y * (-4f / player_camera.transform.localPosition.z);
        Vector3 endpoint = new Vector3(player_camera.transform.localPosition.x, end_y, -4f);
		crosshair.SetActive (false);
		for (float t = 0; t < 0.5f; t += Time.deltaTime){
			player_camera.transform.localPosition = Vector3.Lerp (initPos, endpoint, 0.2f);
			yield return null;
		}

	}
}
