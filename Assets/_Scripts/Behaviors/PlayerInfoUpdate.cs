using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUpdate : MonoBehaviour {

    public GameObject player;
    int hlimit;
    int alimit;
	Text healthText;
	Text ammoText;

    private void Start() {
		player = transform.parent.root.gameObject;
        hlimit = player.GetComponent<Health>().healthLimit;
		Text[] texts = GetComponentsInChildren<Text> ();
		foreach (Text text in texts) {
			if (text.gameObject.name == "Health_text") {
				healthText = text;
			}
			if (text.gameObject.name == "Ammo_text") {
				ammoText = text;
			}
		}
        if (player.GetComponent<Shooting>() != null) {
            alimit = player.GetComponent<Shooting>().maxSnowAmount;
        } else {
            alimit = 0;
        }
    }

    // Update is called once per frame
    void Update () {
        int health = player.GetComponent<Health>().health;
        int ammo;
        if (player.GetComponent<Shooting>() != null) {
            ammo = player.GetComponent<Shooting>().snowAmount;
			healthText.text = health.ToString () + "/" + hlimit.ToString ();
			ammoText.text = ammo.ToString() + "/" + alimit.ToString();
        } else {
			healthText.text = "" + health.ToString() + "/" + hlimit.ToString();
        }
    }
}
