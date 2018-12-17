using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerLocation : MonoBehaviour {
	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null) {
			Debug.Log ("empcannon");
			Vector3 defaultRotation = new Vector3 (90, 0, 0);
			transform.localEulerAngles = defaultRotation;
		}
	}

	/*
	void OnTriggerEnter(Collider other2) {
		GameObject other = getParent (other2.gameObject);
		ActivateCannon active = other.GetComponent<ActivateCannon> ();
		Vector3 defaultRotation = new Vector3 (90, 0, 0);

		// is a player
		if (active != null) {
			transform.localEulerAngles = defaultRotation;
			player = other.gameObject;
			active.cannon = this.gameObject;
		}
	}
	*/

	void OnTriggerStay(Collider other2) {
		GameObject other = getParent (other2.gameObject);
		ActivateCannon active = other.GetComponent<ActivateCannon> ();

		// is a player
		if (active != null) {
			player = other.gameObject;
			active.cannon = this.gameObject;
		}
	}

	void OnTriggerExit(Collider other2) {
		GameObject other = getParent (other2.gameObject);
		ActivateCannon active = other.GetComponent<ActivateCannon> ();

		// is a player
		if (active != null) {
			player = null;
			active.cannon = null;
			//transform.localEulerAngles = defaultRotation;
			//GetComponent<ShootAimFort> ().canRotate = false;
			//this.transform.rotation = GetComponent<ShootAimFort>().startingRotation;
		}
	}

	public void setPlayerLocation() {
		//add a coroutine to slowly move player to position
		Vector3 finalRotation = this.transform.eulerAngles;
		finalRotation.x = 0f;
		player.transform.eulerAngles = finalRotation;
		player.transform.position = this.transform.position - this.transform.up*2 + this.transform.forward * 0.5f;


	}

	public GameObject getParent(GameObject child) {
		Transform childTransform = child.transform;
		if (childTransform.parent == null) {
			return child;
		}
		return getParent (childTransform.parent.gameObject);
	}
}
