using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionPreparation : MonoBehaviour {
	
	GameObject gold_snowman;
	GameObject gold_player;
	GameObject purple_snowman;
	GameObject purple_player;
	Animator anim;
	public BothReadyMonitor monitorScript;
	GameControl gamecontrol;
	bool isPlaying = true;

	bool gold_introduced = false;
	bool purple_introduced = false;
	// Use this for initialization
	void Start () {
		gold_snowman = GameObject.Find ("Snowman_Gold");
		gold_player = GameObject.Find ("Player_Gold");
		purple_player = GameObject.Find ("Player_Purple");
		purple_snowman = GameObject.Find ("snowman_purple");
		anim = GetComponent<Animator> ();
		gold_snowman.GetComponent<Movement>().enabled = false;
		gold_player.GetComponent<Movement>().enabled = false;
		purple_snowman.GetComponent<Movement>().enabled = false;
		purple_player.GetComponent<Movement>().enabled = false;
		gold_snowman.transform.position = GameObject.Find ("gold_snowman_marker").transform.position;
		gold_snowman.transform.rotation = GameObject.Find ("gold_snowman_marker").transform.rotation;
		gold_player.transform.position = GameObject.Find ("gold_player_marker").transform.position;
		gold_player.transform.rotation = GameObject.Find ("gold_player_marker").transform.rotation;
		purple_snowman.transform.position = GameObject.Find ("purple_snowman_marker").transform.position;
		purple_snowman.transform.rotation = GameObject.Find ("purple_snowman_marker").transform.rotation;
		purple_player.transform.position = GameObject.Find ("purple_player_marker").transform.position;
		purple_player.transform.rotation = GameObject.Find ("purple_player_marker").transform.rotation;
		gamecontrol = GameObject.Find ("GameControl").GetComponent<GameControl> ();
		GameObject.Find ("BGM").GetComponent<AudioSource> ().Play ();
		GameObject[] all = GameObject.FindGameObjectsWithTag("Cannon");
		for (int i =0; i<all.Length; ++i) {
            Destroy(all[i]);
		}

        gold_player.GetComponent<CollectSnowFort>().touchingSnowPile = false;
        purple_player.GetComponent<CollectSnowFort>().touchingSnowPile = false;
	}
	
	// Update is called once per frame
	void Update () {
		isPlaying = AnimatorIsPlaying ();
		for (int i = 0; i < gamecontrol.inputDeviceArray.Length; i++){
			if (gamecontrol.inputDeviceArray[i].Action4.IsPressed){
				isPlaying = false;
			}
		}

		if (!gold_introduced && Vector3.Angle(transform.forward, gold_player.transform.position - transform.position) < 20f && Vector3.Distance(transform.position, gold_player.transform.position) < 5f){
			intro_animation ("gold");
		}
		if (!purple_introduced && Vector3.Angle(transform.forward, purple_player.transform.position - transform.position) < 20f && Mathf.Abs(Vector3.Distance(transform.position, purple_player.transform.position)) < 6f){
			intro_animation ("purple");
		}
		if (!isPlaying){
			stopPoseAnimation ();
			gold_snowman.GetComponent<Movement>().enabled = true;
			gold_player.GetComponent<Movement>().enabled = true;
			purple_snowman.GetComponent<Movement>().enabled = true;
			purple_player.GetComponent<Movement>().enabled = true;
			monitorScript.inIntroduction = false;
			this.gameObject.SetActive (false);
		}
	}

	// https://answers.unity.com/questions/362629/how-can-i-check-if-an-animation-is-being-played-or.html
	bool AnimatorIsPlaying(){
		return anim.GetCurrentAnimatorStateInfo(0).length >
			anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
	}

	void intro_animation(string team){
		if (team == "gold"){
			gold_introduced = true;
			gold_player.GetComponent<Animator> ().SetInteger ("rand_pose", Random.Range (1, 4));
			gold_snowman.GetComponent<Animator> ().SetInteger ("rand_pose", Random.Range (1, 5));

		}else{
			purple_introduced = true;
			purple_player.GetComponent<Animator> ().SetInteger ("rand_pose", Random.Range (1, 4));
			purple_snowman.GetComponent<Animator> ().SetInteger ("rand_pose", Random.Range (1, 5));


		}
	}

	void stopPoseAnimation(){
		gold_player.GetComponent<Animator> ().SetInteger ("rand_pose", 0);
		purple_player.GetComponent<Animator> ().SetInteger ("rand_pose", 0);
		gold_snowman.GetComponent<Animator> ().SetInteger ("rand_pose", 0);
		purple_snowman.GetComponent<Animator> ().SetInteger ("rand_pose", 0);

	}
}
