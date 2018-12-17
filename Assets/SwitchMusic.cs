using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusic : MonoBehaviour {
	public AudioClip BattleStartMusic;
	public AudioClip BattleEndMusic;
	public AudioClip MenuMusic;
	AudioSource audioSource;
	void Start(){
		audioSource = GetComponent<AudioSource> ();
	}
	public void change(string music){
		if (music == "end"){
			audioSource.clip = BattleEndMusic;
			audioSource.Stop ();
			audioSource.Play ();
		}
	}
}
