using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.UI;

public class Shooting : MonoBehaviour {
	//Shooting projectiles
	public AnimationCurve shootingPower;
	public float shootingPowerModifier = 500f;
	public Vector3 shootingAngleOffset = new Vector3(0f, 1f, 0f);
	public int snowAmount = 0;
	public int maxSnowAmount = 4;

	public bool canMove = true;


	public float rightHandOffset = .5f;
	public float upHandOffset = .25f;

    public GameControl game;
    public GameObject hitNotification;
	public GameObject cancelUI;
    public GameObject playerScoreMonitor;

	public bool isShooting = false;
	public GameObject snowball;
	public Image force_bar;
	public ShowHideSnowBalls ammoTracker;
	public AudioSource shootSound;
	//Controllers
	private int player_num;
	public InputDevice inputDevice;
	private Movement movement;
	bool cancelled = false;
	//camera
	private GameObject camera;

	//UI
	Ammobar ammobarScript;
	Coroutine shootingCoroutine;
	// Use this for initialization
	void Start () {
		movement = GetComponent<Movement> ();
		player_num = movement != null ? movement.player_num : -1;
		camera = GetComponentInChildren<Camera> ().gameObject;
		force_bar.enabled = false;
		ammobarScript = GetComponentInChildren<Ammobar> ();
        game = GameObject.Find("GameControl").GetComponent<GameControl>();
		cancelUI.SetActive (false);

    }

	public void disable() {
		canMove = false;
	}

	public void enable() {
		canMove = true;
	}

    // Update is called once per frame
    void Update () {
		inputDevice = (InputManager.Devices.Count > player_num) ? InputManager.Devices[player_num] : null;

		if (!canMove) {
			return;
		}
		if (inputDevice != null) {
			if (!inputDevice.RightBumper.IsPressed){
				cancelled = false;
			}


			//Right bumper shoots snowballs
			if (inputDevice.RightBumper.IsPressed && snowAmount > 0 && !isShooting && !cancelled) {
				shootingCoroutine = StartCoroutine ("ShootBall");
				cancelUI.SetActive (true);
            }
            else if(inputDevice.RightBumper.WasReleased)
            {
                cancelUI.SetActive(false);
            }

			//LEft Bumper cancel shooting
			if (inputDevice.LeftBumper.IsPressed && isShooting) {
				cancelled = true;
				StopCoroutine (shootingCoroutine);
				force_bar.enabled = false;
				isShooting = false; //just in case
				cancelUI.SetActive (false);

				//speeds back up
				if (movement != null) {
					movement.speedUp ();
				}
			}
		}
	}

    public void HitConfirmation() {
        playerScoreMonitor.GetComponent<PlayerScoreMonitor>().hit += 1;
        StartCoroutine(HitConfirm());
    }

    IEnumerator HitConfirm() {
        hitNotification.SetActive(true);
        hitNotification.GetComponent<PathFollower>().ExternalCall();
        yield return new WaitForSeconds(0.5f);
        hitNotification.SetActive(false);
    }

    IEnumerator ShootBall() {
		isShooting = true;
		float power = 0f;
		float time = 0f;
		force_bar.enabled = true;
		force_bar.fillAmount = 0;

		//slows the player down while shooting
		if (movement != null) {
			movement.slowDown ();
		}

		//waits until player stops pressing the bumper
		//keeps powering up 
		while (isShooting) {
			time += Time.deltaTime;
			power = shootingPower.Evaluate (Mathf.Min(time, 1f));
			force_bar.fillAmount = power + 0.1f;
			if (inputDevice == null || !inputDevice.RightBumper.IsPressed) {
				isShooting = false;
			}

			yield return null;
		}
			
		removeSnowBall(1);
		shootSound.Play ();	
		Vector3 startingOffset = this.transform.right * rightHandOffset;
		Vector3 upDownOffset = this.transform.up * upHandOffset;
		GameObject newSnowball = (GameObject)Instantiate(snowball, this.transform.position + startingOffset + upDownOffset, camera.transform.rotation);
        if (!game.inTutorial) playerScoreMonitor.GetComponent<PlayerScoreMonitor>().shoot += 1;
		Debug.Log ("finalpower = " + power);
		newSnowball.GetComponent<Rigidbody>().AddForce((newSnowball.transform.forward + shootingAngleOffset) * power * shootingPowerModifier);
		newSnowball.GetComponent<SnowBallCollisions>().setPlayerNum(player_num);
        newSnowball.GetComponent<SnowBallCollisions>().owner = this;
		force_bar.enabled = false;
		isShooting = false; //just in case

		//speeds back up
		if (movement != null) {
			movement.speedUp ();
		}
	}

	public void addSnowBall(int amount) {
		snowAmount = Mathf.Min(maxSnowAmount, snowAmount + amount);
		if (ammoTracker != null) {
			ammoTracker.updateBalls (snowAmount);
		}
		ammobarScript.increase_ammo (snowAmount);
	}

	public void removeSnowBall(int amount) {
		snowAmount = Mathf.Max(0, snowAmount - amount);
		if (ammoTracker != null) {
			ammoTracker.updateBalls (snowAmount);
		}
		ammobarScript.decrease_ammo (snowAmount);
	}

}
