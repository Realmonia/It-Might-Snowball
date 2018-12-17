using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.UI;

public class CollectSnow : MonoBehaviour {
	public bool touchingSnowPile = false;
	public bool gatheringSnow = false;
	public SnowPile snowPile = null;
	public Image CollectingUI;
	public GameObject CollectText;

	//time in seconds it takes to gather one snowball
	public int timeToCollect = 1;
	private Shooting shooting;

	InputDevice inputDevice;

	public bool canMove = true;
	public void disable() {
		canMove = false;
	}

	public void enable() {
		canMove = true;
	}

	void Start() {
		shooting = GetComponent<Shooting> ();
		CollectingUI.transform.parent.gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		inputDevice = GetComponent<Movement> ().getInputDevice ();
	}

	void FixedUpdate() {
		if (!canMove) {
			return;
		}

		if (touchingSnowPile && shooting.maxSnowAmount != shooting.snowAmount && !shooting.isShooting) {
			if (inputDevice != null && inputDevice.Action1.IsPressed) {
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

	IEnumerator gatherSnow() {
        
		gatheringSnow = true;
		float time = 0f;
		CollectingUI.transform.parent.gameObject.SetActive (true);
		while (inputDevice.Action1.IsPressed) {
			if (!touchingSnowPile){
				break;
			}

			if (shooting.snowAmount == shooting.maxSnowAmount) {
				break;
			}

            if (shooting.isShooting)
            {
                break;
            }
			time += Time.deltaTime;
			CollectingUI.fillAmount = time;
			if (time >= timeToCollect) {
				shooting.addSnowBall (1);
				if (snowPile != null) {
					snowPile.removeSnow (1);
				}
				time = 0;
			}
			yield return null;
		}


		gatheringSnow = false;
		CollectingUI.transform.parent.gameObject.SetActive (false);
	}
}
