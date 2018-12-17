using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFortCollect : MonoBehaviour {
	public int snow_amount = 1;
	public bool isCannon;
	Vector3 originalScale;

	public int playerNum = -1;

	void Start(){
		originalScale = transform.localScale;
	}

	void Update(){

	}

	public void removeSnow (int amount) {
		snow_amount = Mathf.Max (0, snow_amount - amount);


		//remove size, etc here
		resize(snow_amount);

		if (snow_amount == 0) {
			GetComponent<SphereCollider> ().transform.position = new Vector3(0, 100, 0);
		}
	}

	void resize(int amount){
		Vector3 newScale = new Vector3 (originalScale.x * (float)amount / 10, originalScale.y, originalScale.z * (float)amount / 10);
		transform.localScale = newScale;
		if (amount <= 0){
			GetComponent<SphereCollider> ().center = new Vector3 (0, -3, 0);
		}

	}

	void OnTriggerEnter(Collider other2) {
		handleTriggerStays (other2);
	}

	void OnTriggerStay(Collider other2){
		handleTriggerStays (other2);
	}

	void handleTriggerStays(Collider other2) {
		GameObject other = getParent (other2.gameObject);
		CollectSnowFort collecter = other.GetComponent<CollectSnowFort> ();

		if (collecter != null) {
			int otherPlayerNum = other.GetComponent<Movement> ().player_num;

			if (otherPlayerNum == playerNum || otherPlayerNum == playerNum - 1) {
				collecter.touchingSnowPile = true;
				collecter.snowfort = this;
				collecter.oldFort = this.gameObject;
				collecter.isSnowCannon = isCannon;
			}
		}
	}

	void OnTriggerExit(Collider other2) {
		GameObject other = getParent (other2.gameObject);
		CollectSnowFort collecter = other.GetComponent<CollectSnowFort> ();

		if (collecter != null) {
			int otherPlayerNum = other.GetComponent<Movement> ().player_num;

			if (otherPlayerNum == playerNum || otherPlayerNum == playerNum - 1) {
				collecter.touchingSnowPile = false;
				collecter.snowfort = null;
				collecter.oldFort = null;
			}
		}
	}

	public GameObject getParent(GameObject child) {
		Transform childTransform = child.transform;
		if (childTransform.parent == null) {
			return child;
		}
		return getParent (childTransform.parent.gameObject);
	}
}
