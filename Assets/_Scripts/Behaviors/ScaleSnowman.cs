using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleSnowman : MonoBehaviour {
	public float sizeChangeScale;
	public float movementChangeScale;
	public Transform bodyparts;
	public Transform step;
	Movement movementScript;
	Animator anim;
	float initSpeed;

	void Start(){
		movementScript = GetComponent<Movement> ();
		initSpeed = movementScript.movement_speed;
		anim = GetComponent <Animator> ();
		anim.SetFloat ("walkSpeed", 0.5f);

	}
	public void change(int health){
		float scale = sizeChangeScale * (health - 3);
		Vector3 newScale = new Vector3 (1+scale, 1+scale, 1+scale);
		bodyparts.localScale = newScale;
		step.localScale = newScale;
		movementScript.movement_speed = initSpeed - movementChangeScale * (health - 3);
		anim.SetFloat ("walkSpeed", 0.5f + 0.15f*(health - 3));

	}
}
