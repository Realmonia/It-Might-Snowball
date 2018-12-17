using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {

	// Use this for initialization
	void Start () {

        StartCoroutine(FadeOutFromStart());

	}

    IEnumerator FadeOutFromStart () {
        for (int i=0; i < 128; ++i) {
            gameObject.GetComponent<Image>().color += new Color32(0, 0, 0, 1);
            yield return new WaitForSeconds(0.05f);
        }
    }
	
}
