using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.UI;

public class HealSnowman : MonoBehaviour {
	public float timeToHeal = .5f;
	public bool touchingSnowman = false;
	public GameObject snowman;
	public GameObject HealText;
	public GameObject collectText;
    public GameObject tutorial;
	//public Image HealingUI;


	private InputDevice inputDevice;

	private Shooting shooting;
    public bool isHealing = false;
	CollectSnow collectScript;
	// Use this for initialization
	void Start () {
		shooting = GetComponent<Shooting> ();
        HealText.SetActive(false);
		collectText.SetActive (false);
		collectScript = GetComponent<CollectSnow> ();
    }

    // Update is called once per frame
    void Update () {
		inputDevice = GetComponent<Movement> ().getInputDevice ();
		if (inputDevice != null) {
            //Right bumper shoots snowballs
			if (inputDevice.LeftBumper.IsPressed && shooting.snowAmount > 0 && !isHealing && snowman.GetComponent<Health>().canBeHealed() && touchingSnowman) {
				StartCoroutine ("healSnowman");
			}
			if (inputDevice.Action1.IsPressed && !isHealing && snowman.GetComponent<Health>().health > 1 && touchingSnowman) {
				StartCoroutine ("takeSnow");
			}
		}
	}

	IEnumerator healSnowman() {
		isHealing = true;
		float time = 0f;
		//HealingUI.transform.parent.gameObject.SetActive (true);

		while (isHealing) {
			time += Time.deltaTime;
			if (inputDevice == null || !inputDevice.LeftBumper.IsPressed || !touchingSnowman) {
				isHealing = false;
				break;
			}
			if (time > timeToHeal) {
				break;
			}
            Debug.Log(time);
			yield return null;
		}

		if (isHealing) {
			snowman.GetComponent<Health> ().addHealth (1);
			shooting.snowAmount -= 1;
		}
        if (tutorial.GetComponent<TutorialEventChecker>().monitorPG2)
            tutorial.GetComponent<TutorialEventChecker>().tutorialEvent[1] = true;
		isHealing = false;
	}

	IEnumerator takeSnow() {
		isHealing = true;
		float time = 0f;
		collectScript.gatheringSnow = true;
		while (isHealing) {
			time += Time.deltaTime;
			if (inputDevice == null || !inputDevice.Action1.IsPressed || !touchingSnowman) {
				isHealing = false;
				break;
			}
			if (time > timeToHeal) {
				break;
			}
			//HealingUI.fillAmount = time * 2;
			yield return null;
		}
		//HealingUI.transform.parent.gameObject.SetActive (false);

		if (isHealing) {
			snowman.GetComponent<Health> ().removeHealth (1);
			shooting.addSnowBall (1);
		}
		collectScript.gatheringSnow = false;

		isHealing = false;
	}

	void OnCollisionStay(Collision other) {
		if (other.gameObject.GetInstanceID() == snowman.GetInstanceID()) {
			touchingSnowman = true;
			if (snowman.GetComponent<Health> ().health != 
				snowman.GetComponent<Health> ().healthLimit) {
				HealText.SetActive (true);
			}
			if (shooting.snowAmount != shooting.maxSnowAmount) {
				collectText.SetActive (true);
			}
		}
	}

	void OnCollisionExit(Collision other) {
		if (other.gameObject.GetInstanceID() == snowman.GetInstanceID()) {
			touchingSnowman = false;
			HealText.SetActive (false);
			collectText.SetActive (false);

		}
	}
}
