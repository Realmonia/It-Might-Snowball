using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBattle : MonoBehaviour {

    public float acc = 10f;
    float time = 0f;
    public GameObject anchor;
    Coroutine a;

    // Use this for initialization
    void Start() {
        Debug.Log(gameObject.name);
        FloatGo();
    }

    // Update is called once per frame
    public void FloatGo () {
        a = StartCoroutine(FloatInfo());
    }

    public void Reset() {
        time = 0f;
        StopCoroutine(a);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    IEnumerator FloatInfo() {
        yield return new WaitForSeconds(2f);
        while (true) {
            gameObject.transform.position += new Vector3(0f, acc * Mathf.Pow(Time.deltaTime,5) , 0f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
