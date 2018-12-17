using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowBallCollisions : MonoBehaviour {
	public int player_num = -1;
	public float destructionTime = 1f;
	public Shooting owner;
    ControllerVibration vibrationCode;
	//stops from hitting two things
	private int snowballHeath = 1;

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem> ().Stop();
        vibrationCode = GameObject.Find("GameControl").GetComponent<ControllerVibration>();

    }
	

	void OnTriggerEnter(Collider otherCollider) {
        collisionCode(otherCollider);
	}

    void OnTriggerStay(Collider otherCollider) {
        collisionCode(otherCollider);
    }

    void collisionCode(Collider otherCollider) {
        if (otherCollider.gameObject.layer == 9) { //other player

            //tries to grab movement from player
            GameObject other = otherCollider.gameObject;
            other = getParent(otherCollider.gameObject);

            Movement movement = other.GetComponent<Movement>();

            //stops player from hitting themselves and their teammates
            if (movement != null && movement.player_num != player_num &&
				movement.player_num != player_num + 1) {
                //makes sure snowball can only hit one player
                snowballHeath -= 1;

                if (snowballHeath > -1) {

                    //Remove Health?
                    other.GetComponent<Health>().removeHealth(1);
                    

                    other.GetComponentInChildren<CameraPositionFix> ().CameraShake ();

					if (other.GetComponent<PlayerType>().isSnowMan){
						other.GetComponent<Movement> ().StartCoroutine(other.GetComponent<Movement> ().pushback ());
					}
					other.GetComponentInChildren<HurtSound> ().play (true);

					if (owner != null) {
						owner.HitConfirmation ();
                        vibrationCode.vibrate(false, owner.inputDevice, 0.2f, 5);

                    }
					var main = GetComponent<ParticleSystem> ().main;
					main.startColor = new Color(255f, 0f, 0f);
                    StartCoroutine("snowBallDestruction");
                }
            }


            if (movement != null && movement.player_num != player_num &&
				movement.player_num == player_num + 1) {
				var main = GetComponent<ParticleSystem> ().main;
				main.startColor = new Color(255f, 0f, 0f);
                StartCoroutine("snowBallDestruction");
            }
        }

		//hit wall
		Debug.Log(otherCollider.gameObject.layer);
        if (otherCollider.gameObject.layer == 0 || otherCollider.gameObject.layer == 11) {
			Debug.Log ("Snowball error");
			StartCoroutine("snowBallDestruction");
        }
        if (otherCollider.gameObject.layer == 12) { //dummy
			otherCollider.gameObject.GetComponent<OnHitRotate>().SnowballHitRotate();
			var main = GetComponent<ParticleSystem> ().main;
			main.startColor = new Color(255f, 0f, 0f);
            StartCoroutine("snowBallDestruction");
        }

    }

	public void setPlayerNum(int playerIn) {
		player_num = playerIn;
	}

	public GameObject getParent(GameObject child) {
		Transform childTransform = child.transform;
		if (childTransform.parent == null) {
			return child;
		}
		return getParent (childTransform.parent.gameObject);
	}

	IEnumerator snowBallDestruction() {
		float time = 0f;

		//stops motion and turns on particles
		Rigidbody rigid = GetComponent<Rigidbody> ();
		if (rigid != null) {
			GetComponent<Rigidbody> ().useGravity = false;
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}
		GetComponent<ParticleSystem> ().Play();
		GetComponent<MeshRenderer> ().enabled = false;

		//after destructionTime seconds, destroys object
		while (time < destructionTime) {
			time += Time.deltaTime;
			yield return null;
		}


		Destroy (this.gameObject);
	}
}
