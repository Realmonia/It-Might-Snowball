using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.UI;

public class HealSnowmanProximity : MonoBehaviour {
	public float timeToHeal = .5f;
	public bool touchingSnowman = false;
	public GameObject snowman;
	public GameObject HealText;
	public GameObject collectText;
	public GameObject tutorial;
	public Image progress_bar;
	CollectSnow collectsnowScript;
	public float distToHeal = 1.5f;
	//public Image HealingUI;

	public bool canMove = true;
	public void disable() {
		canMove = false;
	}

	public void enable() {
		canMove = true;
	}


	private InputDevice inputDevice;

	private Shooting shooting;
	public bool isHealing = false;

	// Use this for initialization
	void Start () {
		shooting = GetComponent<Shooting> ();
		HealText.SetActive(false);
		collectText.SetActive (false);
		collectsnowScript = GetComponent<CollectSnow> ();
	}

	// Update is called once per frame
	void Update () {
		CheckIfCloseBy ();

		inputDevice = GetComponent<Movement> ().getInputDevice ();

		if (!canMove) {
			return;
		}
		if (inputDevice != null) {
			//Right bumper shoots snowballs
			if (inputDevice.LeftBumper.IsPressed && shooting.snowAmount > 0 && !isHealing && snowman.GetComponent<Health>().canBeHealed() && touchingSnowman && !shooting.isShooting) {
				StartCoroutine ("healSnowman");
			}
			if (!collectsnowScript.touchingSnowPile && inputDevice.Action1.IsPressed && !isHealing && snowman.GetComponent<Health>().health > 1 && touchingSnowman && shooting.snowAmount < 4 && !shooting.isShooting) {
				StartCoroutine ("takeSnow");
			}
		}
	}

	IEnumerator healSnowman() {
		isHealing = true;
		float time = 0f;
		//HealingUI.transform.parent.gameObject.SetActive (true);

		while (isHealing) {
			progress_bar.transform.parent.gameObject.SetActive (true);
			time += Time.deltaTime;
			progress_bar.fillAmount = time / timeToHeal;
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
			shooting.removeSnowBall(1);
            if (tutorial.GetComponent<TutorialEventChecker>().monitorPG2)
                tutorial.GetComponent<TutorialEventChecker>().tutorialEvent[1] = true;
        }
		isHealing = false;
		progress_bar.transform.parent.gameObject.SetActive (false);

	}

	IEnumerator takeSnow() {
		isHealing = true;
		float time = 0f;
		collectsnowScript.gatheringSnow = true;
		while (isHealing) {

			progress_bar.transform.parent.gameObject.SetActive (true);
			time += Time.deltaTime;
			progress_bar.fillAmount = time / timeToHeal;
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
		collectsnowScript.gatheringSnow = false;

		isHealing = false;
		progress_bar.transform.parent.gameObject.SetActive (false);
	}

	// Checks if player or snowman within a certain distance apart
	// if they are, allow healing and collecting
	// otherwise disallow them
	void CheckIfCloseBy() {
		float dist = Vector3.Distance (this.transform.position, snowman.transform.position);

		if (dist < distToHeal && snowman.GetComponent<Health>().health != 0) {
			touchingSnowman = true;
            if (!shooting.isShooting) {
                if (snowman.GetComponent<Health>().health !=
                    snowman.GetComponent<Health>().healthLimit && shooting.snowAmount > 0)
                {
                    HealText.SetActive(true);
                }
                if (shooting.snowAmount != shooting.maxSnowAmount)
                {
                    collectText.SetActive(true);
                }
            }
            else {
                HealText.SetActive(false);
                collectText.SetActive(false);
            }
        } else {
			touchingSnowman = false;
			HealText.SetActive (false);
			if (!collectsnowScript.touchingSnowPile) {
				collectText.SetActive (false);
			}
		}

	}
}
