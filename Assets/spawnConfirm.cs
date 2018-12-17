using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnConfirm : MonoBehaviour {

    public GameObject confirmInfo;
    GameObject confirm;


    public void Spawn() {
        confirm = Instantiate(confirmInfo, transform.position, Quaternion.identity);
        confirm.GetComponent<PathFollower>().anchor = gameObject;
        //StartCoroutine(DestroyIt());
    }

    IEnumerator DestroyIt() {
        yield return new WaitForSeconds(0.5f);
        Destroy(confirm);
    }


}
