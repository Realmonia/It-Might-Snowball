using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class ActivateCannon : MonoBehaviour {
	public GameObject cannon = null;

	DisableOtherScripts disable;
	InputDevice inputDevice;
	Aiming aimingScript;
	public bool inCannon = false;
	public bool toggling = false;

	private bool isAiming = false;
	GameObject player_camera;
	int player_num;
	public GameObject crosshair;
	Rigidbody rb;
	public Coroutine cannonCoroutine;
	public float prevEndY = 0f;

	// Use this for initialization
	void Start () {
		disable = GetComponent<DisableOtherScripts> ();
		player_num = GetComponent<Movement> ().player_num;
		player_camera = GetComponentInChildren<Camera> ().gameObject;
		aimingScript = GetComponent<Aiming> ();
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		inputDevice = GetComponent<Movement> ().getInputDevice ();
		if (toggling) {
			return;
		}

		if (inputDevice != null) {
			if (inCannon && cannon != null){
				Vector3 rotation = new Vector3 (player_camera.transform.localEulerAngles.x, cannon.transform.localEulerAngles.y, player_camera.transform.localEulerAngles.z);
				player_camera.transform.localEulerAngles = rotation;
			}
			if (cannon != null) {
				if (inputDevice.Action2.WasPressed) {
					if (!inCannon) {
						//put player in cannon and disable scripts
						disable.disable ();
						if (cannonCoroutine != null){
							StopCoroutine (cannonCoroutine);
						}
						if (aimingScript.aiming_coroutine != null){
							aimingScript.StopCoroutine (aimingScript.aiming_coroutine);
						}
						cannonCoroutine = StartCoroutine (EnterAimingMode());
						cannon.GetComponent<SetPlayerLocation> ().setPlayerLocation ();
						cannon.GetComponent<ShootAimFort> ().canRotate = true;
						inCannon = true;
					} else {
						//reset everything give player back control
						if (cannonCoroutine != null){
							StopCoroutine (cannonCoroutine);
						}
						cannonCoroutine = StartCoroutine (ExitAimingMode());
						inCannon = false;
						disable.enable ();
						cannon.GetComponent<ShootAimFort> ().resetRotation ();
						cannon.GetComponent<ShootAimFort> ().canRotate = false;
					}
				}
			} else { //cannon = null
				if (inCannon) {
					if (cannonCoroutine != null){
						StopCoroutine (cannonCoroutine);
					}
					cannonCoroutine = StartCoroutine (ExitAimingMode());
					inCannon = false;
					disable.enable ();
				}
			}

		}
	}

	IEnumerator EnterAimingMode(){
		//isAiming = true;
		GetComponent<Movement>().rotationSlowDown();
		//float end_y = player_camera.transform.localPosition.y * (0f / player_camera.transform.localPosition.z);
		Vector3 endpoint = new Vector3(0f, 1.84f, 0.4f);
		Vector3 initPos = player_camera.transform.localPosition;
//		player_camera.transform.localPosition = endpoint;
		Vector3 endRotation = new Vector3(6, 0, 0);


		crosshair.SetActive (true);
		for (float t = 0; t < 0.5f; t += Time.deltaTime){
			player_camera.transform.localPosition = Vector3.Lerp (initPos, endpoint, 2*t);
			yield return null;
		}
		player_camera.transform.localPosition = endpoint;
		player_camera.transform.eulerAngles = endRotation;
	}

	public IEnumerator ExitAimingMode(){
		//isAiming = false;
		GetComponent<Movement>().rotationSpeedUp();
		//float end_y = player_camera.transform.localPosition.y * (-4f / player_camera.transform.localPosition.z);
		Vector3 endpoint = new Vector3(0.42f, 1.48f, -4f);
		player_camera.transform.localRotation = Quaternion.identity;
		Vector3 initPos = player_camera.transform.localPosition;
		crosshair.SetActive (false);
		for (float t = 0; t < 0.5f; t += Time.deltaTime){
			player_camera.transform.localPosition = Vector3.Lerp (initPos, endpoint, 2*t);
			yield return null;
		}
		player_camera.transform.localPosition = endpoint;
		cannonCoroutine = null;
	}
}
