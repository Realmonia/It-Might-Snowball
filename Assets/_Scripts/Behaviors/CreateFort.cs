using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.UI;

public class CreateFort : MonoBehaviour {
	public bool touchingSnowPile = false;
	public bool gatheringSnow = false;
	public SnowPile snowPile = null;
	public Image CollectingUI;
	public GameObject CollectText;
	public GameObject Fort;

	//time in seconds it takes to gather one snowball
	public int timeToCollect = 3;
	public float distanceFromPlayer = 2f;
	public int amountOfSnowNeeded = 4;
	private Shooting shooting;
	private CollectSnow collectSnow;

	InputDevice inputDevice;
	private GameObject camera;

	void Start() {
		shooting = GetComponent<Shooting> ();
		CollectingUI.transform.parent.gameObject.SetActive (false);
		collectSnow = GetComponent<CollectSnow> ();
		camera = GetComponentInChildren<Camera> ().gameObject;
	}

	// Update is called once per frame
	void Update () {
		inputDevice = GetComponent<Movement> ().getInputDevice ();
		touchingSnowPile = collectSnow.touchingSnowPile;
		snowPile = collectSnow.snowPile;
	}

	void FixedUpdate() {
		if (touchingSnowPile && snowPile.snow_amount > 4) { //check if posible
			if (inputDevice != null && inputDevice.Action2.IsPressed) {
				if (!gatheringSnow ) {
					StartCoroutine (gatherSnow());
				}
			}else{
				CollectText.SetActive (true);
			}
		}else{
			CollectText.SetActive (false);
		}
	}

	//if player holds down button, spawns a fort in front of them
	IEnumerator gatherSnow() {
		gatheringSnow = true;
		float time = 0f;
		CollectingUI.transform.parent.gameObject.SetActive (true);
		while (inputDevice.Action2.IsPressed) {
			if (!touchingSnowPile){
				break;
			}

			if (snowPile.snow_amount < snowPile.amount_needed_for_fort) {
				break;
			}


			time += Time.deltaTime;
			CollectingUI.fillAmount = time;
			if (time >= timeToCollect) {
				//create fort in front of player and remove snow
				if (snowPile != null) {
					snowPile.removeSnow (amountOfSnowNeeded);
					Vector3 startingOffset = this.transform.forward * distanceFromPlayer;
					GameObject newFort = (GameObject)Instantiate(Fort,
						this.transform.position + startingOffset, camera.transform.rotation);
					float test = 0f;
					newFort.transform.position = new Vector3(newFort.transform.position.x,
						.5f, newFort.transform.position.z);
				}
				break;
			}
			yield return null;
		}


		gatheringSnow = false;
		CollectingUI.transform.parent.gameObject.SetActive (false);
	}
}
