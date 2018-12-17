using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class SnowmanAttack : MonoBehaviour {
	public float attackCoolDown = 0.75f;
	InputDevice inputdevice;
    Movement movementScript;
    bool isAttacking = false;
    SnowmanAnimationControl animControl;
	AbilityCooldown cooldownUI;

    public GameControl game;
    public GameObject hitConfirm;
    public GameObject snowmanScoreMonitor;

    private TrailRenderer snowmangold_leftarm_trail;
    private TrailRenderer snowmangold_rightarm_trail;
    private TrailRenderer snowmanpurple_leftarm_trail;
    private TrailRenderer snowmanpurple_rightarm_trail;


    private bool canDealDamage = false;
    // Use this for initialization
    void Start() {
        movementScript = GetComponent<Movement>();
        animControl = GetComponent<SnowmanAnimationControl>();
		cooldownUI = GetComponentInChildren<AbilityCooldown> ();
        game = GameObject.Find("GameControl").GetComponent<GameControl>();
        snowmangold_leftarm_trail = GameObject.Find("snowmangold_leftarm_trail").GetComponent<TrailRenderer>();
        snowmangold_rightarm_trail = GameObject.Find("snowmangold_rightarm_trail").GetComponent<TrailRenderer>();
        snowmanpurple_leftarm_trail = GameObject.Find("snowmanpurple_leftarm_trail").GetComponent<TrailRenderer>();
        snowmanpurple_rightarm_trail = GameObject.Find("snowmanpurple_rightarm_trail").GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (inputdevice != null) {
            if (inputdevice.RightBumper.IsPressed) {
                if (!isAttacking) {
                    StartCoroutine(Attack());
                }
            }
        }
    }

    void LateUpdate() {
        inputdevice = movementScript.getInputDevice();
    }

    IEnumerator Attack() {
        /**********************/
        snowmangold_leftarm_trail.enabled = true;
        snowmangold_rightarm_trail.enabled = true;
        snowmanpurple_leftarm_trail.enabled = true;
        snowmanpurple_rightarm_trail.enabled = true;
        /**********************/
        isAttacking = true;
        movementScript.slowDown();
        animControl.beginAttack();
        startAttack();
		cooldownUI.using_attack (0.5f);
        yield return new WaitForSeconds(0.5f);
        endAttack();
        movementScript.speedUp();
        animControl.endAttack();
        /**********************/
        snowmangold_leftarm_trail.enabled = false;
        snowmangold_rightarm_trail.enabled = false;
        snowmanpurple_leftarm_trail.enabled = false;
        snowmanpurple_rightarm_trail.enabled = false;
        /**********************/
        cooldownUI.attack_cooldown (attackCoolDown);
		yield return new WaitForSeconds(attackCoolDown);
        isAttacking = false;
    }

    public void startAttack() {
        canDealDamage = true;
    }
    public void endAttack() {
        canDealDamage = false;
    }


    void OnTriggerEnter(Collider otherCollider) {
        if (canDealDamage) {
            Debug.Log(otherCollider.gameObject.name);
            if (otherCollider.gameObject.layer == 9) {
                GameObject otherParent = otherCollider.gameObject;
                otherParent = getParent(otherCollider.gameObject);
                if (otherParent.GetComponent<Movement>().player_num ==
                   this.gameObject.GetComponent<Movement>().player_num - 1) {
                    return;
                }

                endAttack();
                GameObject other = otherCollider.gameObject;
                other = otherCollider.transform.root.gameObject;
                StartCoroutine(HitConfirm());
                game.gameObject.GetComponent<ControllerVibration>().vibrate(true, inputdevice, 0.3f, 3f);
                if (!game.inTutorial) snowmanScoreMonitor.GetComponent<SnowmanScoreMonitor>().damageCause += 1;
                other.GetComponent<Health>().removeHealth(1);
            }
            if (otherCollider.gameObject.layer == 12) {
                otherCollider.gameObject.GetComponent<OnHitRotate>().SnowmanHitRotate();
            }
        }
    }

    IEnumerator HitConfirm() {
        hitConfirm.SetActive(true);
        hitConfirm.GetComponent<PathFollower>().ExternalCall();
        yield return new WaitForSeconds(0.5f);        
        hitConfirm.SetActive(false);
    }

    public GameObject getParent(GameObject child) {
        Transform childTransform = child.transform;
        if (childTransform.parent == null) {
            return child;
        }
        return getParent(childTransform.parent.gameObject);
    }
}