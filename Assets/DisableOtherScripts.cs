using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOtherScripts : MonoBehaviour {
	//enables or disables these scripts
	Shooting shooting;
	Movement movement;
	CollectSnow collectSnow;
	CollectSnowFort collectFort;
	Aiming aiming;
	Rigidbody rb;
    HealSnowmanProximity heal;
	//ActivateCannon active;

	// Use this for initialization
	void Start () {
		shooting = GetComponent<Shooting> ();
		movement = GetComponent<Movement> ();
		collectSnow = GetComponent<CollectSnow> ();
		collectFort = GetComponent<CollectSnowFort> ();
		aiming = GetComponent<Aiming> ();
		rb = GetComponent<Rigidbody>();
        heal = GetComponent<HealSnowmanProximity>();
		//active = GetComponent<ActivateCannon> ();
	}
	
	public void disable() {
		shooting.disable ();
		movement.disable ();
		rb.velocity = Vector3.zero;
		collectSnow.disable ();
		collectFort.disable ();
		aiming.disable ();
        heal.disable();
		//active.disable ();
	}

	public void enable() {
		shooting.enable ();
		movement.enable ();
		collectSnow.enable ();
		collectFort.enable ();
		aiming.enable ();
        heal.enable();
		//active.enable ();
	}
}
