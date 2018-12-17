using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowWhenSeeDummy : MonoBehaviour {

    public GameObject dummy;
    public GameObject text;

    public GameControl game;

    GameObject player;

    float trh_distance = 15f;

	// Use this for initialization
	void Start () {
        player = gameObject;
    }

    // Update is called once per frame
    void Update() {
        if (!game.inTutorial) {
            text.SetActive(false);
            gameObject.GetComponent<ShowWhenSeeDummy>().enabled = false;
        }
        Vector3 directionToTarget = dummy.transform.position - player.transform.position;
        float angle = Vector3.Angle(player.transform.forward, directionToTarget);
        float distance = directionToTarget.magnitude;
        if (Mathf.Abs(angle) < 30 && distance < trh_distance && player.GetComponent<Shooting>().snowAmount>0 
            && !player.GetComponent<ShowWhenOnCannon>().touchingCannon && !player.GetComponent<ShowWhenOnCannon>().usingCannon) {
            text.SetActive(true);
        } else {
            text.SetActive(false);
        }
    }

}
