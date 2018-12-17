using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowmanDeath : MonoBehaviour {

    public GameObject snow_pile_hat;
    GameObject snow_pile_hat_clone;

    GameObject snowman_purple;
    GameObject snowman_gold;

    bool goldDead = false;
    bool purpleDead = false;

    private void Start()
    {
        snowman_purple = GameObject.Find("snowman_purple");
        snowman_gold = GameObject.Find("Snowman_Gold");
        
    }

    private void Update()
    {
        if(!purpleDead && snowman_purple.GetComponent<Health>().health == 0)
        {
            purpleDead = true;
            Vector3 snowman_purple_death_pos = new Vector3(snowman_purple.transform.position.x-0.24f, snowman_purple.transform.position.y-0.5f, snowman_purple.transform.position.z);
            snow_pile_hat_clone = Instantiate(snow_pile_hat, snowman_purple_death_pos, Quaternion.identity) as GameObject;
        }
        if(!goldDead && snowman_gold.GetComponent<Health>().health == 0)
        {
            goldDead = true;
            Vector3 snowman_gold_death_pos = new Vector3(snowman_gold.transform.position.x, snowman_gold.transform.position.y - 0.5f, snowman_gold.transform.position.z-0.6f);
            snow_pile_hat_clone = Instantiate(snow_pile_hat, snowman_gold_death_pos, Quaternion.identity) as GameObject;
        }
    }
}
