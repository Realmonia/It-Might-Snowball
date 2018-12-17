using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinWaveAnim : MonoBehaviour {
	float time = 0f;
	public bool leftLeg = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = this.transform.eulerAngles;
		time += Time.deltaTime;
		pos[0] = Mathf.Sin (time) * 30;

		if (leftLeg) {
			pos [0] *= -1;
		}
		this.transform.eulerAngles = pos;
	}
}
