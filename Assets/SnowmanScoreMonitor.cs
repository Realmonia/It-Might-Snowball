using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowmanScoreMonitor : MonoBehaviour {

    public GameObject snowman;
    public Text textDamage;
    public Text textDamageTaken;
    public int damageTake = 0;
    public int damageCause = 0;


    private void Update() {
        textDamage.text = damageCause.ToString();
        textDamageTaken.text = damageTake.ToString();
    }

}
