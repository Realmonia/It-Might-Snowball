using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignColor : MonoBehaviour {
	public MeshRenderer bodyColorRenderer;

	public Material gold_material;
	public Material purple_material;

	bool assigned = false;
	
	// Update is called once per frame
	void Update () {
		if (!assigned){
			int player = GetComponent<Movement> ().player_num;
			if (player < 2){
				// Gold
				bodyColorRenderer.material = gold_material;
			} else{
				bodyColorRenderer.material = purple_material;
			}
			assigned = true;
			this.enabled = false;
		}
	}
}
