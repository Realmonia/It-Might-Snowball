using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreMonitor : MonoBehaviour {

    public GameObject player;
    public Text textShoot;
    public Text textHit;
    public int shoot = 0;
    public int hit = 0;

    private void Start() {
    }

    private void Update() {
        if (shoot == 0) {
            textShoot.text = shoot.ToString();
            textHit.text = "N/A";
        } else {
            textShoot.text = shoot.ToString();
            textHit.text = 
                ((int)((float)hit / (float)shoot * 100f)).ToString() + "%";
        }
    }

}
