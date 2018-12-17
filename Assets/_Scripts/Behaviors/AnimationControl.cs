using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour {
	Animator anim;
	Shooting shootingScript;
	Rigidbody rb;
	public AudioSource collectSound;
	CollectSnow collectScript;
    bool isWalking = false;
    public bool victory = false;
    Coroutine walkAnimationCoroutine;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		shootingScript = GetComponent<Shooting> ();
		rb = GetComponent<Rigidbody> ();
		collectScript = GetComponent<CollectSnow> ();
	}
	
	// Update is called once per frame
	void Update(){
		anim.SetBool ("Aiming", shootingScript.isShooting);
		anim.SetBool ("Collecting", collectScript.gatheringSnow);
		if (collectScript.gatheringSnow){
			if (!collectSound.isPlaying) {
				collectSound.Play ();
			}
		}else{
			collectSound.Stop ();
		}
        anim.SetBool("Victory", victory);
		if (!collectScript.gatheringSnow && rb.velocity.magnitude > 0.5f){
            if (!isWalking)
            {
                if (walkAnimationCoroutine != null)
                {
                    StopCoroutine(walkAnimationCoroutine);
                }
                walkAnimationCoroutine = StartCoroutine(setWalking());
            }
		}else{
            if (isWalking)
            {
                if (walkAnimationCoroutine != null)
                {
                    StopCoroutine(walkAnimationCoroutine);
                }
                walkAnimationCoroutine = StartCoroutine(resetWalking());
            }
		}
	}

    IEnumerator setWalking()
    {
        isWalking = true;
        float t = 0;
        while (t < 1)
        {
            t += 0.05f;
            anim.SetLayerWeight(1, t);
            yield return new WaitForFixedUpdate();
        }
        anim.SetLayerWeight(1, 1);

    }

    IEnumerator resetWalking()
    {
        isWalking = false;
        float t = 1;
        while (t < 1)
        {
            Debug.Log(t);
            t -= 0.1f;
            anim.SetLayerWeight(1, t);
            yield return new WaitForFixedUpdate();
        }
        anim.SetLayerWeight(1, 0);

    }
}
