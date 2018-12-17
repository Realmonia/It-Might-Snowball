using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtSound : MonoBehaviour {
	AudioSource audiosource;
	public AudioClip[] snowball_clip;
	public AudioClip[] snowman_clip;
	// Use this for initialization
	void Start () {
		audiosource = GetComponent<AudioSource> ();
	}
	
	public void play(bool snowball){
		if (snowball){
			audiosource.clip = snowball_clip [Random.Range (0, snowball_clip.Length)];
			audiosource.Play ();
		}else{
			audiosource.clip = snowman_clip [Random.Range (0, snowman_clip.Length)];
			audiosource.Play ();
		}
	}
}
