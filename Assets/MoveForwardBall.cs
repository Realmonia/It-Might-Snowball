using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardBall : MonoBehaviour {
	public float speed = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		this.transform.position += speed * this.transform.up;
	}
}
