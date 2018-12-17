using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndShot : MonoBehaviour {

    GameObject gold_snowman;
    GameObject gold_player;
    GameObject purple_snowman;
    GameObject purple_player;
    Animator anim;
    GameControl gamecontrol;

    bool isPlaying = true;

    bool gold_introduced = false;
    bool purple_introduced = false;

    int point = 0;

    bool finish = false;
    bool ready = false;

    public float speed = 10f;
    public float reachDist = 1.0f;

    public int aliveCount = 0;

    GameObject[] travelList;

    // Use this for initialization
    void Start() {
        StartCoroutine(Orz());
    }

    IEnumerator Orz() {
        Time.timeScale = 0.25f;
        gameObject.transform.position -= gameObject.transform.forward * 5f;
        for (int i = 0; i< 100; ++i) {
            yield return new WaitForSeconds(0.005f);
            Rect rec = gameObject.GetComponent<Camera>().rect;
            if (rec.width != 1f) {
                rec.width += 0.05f;
                rec.height += 0.05f;
            }
            if (rec.x!=0f) {
                rec.x -= 0.05f;
            }
            if (rec.y!=0f) {
                rec.y -= 0.05f;
            }
            gameObject.GetComponent<Camera>().rect = rec;
        }
        Time.timeScale = 1;
        gold_snowman = GameObject.Find("Snowman_Gold");
        gold_player = GameObject.Find("Player_Gold");
        purple_player = GameObject.Find("Player_Purple");
        purple_snowman = GameObject.Find("snowman_purple");
        anim = GetComponent<Animator>();
        gold_snowman.GetComponent<Movement>().enabled = false;
        gold_player.GetComponent<Movement>().enabled = false;
        purple_snowman.GetComponent<Movement>().enabled = false;
        purple_player.GetComponent<Movement>().enabled = false;
        gamecontrol = GameObject.Find("GameControl").GetComponent<GameControl>();
        travelList = new GameObject[4];
        if (!gamecontrol.deadPlayers.Contains(0)) {
            travelList[0] = gold_player;
            aliveCount += 1;
            gold_player.GetComponent<AnimationControl>().victory = true;
            gold_player.transform.forward = new Vector3(31.3f, gold_player.transform.position.y, 47.3f) - gold_player.transform.position;
        } else {
            travelList[0] = null;
        }
        if (!gamecontrol.deadPlayers.Contains(1)) {
            travelList[1] = gold_snowman;
            aliveCount += 1;
            gold_snowman.GetComponent<SnowmanAnimationControl>().victory = true;
            gold_snowman.transform.forward = new Vector3(31.3f, gold_snowman.transform.position.y, 47.3f) - gold_snowman.transform.position;
        } else {
            travelList[1] = null;
        }
        if (!gamecontrol.deadPlayers.Contains(2)) {
            travelList[2] = purple_player;
            aliveCount += 1;
            purple_player.GetComponent<AnimationControl>().victory = true;
            purple_player.transform.forward = new Vector3(31.3f, purple_player.transform.position.y, 47.3f) - purple_player.transform.position;
        } else {
            travelList[2] = null;
        }
        if (!gamecontrol.deadPlayers.Contains(3)) {
            travelList[3] = purple_snowman;
            aliveCount += 1;
            purple_snowman.GetComponent<SnowmanAnimationControl>().victory = true;
            purple_snowman.transform.forward = new Vector3(31.3f, purple_snowman.transform.position.y, 47.3f) - purple_snowman.transform.position;

        } else {
            travelList[3] = null;
        }
        Debug.Log(travelList);
        Debug.Log(aliveCount);
    }

    private void Update() {
        if (!finish) { 
            if (point < 4 && !travelList[point]) {
                point++;
                return;
            }

            if (point < 4) {
                float dist = Vector3.Distance(travelList[point].transform.position + travelList[point].transform.forward * 10f, transform.position);

                transform.position = Vector3.Lerp(transform.position, travelList[point].transform.position + travelList[point].transform.forward * 10f, Time.deltaTime * speed);

                Vector3 targetDir = -1f * travelList[point].transform.forward;

                float step = speed * 10f * Time.deltaTime;

                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);

                if (Vector3.Angle(targetDir, transform.forward) > 5f) {
                    transform.rotation = Quaternion.LookRotation(newDir);
                }

                if (dist <= reachDist) {
                    point++;
                }
            }

            if (point >= 4) {
                finish = true;
            }

        }

        if (finish && !ready) {
            /*
            float dist = Vector3.Distance(new Vector3(31.3f, 8.7f, 38.7f), transform.position);

            transform.position = Vector3.Lerp(transform.position, new Vector3(31.3f, 8.7f, 38.7f), Time.deltaTime * speed);

            float step = speed * 10f * Time.deltaTime;

            Vector3 newDirLast = Vector3.RotateTowards(transform.forward, Vector3.up, step, 0.0f);

            if (Vector3.Angle(Vector3.up, transform.forward) > 5f) {
                transform.rotation = Quaternion.LookRotation(newDirLast);
            }

            if (dist <= reachDist) {
                ready = true;
                
            }*/
            ready = true;
            StartCoroutine(FinalShot());
        }
    }

    IEnumerator FinalShot() {
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        gamecontrol.GetComponent<GameControl>().inGameOverCutScene = false;
    }
}
