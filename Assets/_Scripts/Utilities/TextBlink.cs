using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour {

    public Text text;
    public string stream;
    private string current_str;
    Coroutine a;
    private bool[] finish;
    public GameObject gameGoal;

    private void Start() {
        finish = new bool[1];
        finish[0] = false;
    }

    private void Update() {
        if (finish[0]) {
            gameGoal.SetActive(true);
            finish[0] = false;
        }
    }

    public void StartShow() {
        Debug.Log("start show");
        text.enabled = true;
        a = StartCoroutine(AddWordEachTime(0));
    }

    public void DestroyString() {
        current_str = "";
        if (a != null) {
            StopCoroutine(a);
        }
    }

    IEnumerator AddWordEachTime(int id) {
        int len = stream.Length;
        for (int i = 0; i < len; ++i) {
            current_str += stream[i];
            text.text = current_str;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(2f);
        for (int i = 0; i<4; ++i) {
            text.enabled = !text.enabled;
            yield return new WaitForSeconds(0.5f);
        }
        text.text = "";
        yield return new WaitForSeconds(0.5f);
        finish[id] = true;
        yield break;
    }
}
