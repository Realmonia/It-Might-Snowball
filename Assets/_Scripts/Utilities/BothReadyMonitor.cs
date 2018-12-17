using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;


public class BothReadyMonitor : MonoBehaviour {

    public GameObject tut1;
    public GameObject tut2;

    public GameControl game;
	public GameObject introductionCamera;

    public GameObject TutorialGoal1;
    public GameObject TutorialCheckbox1;
    public GameObject TutorialGoal2;
    public GameObject TutorialCheckbox2;
    public GameObject TutorialGoal3;
    public GameObject TutorialCheckbox3;
    public GameObject TutorialGoal4;
    public GameObject TutorialCheckbox4;
    public GameObject TutorialGoal5;
    public GameObject TutorialCheckbox5;
    public GameObject TutorialGoal6;
    public GameObject TutorialCheckbox6;
    public GameObject TutorialGoal7;
    public GameObject TutorialCheckbox7;
    public GameObject TutorialGoal8;
    public GameObject TutorialCheckbox8;
    public GameObject TutorialGoal9;
    public GameObject TutorialCheckbox9;
    public GameObject TutorialGoal10;
    public GameObject TutorialCheckbox10;

    public GameObject GameGoal1;
    public GameObject GameGoalCheckbox1;
    public GameObject GameGoal2;
    public GameObject GameGoalCheckbox2;
    public GameObject GameGoal3;
    public GameObject GameGoalCheckbox3;
    public GameObject GameGoal4;
    public GameObject GameGoalCheckbox4;

    public GameObject bannerToAdjust1;
    public GameObject bannerToAdjust2;

	public GameObject panelForText1;
	public GameObject panelForText2;
	public GameObject panelForText3;
	public GameObject panelForText4;

    private BoxCollider startWallGold;
    private BoxCollider startWallPurple;

    GameObject PurpleIceWall;
	GameObject GoldIceWall;
	List<Animator> iceWallAnimators;


    private bool OK = false;
    public bool[] ready;
    public bool allready;
	public bool inIntroduction = false;

    private void Start() {
        ready = new bool[4];
        ready[0] = false;
        ready[1] = false;
        ready[2] = false;
        ready[3] = false;
		PurpleIceWall = GameObject.Find ("PurpleIceWall");
		GoldIceWall = GameObject.Find ("GoldIceWall");
		Animator[] temp = PurpleIceWall.GetComponentsInChildren<Animator> ();
		Animator[] temp2 = GoldIceWall.GetComponentsInChildren<Animator> ();
		iceWallAnimators = new List<Animator>();
		foreach (Animator anim in temp){
			iceWallAnimators.Add (anim);
		}
		foreach(Animator anim in temp2){
			iceWallAnimators.Add (anim);
		}
		changeWallAnimationSpeed (0f);
        startWallGold = GameObject.Find("TutorialGold").GetComponentInChildren<BoxCollider>();
        startWallPurple = GameObject.Find("TutorialPurple").GetComponentInChildren<BoxCollider>();
    }

    private void Update() {
        if (!OK && tut1.GetComponent<TutorialEventChecker>().finished && tut2.GetComponent<TutorialEventChecker>().finished) {
            StartCoroutine(Startgame());
            OK = true;
        }
        if (OK) {
            if (InputManager.Devices.Count > 0 && InputManager.Devices[0].Action1.IsPressed) {
                ready[0] = true;
            }
            if (ready[0])
                tut1.GetComponent<TutorialEventChecker>().playerWelcome.GetComponent<Text>().text = "Wait for other players";
            if (InputManager.Devices.Count > 1 && InputManager.Devices[1].Action1.IsPressed) {
                ready[1] = true;
            }
            if (ready[1])
                tut1.GetComponent<TutorialEventChecker>().snowmanWelcome.GetComponent<Text>().text = "Wait for other players";
            if (InputManager.Devices.Count > 2 && InputManager.Devices[2].Action1.IsPressed) {
                ready[2] = true;
            }
            if (ready[2])
                tut2.GetComponent<TutorialEventChecker>().playerWelcome.GetComponent<Text>().text = "Wait for other players";
            if (InputManager.Devices.Count > 3 && InputManager.Devices[3].Action1.IsPressed) {
                ready[3] = true;
            }
            if (ready[3])
                tut2.GetComponent<TutorialEventChecker>().snowmanWelcome.GetComponent<Text>().text = "Wait for other players";
        }
        allready = ready[0] && ready[1] && ready[2] && ready[3];
    }

