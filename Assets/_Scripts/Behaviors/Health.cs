using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	public int health = 3;
    public int healthLimit = 3;
    public GameControl game;
    public GameObject playerInfo;
    public GameObject hurtAlert;
    public GameObject snowmanScoreMonitor;
	Healthbar healthbarScript;

	ScaleSnowman scaleScript;

	void Start(){
		scaleScript = GetComponent<ScaleSnowman> ();
		healthbarScript = GetComponentInChildren<Healthbar> ();
        game = GameObject.Find("GameControl").GetComponent<GameControl>();
    }
    public void removeHealth(int amount) {
		health = Mathf.Max (health - amount, 0);
		if (scaleScript) scaleScript.change (health);
		healthbarScript.loseHealth (health, healthLimit);
        if (amount > 0) {
            StartCoroutine(Alert());
        }
        if (gameObject.GetComponent<PlayerType>().isSnowMan) {
            if (!game.inTutorial) snowmanScoreMonitor.GetComponent<SnowmanScoreMonitor>().damageTake += 1;
        }

	}

    IEnumerator Alert() {
        for (int i =0; i< 4; ++i) {
            yield return new WaitForSeconds(0.1f);
            hurtAlert.GetComponent<Image>().color = hurtAlert.GetComponent<Image>().color + new Color32(255, 255, 255, 16);
        }
        for (int i = 0; i < 4; ++i) {
            yield return new WaitForSeconds(0.1f);
            hurtAlert.GetComponent<Image>().color = hurtAlert.GetComponent<Image>().color - new Color32(255, 255, 255, 16);
        }
        yield break;
    }



    IEnumerator wait() {
		yield return null;
		this.gameObject.SetActive (false);
	}

	public void addHealth(int amount) {
		health = Mathf.Min (health + amount, healthLimit);
		scaleScript.change (health);
		healthbarScript.regenHealth (health, healthLimit);

	}

    private void Update() {
        if (health <= 0) {
            game.PlayerDie(gameObject);
        }
    }

	public bool canBeHealed() {
		if (health < healthLimit) {
			return true;
		}

		return false;
	}
}
