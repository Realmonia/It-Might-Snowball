using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMarkRotation : MonoBehaviour {
	Vector3 initPosition;
	Vector3 newPosition;
	float offsetY = 0;
	// Use this for initialization
	void Start () {
		initPosition = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.RotateAround (transform.position, Vector3.up, 2);

	}
}
