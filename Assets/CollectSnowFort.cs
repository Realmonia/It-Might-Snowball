using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.UI;

public class CollectSnowFort : MonoBehaviour {
	public bool touchingSnowPile = false;
	public bool gatheringSnow = false;
	public SnowFortCollect snowfort = null;
	public GameObject oldFort = null;
	public GameObject newForts;
	public Image CollectingUI;
	public GameObject CollectText;
	public GameObject CannonText;
    public GameObject tutorial;
	public AudioSource buildSound;
	public bool isSnowCannon;


    //time in seconds it takes to gather one snowball
    public int timeToCollect = 1;
	private Shooting shooting;
	private Health health;
	private CollectSnow collect;
    private HealSnowmanProximity healScript;
	public bool canMove = true;
	public void disable() {
		canMove = false;
	}

	public void enable() {
		canMove = true;
	}

	InputDevice inputDevice;

	void Start() {
		shooting = GetComponent<Shooting> ();
		CollectingUI.transform.parent.gameObject.SetActive (false);
		health = GetComponent<Health> ();
		collect = GetComponent<CollectSnow> ();
        healScript = GetComponent<HealSnowmanProximity>();
	}

	// Update is called once per frame
	void Update () {
		inputDevice = GetComponent<Movement> ().getInputDevice ();

		//makes B to build text show up
		if (CannonText != null) {
			if (touchingSnowPile) {
				if (oldFort != null && !oldFort.GetComponent<SnowFortCollect> ().isCannon) {
					CannonText.SetActive (true);
				} else {
					CannonText.SetActive (false);
				}
			} else {
				CannonText.SetActive (false);
			}
		}
	}

	void FixedUpdate() {
//		Debug.Log ("this runs");
		if (!canMove) {
			return;
		}

		if (inputDevice == null) {
			return;
		}

		//player collecting from fort
		if (shooting != null) {
//			Debug.Log ("this runshooting");
			if (touchingSnowPile && shooting.maxSnowAmount != shooting.snowAmount && !shooting.isShooting && !collect.touchingSnowPile && !healScript.touchingSnowman) {
				if (inputDevice != null && inputDevice.Action1.IsPressed) {
					if (!gatheringSnow) {
						StartCoroutine (gatherSnow ());
					}
				} else {
					CollectText.SetActive (true);
				}
			}  else {
				CollectText.SetActive (false);
			} 
			if (touchingSnowPile) {
				//create a snowcannon
				if (inputDevice.Action2.IsPressed) {
					if (!gatheringSnow) {
						StartCoroutine (createFortCan ());
					}
				}
			}
		} else {

			//snowman collecting from fort
			if (touchingSnowPile && health.health != health.healthLimit) {
				if (inputDevice != null && inputDevice.Action1.IsPressed) {
					if (!gatheringSnow) {
						StartCoroutine (healSnow ());
					}
				} else {
					CollectText.SetActive (true);
				}
			} else {
				CollectText.SetActive (false);
			}
		}
	}

	IEnumerator gatherSnow() {
		gatheringSnow = true;
		collect.gatheringSnow = true;
		float time = 0f;
		CollectingUI.transform.parent.gameObject.SetActive (true);
		while (inputDevice.Action1.IsPressed) {
			if (!touchingSnowPile){
				break;
			}

			if (shooting.snowAmount == shooting.maxSnowAmount) {
				break;
			}

			time += Time.deltaTime;
			CollectingUI.fillAmount = time;
			if (time >= timeToCollect) {
				shooting.addSnowBall (1);
				if (snowfort != null) {
					snowfort.removeSnow (1);
				}
				time = 0;
			}
			yield return null;
		}

		collect.gatheringSnow = false;

		gatheringSnow = false;
		CollectingUI.transform.parent.gameObject.SetActive (false);
		snowfort = null;
		touchingSnowPile = false;
		oldFort = null;
	}

	IEnumerator healSnow() {
		Debug.Log ("healing from fort");
		gatheringSnow = true;
		float time = 0f;
		CollectingUI.transform.parent.gameObject.SetActive (true);
		while (inputDevice.Action1.IsPressed) {
			if (!touchingSnowPile){
				break;
			}

			if (health.health == health.healthLimit) {
				break;
			}

			time += Time.deltaTime;
			CollectingUI.fillAmount = time;
			if (time >= timeToCollect) {
				health.addHealth (1);
				if (snowfort != null) {
					snowfort.removeSnow (1);
				}
				time = 0;
			}
			yield return null;
		}


		gatheringSnow = false;
		CollectingUI.transform.parent.gameObject.SetActive (false);
	}

	IEnumerator createSnowCan() {
		yield return null;
	}

	//if player holds down button, spawns a fort in front of them
	IEnumerator createFortCan() {
		if (!isSnowCannon) {

			gatheringSnow = true;
			float time = 0f;
			CollectingUI.transform.parent.gameObject.SetActive (true);
			//		GetComponent<ParticleSystem> ().Play ();
			//BuildingProgressParent.SetActive (true);
			//animControl.beginBuldingWall ();
			buildSound.Play();
			while (inputDevice.Action2.IsPressed) {
				if (oldFort == null) {
					break;
				}
				time += Time.deltaTime;
				CollectingUI.fillAmount = time;
				//BuildingProgress.fillAmount = time / timeToCollect;
				if (time >= timeToCollect) {
					//create fort in front of player and remove snow
					GameObject newFort = (GameObject)Instantiate (newForts,
						                     oldFort.transform.position, oldFort.transform.rotation);
					float test = 0f;

					newFort.GetComponent<SnowFortCollect> ().playerNum = 
						GetComponent<Movement> ().player_num + 1;
					//tutorial.GetComponent<TutorialEventChecker>().tutorialEvent[2] = true;
					//reset y location and rotation
					//newFort.transform.position = oldFort.transform.position;
					//newFort.transform.rotation = oldFort.transform.rotation;
					Destroy (oldFort);
                    if (tutorial.GetComponent<TutorialEventChecker>().monitorPG3)
    					tutorial.GetComponent<TutorialEventChecker> ().tutorialEvent [4] = true;
					break;
				}
				yield return null;
			}
			//BuildingProgressParent.SetActive (false);

			//GetComponent<ParticleSystem> ().Stop ();
			gatheringSnow = false;
			CollectingUI.transform.parent.gameObject.SetActive (false);
			buildSound.Stop();

			//animControl.endBuldingWall ();
		}
	}

}