    IEnumerator Startgame() {
        //make sure the walls are there, something in another file(i cannot find) disabled its colliders
        startWallGold.enabled = true;
        startWallPurple.enabled = true;
        tut1.GetComponent<TutorialEventChecker>().playerWelcome.GetComponent<TextBlink>().DestroyString();
        tut1.GetComponent<TutorialEventChecker>().snowmanWelcome.GetComponent<TextBlink>().DestroyString();
        tut2.GetComponent<TutorialEventChecker>().playerWelcome.GetComponent<TextBlink>().DestroyString();
        tut2.GetComponent<TutorialEventChecker>().snowmanWelcome.GetComponent<TextBlink>().DestroyString();
		panelForText1.SetActive(true);
		panelForText2.SetActive(true);
		panelForText3.SetActive(true);
		panelForText4.SetActive(true);

        tut1.GetComponent<TutorialEventChecker>().playerWelcome.GetComponent<Text>().text = "Everyone is ready to go. Press [A] to cofirm.";
        tut1.GetComponent<TutorialEventChecker>().snowmanWelcome.GetComponent<Text>().text = "Everyone is ready to go. Press [A] to cofirm.";
        tut2.GetComponent<TutorialEventChecker>().playerWelcome.GetComponent<Text>().text = "Everyone is ready to go. Press [A] to cofirm.";
        tut2.GetComponent<TutorialEventChecker>().snowmanWelcome.GetComponent<Text>().text = "Everyone is ready to go. Press [A] to cofirm.";
        while (!allready) {
            tut1.GetComponent<TutorialEventChecker>().playerWelcome.GetComponent<Text>().enabled = 
                !tut1.GetComponent<TutorialEventChecker>().playerWelcome.GetComponent<Text>().enabled;
            tut1.GetComponent<TutorialEventChecker>().snowmanWelcome.GetComponent<Text>().enabled =
                !tut1.GetComponent<TutorialEventChecker>().snowmanWelcome.GetComponent<Text>().enabled;
            tut2.GetComponent<TutorialEventChecker>().playerWelcome.GetComponent<Text>().enabled =
                !tut2.GetComponent<TutorialEventChecker>().playerWelcome.GetComponent<Text>().enabled;
            tut2.GetComponent<TutorialEventChecker>().snowmanWelcome.GetComponent<Text>().enabled =
                !tut2.GetComponent<TutorialEventChecker>().snowmanWelcome.GetComponent<Text>().enabled;
            yield return new WaitForSeconds(0.5f);
        }

        //remove the walls and the extra snow piles
        GameObject p1p = tut1.transform.parent.gameObject;
        GameObject p2p = tut2.transform.parent.gameObject;
        //i changed starting index to 1 because i don't wanna have startwallgold/purple disabled at this point
        for (int i = 1; i < p1p.transform.childCount; ++i)
        {
            p1p.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 1; i < p2p.transform.childCount; ++i)
        {
            p2p.transform.GetChild(i).gameObject.SetActive(false);
        }


        //disable wait for other players message 
        tut1.GetComponent<TutorialEventChecker>().playerWelcome.GetComponent<Text>().enabled = false;
        tut1.GetComponent<TutorialEventChecker>().snowmanWelcome.GetComponent<Text>().enabled = false;
        tut2.GetComponent<TutorialEventChecker>().playerWelcome.GetComponent<Text>().enabled = false;
        tut2.GetComponent<TutorialEventChecker>().snowmanWelcome.GetComponent<Text>().enabled = false;

        tut1.GetComponent<TutorialEventChecker>().dummy.SetActive(false);
        tut2.GetComponent<TutorialEventChecker>().dummy.SetActive(false);

        if (tut1.GetComponent<TutorialEventChecker>().player.GetComponent<Health>().health < 3)
        {
            tut1.GetComponent<TutorialEventChecker>().player.GetComponent<Health>().addHealth(3);
        }
        if (tut1.GetComponent<TutorialEventChecker>().snowman.GetComponent<Health>().health < 6)
        {
            tut1.GetComponent<TutorialEventChecker>().snowman.GetComponent<Health>().addHealth(6);
        }
        if (tut1.GetComponent<TutorialEventChecker>().player.GetComponent<Shooting>().snowAmount < 4)
        {
            tut1.GetComponent<TutorialEventChecker>().player.GetComponent<Shooting>().addSnowBall(4);
        }
        if (tut2.GetComponent<TutorialEventChecker>().player.GetComponent<Health>().health < 3)
        {
            tut2.GetComponent<TutorialEventChecker>().player.GetComponent<Health>().addHealth(3);
        }
        if (tut2.GetComponent<TutorialEventChecker>().snowman.GetComponent<Health>().health < 6)
        {
            tut2.GetComponent<TutorialEventChecker>().snowman.GetComponent<Health>().addHealth(6);
        }
        if (tut2.GetComponent<TutorialEventChecker>().player.GetComponent<Shooting>().snowAmount < 4)
        {
            tut2.GetComponent<TutorialEventChecker>().player.GetComponent<Shooting>().addSnowBall(4);
        }

        
        TutorialGoal1.transform.parent.gameObject.SetActive(false);
        TutorialGoal3.transform.parent.gameObject.SetActive(false);
        TutorialGoal5.transform.parent.gameObject.SetActive(false);
        TutorialGoal7.transform.parent.gameObject.SetActive(false);


        game.inTutorial = false;

        // Introduction camera part
        inIntroduction = true;
        // Turn off global UI
        GameObject mainCanvas = GameObject.Find("Canvas");
        mainCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        Camera temp = mainCanvas.GetComponent<Canvas>().worldCamera;
        mainCanvas.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        // Turn on Introduction camera
        introductionCamera.SetActive(true);
        while (inIntroduction) {
            yield return null;
        }
        mainCanvas.GetComponent<Canvas>().worldCamera = temp;
        mainCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        introductionCamera.SetActive(false);

		panelForText1.SetActive(false);
		panelForText2.SetActive(false);
		panelForText3.SetActive(false);
		panelForText4.SetActive(false);
        
		bannerToAdjust1.GetComponent<RectTransform>().sizeDelta = new Vector2(480, 80);
        bannerToAdjust2.GetComponent<RectTransform>().sizeDelta = new Vector2(480, 80);

        gameObject.GetComponent<Text>().text = "Game Start In 3";
        yield return new WaitForSeconds(0.7f);
        gameObject.GetComponent<Text>().text = "";
		changeWallAnimationSpeed (0.5f);

        yield return new WaitForSeconds(0.3f);
		changeWallAnimationSpeed (0);

        gameObject.GetComponent<Text>().text = "2";
        for (int i =0; i< 14; ++i) {
            gameObject.GetComponent<Text>().fontSize += 10;
            yield return new WaitForSeconds(0.05f);
        }


        gameObject.GetComponent<Text>().fontSize -= 140;
        gameObject.GetComponent<Text>().text = "";
		changeWallAnimationSpeed (0.5f);

        yield return new WaitForSeconds(0.3f);
		changeWallAnimationSpeed (0);
        gameObject.GetComponent<Text>().text = "1";
        for (int i = 0; i < 14; ++i) {
            gameObject.GetComponent<Text>().fontSize += 10;
            yield return new WaitForSeconds(0.05f);
        }
		changeWallAnimationSpeed (1f);

        gameObject.GetComponent<Text>().fontSize -= 140;
        gameObject.GetComponent<Text>().text = "";
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<Text>().text = "Go!";

        //after "GO" text appear, destroy the wall
        startWallGold.enabled = false;
        startWallPurple.enabled = false;

        tut1.SetActive(false);
        tut2.SetActive(false);


        for (int i = 0; i < 14; ++i) {
            gameObject.GetComponent<Text>().fontSize += 10;
            yield return new WaitForSeconds(0.05f);
        }
        gameObject.GetComponent<Text>().fontSize -= 140;
        gameObject.GetComponent<Text>().text = "";
        yield return new WaitForSeconds(0.3f);

        TutorialGoal1.SetActive(false);
        TutorialCheckbox1.SetActive(false);
        TutorialGoal2.SetActive(false);
        TutorialCheckbox2.SetActive(false);
        TutorialGoal3.SetActive(false);
        TutorialCheckbox3.SetActive(false);
        TutorialGoal4.SetActive(false);
        TutorialCheckbox4.SetActive(false);
        TutorialGoal5.SetActive(false);
        TutorialCheckbox5.SetActive(false);
        TutorialGoal6.SetActive(false);
        TutorialCheckbox6.SetActive(false);
        TutorialGoal7.SetActive(false);
        TutorialCheckbox7.SetActive(false);
        TutorialGoal8.SetActive(false);
        TutorialCheckbox8.SetActive(false);
        TutorialGoal9.SetActive(false);
        TutorialCheckbox9.SetActive(false);
        TutorialGoal10.SetActive(false);
        TutorialCheckbox10.SetActive(false);

        GameGoal1.SetActive(true);
        GameGoalCheckbox1.SetActive(true);
        GameGoal2.SetActive(true);
        GameGoalCheckbox2.SetActive(true);
        GameGoal3.SetActive(true);
        GameGoalCheckbox3.SetActive(true);
        GameGoal4.SetActive(true);
        GameGoalCheckbox4.SetActive(true);

        
        TutorialGoal1.transform.parent.gameObject.SetActive(true);
        TutorialGoal3.transform.parent.gameObject.SetActive(true);
        TutorialGoal5.transform.parent.gameObject.SetActive(true);
        TutorialGoal7.transform.parent.gameObject.SetActive(true);
        

        yield break;
    }

	void changeWallAnimationSpeed(float speed){
		for (int i = 0; i < iceWallAnimators.Count; i++){
			iceWallAnimators [i].speed = speed;
		}
	}
}
