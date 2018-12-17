using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {
	Image green_bar;
	Image red_bar;
	Coroutine current_coroutine;
	// Use this for initialization
	void Start () {
		Image[] children = GetComponentsInChildren<Image> ();
		foreach(Image image in children){
			if (image.gameObject.name == "red"){
				red_bar = image;
			}
			if (image.gameObject.name == "green"){
				green_bar = image;
			}
		}
	}



	public void loseHealth(int currentHealth, int maxHealth){
		green_bar.fillAmount = (float)currentHealth / maxHealth;
		if (current_coroutine != null){
			StopCoroutine (current_coroutine);
		}
		current_coroutine = StartCoroutine (red_decrease (green_bar.fillAmount));
	}

	public void regenHealth(int currentHealth, int maxHealth){
		float target = (float)currentHealth / maxHealth;
		if (current_coroutine != null){
			StopCoroutine (current_coroutine);
		}
		current_coroutine = StartCoroutine (green_increase (target));
	}

	IEnumerator red_decrease(float target_amount){
		float amount_to_decrease = red_bar.fillAmount - target_amount;
		while (red_bar.fillAmount > target_amount) {
			red_bar.fillAmount -= amount_to_decrease / 10;
			yield return null;
		}
	}

	IEnumerator green_increase(float target_amount){
		float amount_to_increase = target_amount - green_bar.fillAmount;
		while (green_bar.fillAmount < target_amount) {
			green_bar.fillAmount += amount_to_increase / 5;
			if (green_bar.fillAmount > target_amount) {
				green_bar.fillAmount = target_amount;
			}
			
			yield return null;
		}
		red_bar.fillAmount = green_bar.fillAmount;
	}
}
