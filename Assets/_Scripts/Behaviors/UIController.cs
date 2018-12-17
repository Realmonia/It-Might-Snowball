using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

	Health healthScript;
	Shooting shootingScript;
	// Use this for initialization
	void Start () {
		healthScript = GetComponent<Health> ();
		shootingScript = GetComponent<Shooting> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
