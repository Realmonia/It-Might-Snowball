using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvertUIPosition : MonoBehaviour {
	public float offset_y = 0;
	public float offset_x = 0;

	Camera Main_Camera;
	GameObject rootParent;
	int player_num;
	Vector3 rawConvertedPosition;


    void Start() {
        rootParent = transform.root.gameObject;
        if (rootParent != null) {
            Main_Camera = rootParent.GetComponentInChildren<Camera>();
            player_num = rootParent.GetComponent<Movement>().player_num;
        }
        if (this.gameObject.name == "Healing_text") {
            offset_y = -520;
        } else {
            offset_y = -600;
        }
    }
	// Update is called once per frame
	void Update () {
		rawConvertedPosition = Main_Camera.WorldToScreenPoint (rootParent.transform.position);
		// This might need reconfiguration for 2 displays
		// For player at right side of the split screen
		if (player_num >= 2) {
			rawConvertedPosition.x -= Screen.width / 2;
		}
		// For player at bottom side of the split screen
		if (player_num % 2 != 0){
			rawConvertedPosition.y += Screen.height / 2;
		}
		rawConvertedPosition.x += offset_x;
		rawConvertedPosition.y += offset_y;
		GetComponent<RectTransform> ().anchoredPosition = rawConvertedPosition;

	}
}
