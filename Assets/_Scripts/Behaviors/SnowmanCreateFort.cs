using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.UI;

public class SnowmanCreateFort : MonoBehaviour {
	public bool gatheringSnow = false;
	public GameObject Fort;
    public GameObject tutorial;

	//time in seconds it takes to create fort
	public float timeToCollect = 1f;
	public float distanceFromPlayer = 2f;
	public int amountOfSnowNeeded = 2;
	public Image BuildingProgress;
	public float rayCastDistance = 1f;

	private Shooting shooting;
	private CollectSnow collectSnow;

	InputDevice inputDevice;
	private GameObject camera;
	GameObject BuildingProgressParent;
	SnowmanAnimationControl animControl;

	void Start() {
		shooting = GetComponent<Shooting> ();
		collectSnow = GetComponent<CollectSnow> ();
		camera = GetComponentInChildren<Camera> ().gameObject;
		GetComponent<ParticleSystem> ().Stop ();
		BuildingProgressParent = BuildingProgress.transform.parent.gameObject;
		BuildingProgressParent.SetActive (false);
		animControl = GetComponent<SnowmanAnimationControl> ();
	}

	// Update is called once per frame
	void Update () {
		inputDevice = GetComponent<Movement> ().getInputDevice ();
	}

	void FixedUpdate() {
		if (GetComponent<Health>().health > amountOfSnowNeeded) { //check if posible
			if (inputDevice != null && inputDevice.Action2.IsPressed) {
				if (!gatheringSnow ) {
					StartCoroutine (gatherSnow());
				}
			}
		}
	}

	//if player holds down button, spawns a fort in front of them
	IEnumerator gatherSnow() {
		gatheringSnow = true;
		float time = 0f;
		GetComponent<ParticleSystem> ().Play ();
		BuildingProgressParent.SetActive (true);
		animControl.beginBuldingWall ();
		while (inputDevice.Action2.IsPressed) {

			if (GetComponent<Health>().health <= amountOfSnowNeeded) {
				break;
			}

			Vector3 forward = transform.TransformDirection(Vector3.forward) * rayCastDistance;
			Vector3 down = transform.TransformDirection (Vector3.down) * rayCastDistance;
			Debug.DrawRay(transform.position + forward / 1.5f + down / 2, forward, Color.green);
			if (Physics.Raycast (transform.position + forward / 1.5f + down / 2, forward, 3f)) {
				Debug.Log ("Something in way of fort!");
				break;
			}

			time += Time.deltaTime;
			BuildingProgress.fillAmount = time / timeToCollect;
			if (time >= timeToCollect) {
				//create fort in front of player and remove snow
				if (GetComponent<Health>().health > amountOfSnowNeeded) {
					GetComponent<Health> ().removeHealth (amountOfSnowNeeded);
					Vector3 startingOffset = this.transform.forward * distanceFromPlayer;
					GameObject newFort = (GameObject)Instantiate(Fort,
						this.transform.position + startingOffset, camera.transform.rotation);
					float test = 0f;

					newFort.GetComponent<SnowFortCollect> ().playerNum = 
						GetComponent<Movement> ().player_num;

                    if (tutorial.GetComponent<TutorialEventChecker>().monitorSG1)
                        tutorial.GetComponent<TutorialEventChecker>().tutorialEvent[2] = true;
                    //reset y location and rotation
                    newFort.transform.position = new Vector3(newFort.transform.position.x,
						0f, newFort.transform.position.z);
					newFort.transform.eulerAngles = new Vector3 (0f, 
						newFort.transform.eulerAngles.y, 0f);
				}
				break;
			}
			yield return null;
		}
		BuildingProgressParent.SetActive (false);

		GetComponent<ParticleSystem> ().Stop ();
		gatheringSnow = false;
		animControl.endBuldingWall ();
	}
}
