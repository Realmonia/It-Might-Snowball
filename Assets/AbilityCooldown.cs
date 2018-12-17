using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour {
	public Color ability_active;
	Image dash_fill;
	Image attack_fill;
    Vector3 origScale;
    Coroutine dash_cooldown_coroutine;
    Coroutine attack_cooldown_coroutine;
	// Use this for initialization
	void Start () {
		Image[] temp = GetComponentsInChildren<Image> ();
		foreach(Image image in temp){
			if (image.gameObject.name == "dash_fill"){
				dash_fill = image;
			}
			if (image.gameObject.name == "attack_fill"){
				attack_fill = image;
			}
		}
        origScale = dash_fill.rectTransform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (dash_fill == null || attack_fill == null){
			this.enabled = false;
		}
	}

	public void using_dash(float duration){
        if (dash_cooldown_coroutine != null) {
            StopCoroutine(dash_cooldown_coroutine);
        }
        StartCoroutine (using_coroutine(dash_fill, duration));
	}

	IEnumerator using_coroutine(Image fill, float duration){
        fill.rectTransform.localScale = origScale;
		fill.color = ability_active;
		yield return new WaitForSeconds (duration);
		fill.color = Color.white;
	}

	public void dash_cooldown(float cooldown){
        dash_cooldown_coroutine = StartCoroutine(cooldown_coroutine (dash_fill, cooldown));
	}

	IEnumerator cooldown_coroutine(Image fill, float cooldown){
		float time = 0;
		while (time < cooldown){
			time += Time.deltaTime;
			fill.fillAmount = time / cooldown;
			yield return null;
		}
		time = 0;
		while(time < 0.2f){
			time += Time.deltaTime;
			fill.rectTransform.localScale *= 1.05f;
			yield return null;
		}
		time = 0;
		while(time < 0.2f){
			time += Time.deltaTime;
			fill.rectTransform.localScale /= 1.05f;
			yield return null;
		}
	}

	public void using_attack(float duration){
        if (attack_cooldown_coroutine != null) {
            StopCoroutine(attack_cooldown_coroutine);
        }
        StartCoroutine (using_coroutine(attack_fill, duration));
	}


	public void attack_cooldown(float cooldown){
        attack_cooldown_coroutine = StartCoroutine(cooldown_coroutine (attack_fill, cooldown));
	}
}
