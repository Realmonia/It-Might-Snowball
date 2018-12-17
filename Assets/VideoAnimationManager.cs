using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class VideoAnimationManager : MonoBehaviour {
	Animator gold_player_animator;
	Animator gold_snowman_animator;
	Animator purple_snowman_animator;
	Animator purple_player_animator;
	Animator MainCameraAnimator;
	GameObject canvas;
	GameObject gold_wall;
	GameObject purple_wall;
	// Use this for initialization
	void Start () {
		gold_player_animator = GameObject.Find ("Player_Gold").GetComponent<Animator> ();
		gold_snowman_animator = GameObject.Find ("Snowman_Gold").GetComponent<Animator> ();
		purple_player_animator = GameObject.Find ("Player_Purple").GetComponent<Animator> ();
		purple_snowman_animator = GameObject.Find ("snowman_purple").GetComponent<Animator> ();
		MainCameraAnimator = GameObject.Find ("Main Camera").GetComponent<Animator> ();
		canvas = GameObject.Find ("AnimationUI");
		Physics.IgnoreLayerCollision (9, 9);
		gold_wall = GameObject.Find ("TutorialGold");
		purple_wall = GameObject.Find ("TutorialPurple");
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Space)){
			playScene(-1);
			canvas.SetActive(true);
		}
	}
	
	public void playScene(int scene_num){
		gold_player_animator.SetInteger ("scene", scene_num);
		gold_snowman_animator.SetInteger ("scene", scene_num);
		purple_player_animator.SetInteger ("scene", scene_num);
		purple_snowman_animator.SetInteger ("scene", scene_num);
		MainCameraAnimator.SetInteger ("scene", scene_num);
		scene_preparation (scene_num);
		canvas.SetActive (false);
	}

	void scene_preparation(int scene_num){
		resetScene ();
		if (scene_num == 0){
			gold_wall.SetActive (false);
			purple_wall.SetActive (false);
			MainCameraAnimator.GetComponent<PostProcessingBehaviour> ().enabled = true;
            gold_snowman_animator.transform.position = new Vector3(33.2f, 0.81f, 19.69f);
            Quaternion temp = Quaternion.identity;
            temp.eulerAngles = new Vector3(0, 305.5f, 0);
            gold_snowman_animator.transform.rotation = temp;
            purple_snowman_animator.transform.position = new Vector3(24.32f, 1.23f, 66.01f);
            temp.eulerAngles = new Vector3(0, 130.6f, 0);
            purple_snowman_animator.transform.rotation = temp;
        }
		if(scene_num == 1){
			gold_wall.SetActive (false);
			purple_wall.SetActive (false);
			MainCameraAnimator.GetComponent<PostProcessingBehaviour> ().enabled = false;
            gold_snowman_animator.transform.position = new Vector3(33.2f, 0.81f, 19.69f);
            Quaternion temp = Quaternion.identity;
            temp.eulerAngles = new Vector3(0, 305.5f, 0);
            gold_snowman_animator.transform.rotation = temp;
            purple_snowman_animator.transform.position = new Vector3(24.32f, 1.23f, 66.01f);
            temp.eulerAngles = new Vector3(0, 130.6f, 0);
            purple_snowman_animator.transform.rotation = temp;
        }

		if (scene_num == 2){
            MainCameraAnimator.GetComponent<PostProcessingBehaviour>().enabled = false;
            gold_player_animator.gameObject.SetActive (false);
			gold_snowman_animator.gameObject.SetActive (false);
			purple_player_animator.gameObject.SetActive (false);
			purple_snowman_animator.gameObject.SetActive (false);

		}

        if (scene_num == 3)
        {
            MainCameraAnimator.GetComponent<PostProcessingBehaviour>().enabled = false;
            
        }
    }

	// https://answers.unity.com/questions/362629/how-can-i-check-if-an-animation-is-being-played-or.html
	bool AnimatorIsPlaying(){
		return MainCameraAnimator.GetCurrentAnimatorStateInfo(0).length >
			MainCameraAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
	}

	void resetScene(){
		gold_wall.SetActive (true);
		purple_wall.SetActive (true);
		gold_player_animator.gameObject.SetActive (true);
		gold_snowman_animator.gameObject.SetActive (true);
		purple_player_animator.gameObject.SetActive (true);
		purple_snowman_animator.gameObject.SetActive (true);
	}
}
