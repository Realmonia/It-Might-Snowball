using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBodyPart : MonoBehaviour {

	//removes this body part if player loses health
	public int healthToRemoveAt = 0;
	private Health playerHealth;

	// Use this for initialization
	void Start () {
		playerHealth = getParent (this.gameObject).GetComponent<Health> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerHealth != null) {
			if (playerHealth.health <= healthToRemoveAt) {
				this.gameObject.SetActive (false);
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
