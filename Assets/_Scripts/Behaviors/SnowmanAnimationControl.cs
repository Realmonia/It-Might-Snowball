using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanAnimationControl : MonoBehaviour {
	Animator anim;
	Rigidbody rb;
	bool turnOffWalking = false;
    public bool victory = false;
	public AudioSource attackSound;
	public AudioSource buildSound;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();
		attackSound.mute = true;
	}
	
	void Update(){
		if (!turnOffWalking && rb.velocity.magnitude > 0.01f){
			anim.SetLayerWeight (1, 1);
		}else{
			anim.SetLayerWeight (1, 0);
		}
        anim.SetBool("Victory", victory);
        if (victory) turnOffWalking = true;
	}
	public void beginAttack(){
		attackSound.mute = false;
		turnOffWalking = true;
		anim.SetBool ("isAttacking", true);

	}

	public void endAttack(){
		attackSound.mute = true;
		turnOffWalking = false;
		anim.SetBool ("isAttacking", false);
	}

	public void beginBuldingWall(){
		turnOffWalking = true;
		if (!buildSound.isPlaying) {
			buildSound.Play ();
		}
		anim.SetBool ("isBuilding", true);
	}

	public void endBuldingWall(){
		buildSound.Stop ();
		turnOffWalking = false;
		anim.SetBool ("isBuilding", false);
	}

	public void beginDashing(){
		turnOffWalking = true;
		anim.SetBool ("isDashing", true);
	}
	public void endDashing(){
		turnOffWalking = false;
		anim.SetBool ("isDashing", false);
	}


}
