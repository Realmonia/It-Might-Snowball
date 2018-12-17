using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAttack : MonoBehaviour {
	CapsuleCollider collider;
	SnowmanAttack attackScript;
	// Use this for initialization
	void Start () {
		collider = GetComponent<CapsuleCollider> ();
		collider.enabled = false;
		attackScript = GetComponentInParent<SnowmanAttack> ();
	}
	


	void OnTriggerEnter(Collider otherCollider) {
		Debug.Log (otherCollider.gameObject.name);
		if (otherCollider.gameObject.layer == 9){
			GameObject other = otherCollider.gameObject;
			other = otherCollider.transform.root.gameObject;
			other.GetComponent<Health>().removeHealth(1);
			other.GetComponentInChildren<HurtSound> ().play (false);
			attackScript.endAttack ();
		}
	}
}
