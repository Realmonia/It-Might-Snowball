using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowPile : MonoBehaviour {
	public int snow_amount = 10;
	public int amount_needed_for_fort = 5;
	Vector3 originalScale;
	Material material;

	bool discovered = false;
	void Start(){
		originalScale = transform.localScale;
		material = GetComponent<Renderer> ().material;
	}

	void Update(){
		if (!discovered) {
			material.EnableKeyword ("_EMISSION");
			float emission = Mathf.PingPong (Time.time/3, 0.25f);
			Color baseColor = Color.HSVToRGB (0.4675f, 0.97f, 0.183f);
			Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
			material.SetColor ("_EmissionColor", finalColor);
		}else{
			material.DisableKeyword ("_EMISSION");
		}
	}
	public void removeSnow (int amount) {
		snow_amount = Mathf.Max (0, snow_amount - amount);

		//remove size, etc here
		resize(snow_amount);
	}

	void resize(int amount){
		Vector3 newScale = new Vector3 (originalScale.x * (float)amount / 10, originalScale.y, originalScale.z * (float)amount / 10);
		transform.localScale = newScale;
		if (amount <= 0){
			GetComponent<SphereCollider> ().center = new Vector3 (0, -3, 0);
		}

	}

	void OnTriggerEnter(Collider other2) {
		GameObject other = getParent (other2.gameObject);
		CollectSnow collecter = other.GetComponent<CollectSnow> ();
		if (collecter != null) {
			collecter.touchingSnowPile = true;
			collecter.snowPile = this;
			discovered = true;
		}
	}

	void OnTriggerStay(Collider other2){
		GameObject other = getParent (other2.gameObject);
		CollectSnow collecter = other.GetComponent<CollectSnow> ();
		if (collecter != null) {
			collecter.touchingSnowPile = true;
			collecter.snowPile = this;
			discovered = true;
		}
	}

	void OnTriggerExit(Collider other2) {
		GameObject other = getParent (other2.gameObject);
		CollectSnow collecter = other.GetComponent<CollectSnow> ();
		if (collecter != null) {
			collecter.touchingSnowPile = false;
			collecter.snowPile = null;
			discovered = false;
		}
	}

	public GameObject getParent(GameObject child) {
		Transform childTransform = child.transform;
		if (childTransform.parent == null) {
			return child;
		}
		return getParent (childTransform.parent.gameObject);
	}

	public void createFort() {

	}
}
