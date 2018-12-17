using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour {
	public float footStepSpeed;
	public AudioClip[] footStepClips;
	AudioSource footStepSource;
	Animator anim;
	float timer = 0;
	float VolumnModifier;
	bool isDashing = false;
	bool isSnowman = false;

	// Use this for initialization
	void Start () {
		footStepSource = GetComponent<AudioSource> ();
		anim = GetComponentInParent<Animator> ();
		isSnowman = GetComponentInParent<PlayerType> ().isSnowMan;
		VolumnModifier = isSnowman ? 0.5f : 0.8f;
	}
	
	// Update is called once per frame
	void Update () {
		if (isSnowman){
			isDashing = anim.GetBool ("isDashing");
		}

		if (!isDashing) {
			footStepSource.volume = anim.GetLayerWeight (1) * VolumnModifier;
			float calculatedSpeed = footStepSpeed + 0.5f - 0.5f * anim.GetFloat ("walkSpeed");
			if (timer > calculatedSpeed) {
				timer = 0;
				footStepSource.clip = footStepClips [Random.Range (0, footStepClips.Length)];
				footStepSource.Play ();
			}
			timer += Time.deltaTime;
		}else{
			if (timer > 0.13f) {
				timer = 0;
				footStepSource.clip = footStepClips [Random.Range (0, 3)];
				footStepSource.volume = Random.Range(0.4f, 0.6f);
				footStepSource.Play ();
			}
			timer += Time.deltaTime;
		}
	}
}
