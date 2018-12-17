using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour {

    public Vector3[] path;
    public GameObject anchor;
    public float speed = 5.0f;
    public float reachDist = 1.0f;
    public int currentPoint = 0;
	public bool finish = false;
    public float speedMultiplier = 1f;

    Vector3 offset;

    public AnimationCurve anim;

    private bool ready = false;

    private void Awake() {
        offset = transform.position - anchor.transform.position;
    }

    private void Start() {
        if (path.Length > 0) {
            transform.position = anchor.transform.position + path[0] + offset;
        }
        StartCoroutine(In());
    }

    public void ExternalCall() {
        StartCoroutine(InOrz());
    }

    // Update is called once per frame
    void Update () {
        if (!finish && ready && path.Length>0) {
            float dist = Vector3.Distance(anchor.transform.position + path[currentPoint] + offset, transform.position);

            transform.position = Vector3.Lerp(transform.position, anchor.transform.position + path[currentPoint] + offset, Time.deltaTime * speed);

            if (dist <= reachDist) {
                currentPoint++;
            }

            if (currentPoint >= path.Length) {
                finish = true;
            }
        }
	}

    IEnumerator In() {
        float curveTime = 0f;
        float curveAmount = anim.Evaluate(curveTime);

        while (curveTime < 1f) {
            curveTime += Time.deltaTime * speedMultiplier;
            curveAmount = anim.Evaluate(curveTime);
            gameObject.transform.localScale = new Vector3(curveAmount, curveAmount, 1f);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        ready = true;
    }

    IEnumerator InOrz() {
        float curveTime = 0f;
        float curveAmount = anim.Evaluate(curveTime);

        while (curveTime < 1f) {
            curveTime += Time.deltaTime * speedMultiplier;
            curveAmount = anim.Evaluate(curveTime);
            gameObject.transform.localScale = new Vector3(curveAmount, curveAmount, 1f);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        float dist = Vector3.Distance(anchor.transform.position + path[currentPoint] + offset, transform.position);

        transform.position = Vector3.Lerp(transform.position, anchor.transform.position + path[currentPoint] + offset, Time.deltaTime * speed);

        if (dist <= reachDist) {
            currentPoint++;
        }

        if (currentPoint >= path.Length) {
            finish = true;
        }
    }
}
