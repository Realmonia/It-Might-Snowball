using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class ShootAimFort : MonoBehaviour {
	GameObject player;
	Shooting shooting;
	InputDevice inputDevice;
	AudioSource shootSound;
	public bool isShooting = false;
	public bool canRotate = false;


	public Vector3 shootingAngleOffset = new Vector3(0f, 1f, 0f);
	public float forwardOffset = 2f;
	public float shootingPowerModifier = 10f;
	public AnimationCurve shootingPower;
	public GameObject CannonBall;

	public Quaternion startingRotation;
	Animator anim;
	// Use this for initialization
	void Start () {
		startingRotation = this.transform.rotation;
		anim = GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		player = GetComponent<SetPlayerLocation> ().player;
		if (player != null) {
			inputDevice = player.GetComponent<Movement> ().getInputDevice ();
			shooting = player.GetComponent<Shooting> ();
			if (shootSound == null){
				shootSound = player.transform.Find ("Soundbox").Find("cannonsound").gameObject.GetComponent<AudioSource>();
			}
		} else {
			return;
		}
			

		if (inputDevice != null) {
			//camera rotation
			if (inputDevice.RightBumper.IsPressed && shooting.snowAmount > 0 && !isShooting) {
				StartCoroutine ("ShootBall");
			}
		}
	}

	IEnumerator ShootBall() {
		isShooting = true;
		float power = 0f;
		float time = 0f;
		//force_bar.enabled = true;
		//force_bar.fillAmount = 0;


		//waits until player stops pressing the bumper
		//keeps powering up 
		while (isShooting) {
			anim.SetBool ("isShooting", true);
			time += Time.deltaTime;
			power = shootingPower.Evaluate (Mathf.Min(time, 1f));
			//force_bar.fillAmount = power + 0.1f;
			if (inputDevice == null || !inputDevice.RightBumper.IsPressed) {
                break;
			}

			yield return null;
		}
		anim.SetBool ("isShooting", false);
		shootSound.Play ();
		shooting.removeSnowBall(1);
		Vector3 startingOffset = this.transform.up * forwardOffset;
		GameObject newSnowball = (GameObject)Instantiate(CannonBall, this.transform.position + startingOffset, this.transform.rotation);
		//if (!game.inTutorial) playerScoreMonitor.GetComponent<PlayerScoreMonitor>().shoot += 1;
		Debug.Log ("finalpower = " + power);
		newSnowball.GetComponent<MoveForwardBall> ().speed = .5f;
		newSnowball.GetComponent<SnowBallCollisions>().setPlayerNum(player.GetComponent<Movement>().player_num);
        //force_bar.enabled = false;
        yield return new WaitForSeconds(1f);
		isShooting = false; //just in case

	}

	void FixedUpdate() {
		// Horizontal Rotation
		if (inputDevice == null) {
			return;
		}

		if (player == null || shooting.canMove) {
			return;
		}

		float rotation_Y = - inputDevice.RightStick.X;

		//set bounds for rotation
		if (this.transform.localRotation.y > 0.3f && rotation_Y < 0) {
			rotation_Y = 0;
		}
		if (this.transform.localRotation.y < -0.3f && rotation_Y > 0) {
			rotation_Y = 0;
		}

		transform.Rotate (Vector3.forward, rotation_Y);
	}

	public void resetRotation () {
		Debug.Log ("rotation reset");
		this.transform.rotation = startingRotation;
	}
}
