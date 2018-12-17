using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Movement : MonoBehaviour {

	public float movement_speed;
	public int player_num;
	public float slowdown_modifier = .5f;
	InputDevice inputDevice;
	Rigidbody rb;
	GameObject player_camera;
	SnowmanAnimationControl animControl;
	AbilityCooldown cooldownUI;
    private float rotationSpeed = 1f;
    public float minRotationSpeed = .5f;
	public bool canMove = true;
	Animator anim;
	public bool push;

	public float dashForce;
    public float dashDuration;
	public float dashCoolDown;
	public float knockbackForce = 10000f;
	private bool snowman;
	private bool isDashing = false;
	private bool canDash = true;
	private float time;
	private float starting_speed;

	// Use this for initialization
	void Start () {
		rb = GetComponentInChildren<Rigidbody> ();
		player_camera = GetComponentInChildren<Camera> ().gameObject; 
		starting_speed = movement_speed;
		animControl = GetComponent<SnowmanAnimationControl> ();
        snowman = GetComponent<PlayerType>().isSnowMan;
		cooldownUI = GetComponentInChildren<AbilityCooldown> ();
		anim = GetComponent<Animator> ();
    }

	public void disable() {
		canMove = false;
	}

	public void enable() {
		canMove = true;
	}

    // Update is called once per frame
	void Update () {
		if (push){
			push = false;
			StartCoroutine(pushback ());
		}
		inputDevice = (InputManager.Devices.Count > player_num) ? InputManager.Devices[player_num] : null;
	
		if (!canMove) {
			return;
		}

		if (inputDevice != null) {

			if (snowman && inputDevice.Action3.IsPressed && canDash) {
                //need to disable the snowman attack here
                GetComponent<SnowmanAttack>().enabled = false;
                StartCoroutine(Dash ());
            }
        }

	}

	public InputDevice getInputDevice() {
		return inputDevice;
	}

	void FixedUpdate(){
		if (!canMove) {
			return;
		}

		if (inputDevice != null){
			// Movement
			Vector3 forward_movement = transform.forward * inputDevice.Direction.Y;
			Vector3 horizontal_movement = transform.right * inputDevice.Direction.X;
			Vector3 falling_velocity = new Vector3(0, rb.velocity.y, 0);
			if (isDashing){
				
				rb.AddForce ((forward_movement + horizontal_movement) * dashForce*1.8f);
			}else{
				rb.velocity = (movement_speed * (forward_movement + horizontal_movement) + falling_velocity);
			}

			// Horizontal Rotation
			float rotation_Y = inputDevice.RightStick.X;
			transform.Rotate (Vector3.up, rotation_Y*2*rotationSpeed);

			// Lookup and Lookdown
			float rotation_x = inputDevice.RightStick.Y;
			if (player_camera.transform.localRotation.x > 0.15f && rotation_x <0) {
				rotation_x = 0;
			}
			else if (player_camera.transform.localRotation.x < -0.09f && rotation_x >0) {
				rotation_x = 0;
			}else{
				player_camera.transform.RotateAround (this.transform.position, this.transform.right, -rotation_x*rotationSpeed);
			}
			anim.SetFloat ("walkSpeed", rb.velocity.magnitude/4);
		}
	}

	//modifies movement speed
	public void slowDown() {
		movement_speed = starting_speed * slowdown_modifier;
	}

	//modifies movement speed
	public void speedUp() {
		movement_speed = starting_speed;
	}

    public void rotationSlowDown() {
        rotationSpeed = minRotationSpeed;
    }

    public void rotationSpeedUp() {
        rotationSpeed = 1f;
    }

	IEnumerator Dash(){
        StartCoroutine(dashCameraStart ());
		canDash = false;
		isDashing = true;
		animControl.beginDashing ();
        /*******************************/

        /*******************************/
        cooldownUI.using_dash (dashDuration);
		yield return new WaitForSeconds (dashDuration);
		isDashing = false;
		animControl.endDashing ();
        /*******************************/

        /*******************************/
        StartCoroutine(dashCameraEnd ());
        //need to re-enable snowman attack
        GetComponent<SnowmanAttack>().enabled = true;

        cooldownUI.dash_cooldown (dashCoolDown);
		yield return new WaitForSeconds (dashCoolDown);
		canDash = true;
	}

	IEnumerator dashCameraStart(){
		Debug.Log ("here");
		float end_y = player_camera.transform.localPosition.y * (-6.5f / player_camera.transform.localPosition.z);
		Vector3 endpoint = new Vector3(player_camera.transform.localPosition.x, end_y, -6.5f);
		for (float t = 0; t < 0.5f; t += Time.deltaTime){
			player_camera.transform.localPosition = Vector3.Lerp (player_camera.transform.localPosition, endpoint, 0.2f);
			yield return null;
		}

	}

	IEnumerator dashCameraEnd(){
		float end_y = player_camera.transform.localPosition.y * (-4f / player_camera.transform.localPosition.z);
		Vector3 endpoint = new Vector3(player_camera.transform.localPosition.x, end_y, -4f);
		for (float t = 0; t < 0.5f; t += Time.deltaTime){
			player_camera.transform.localPosition = Vector3.Lerp (player_camera.transform.localPosition, endpoint, 0.2f);
			yield return null;
		}

	}

    public IEnumerator pushback()
    {
        float timer = 0f;
        while (timer < 0.2f)
        {
            timer += Time.deltaTime;
            rb.velocity = Vector3.zero;
            rb.AddForce(-transform.forward * knockbackForce);
            yield return new WaitForFixedUpdate();
        }

    }


}
