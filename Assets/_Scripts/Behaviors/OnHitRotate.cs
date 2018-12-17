using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitRotate : MonoBehaviour {

    public GameObject player;
    public GameObject snowman;
    public GameObject tutorialWall;

    private void Start() {
        Collider cd = gameObject.GetComponent<CapsuleCollider>();
    }

    public void SnowballHitRotate() {
        Debug.Log("Snowball hit!");
        if (tutorialWall.GetComponent<TutorialEventChecker>().monitorPG1)
            tutorialWall.GetComponent<TutorialEventChecker>().tutorialEvent[0] = true;
        StartCoroutine(Rotate());
    }

    public void SnowmanHitRotate() {
        Debug.Log("Snowman hit!");
        if (tutorialWall.GetComponent<TutorialEventChecker>().monitorSG2)
            tutorialWall.GetComponent<TutorialEventChecker>().tutorialEvent[3] = true;
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate() {
        for (int i = 0; i < 50; ++i) {
            transform.Rotate(- Vector3.up * Time.deltaTime*360f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
