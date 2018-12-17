using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionFix : MonoBehaviour {
	LayerMask terrain_layer = 1<<11;
	Vector3 originalPos;
	Vector3 originalWorldPos;
	public bool shake;

	// Use this for initialization
	void Start () {
		
	}

	public void CameraShake(){
		originalPos = transform.localPosition;

		originalWorldPos = transform.position;
		StartCoroutine (shakeCamera());
	}

	IEnumerator shakeCamera(){
		float time = 0f;
		float calculated_y = 0;
		float z_before_pushing = 0;
		Vector3 temp = Vector3.zero;
		Quaternion temp2 = Quaternion.identity;
		while (time < 0.5f){
			time += Time.deltaTime;
			temp = Random.insideUnitSphere * 0.05f;
			temp2 = transform.localRotation;
			temp2.y = temp.x;
			temp2.z = temp.z;
			transform.localRotation = temp2;
			yield return new WaitForFixedUpdate();
		}
		temp2.y = 0;
		temp2.z = 0;
		transform.localRotation = temp2;
	}

	IEnumerator CameraMoveFoward(){
		float end_y = this.transform.localPosition.y * (-6f / this.transform.localPosition.z);
		Vector3 endpoint = new Vector3(this.transform.localPosition.x, end_y, this.transform.localPosition.z+0.5f);
		for (float t = 0; t < 0.2f; t += Time.deltaTime){
			this.transform.localPosition = Vector3.Lerp (this.transform.localPosition, endpoint, 0.5f);
			yield return null;
		}
	}


}
