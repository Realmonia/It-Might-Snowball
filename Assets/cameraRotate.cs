using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotate : MonoBehaviour {
	//y : 70-168
	// Use this for initialization
	// Update is called once per frame
	float smooth = 1.0f;
	float tiltAngle = -90.0f;

	void Update()
	{
		// Smoothly tilts a transform towards a target rotation.
		float tiltAroundY = Input.GetAxis("Horizontal") * tiltAngle;
		//float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

		Quaternion target = Quaternion.Euler(0, 338, 0);

		// Dampen towards the target rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
	}
}
