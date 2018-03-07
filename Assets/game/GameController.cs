using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public CameraController myCamera;
    public SpikeGenerator spikeGenerator;
    public GameObject overlay;
    public Player player;
    public CurrentScore currentScore;
    public GameObject secondChanceContainer;
    public SecondChance mySecondChance;

    private bool usedSecondChance = false;
    
    private float scoreDelta = 0.5f;

    public static int passedScreens = 3;
    int secondChanceScore = -1;
    //score per scoreDelta
    void addScore() {
        currentScore.addDelta();
    }
    void handlePause(bool isPaused) {
        overlay.SetActive(isPaused);
    }
    
    void handleScreenPass() {
        passedScreens++;
    }

    void Awake() {
        spikeGenerator.playerHeight = player.height;
        mySecondChance = secondChanceContainer.GetComponent<SecondChance>();
    }

    void restart() {
        //try 2nd chance
        State.instance.runs++;
        //play lose animation
        //smooth camera reset pos
        myCamera.reset();

        player.reset();
        spikeGenerator.reset();

        mySecondChance.cancelTimer();
        secondChanceContainer.SetActive(false);
    }

    void handleDeath() {


        CancelInvoke();

        myCamera.isMoving = false;
        player.freeze(true);

        if (usedSecondChance) {
            usedSecondChance = false;
            Invoke("restart", .25f);
        } else {
            Invoke("unfoldSecondChanceMenu", .25f);
        }
    }

    void unfoldSecondChanceMenu() {
        secondChanceContainer.SetActive(true);

        mySecondChance.setTimer();
    }

    void secondChance() {
        Debug.Log("adRewarded");
        
        secondChanceScore = currentScore.scoreGetter;

        usedSecondChance = true;

        player.centerSelf();

        Vector3 cameraPos = myCamera.pos;
        myCamera.pos = new Vector3(cameraPos.x, player.pos.y + 3.5f,cameraPos.z);
        spikeGenerator.resetInMiddle(myCamera.pos.y + 2.5f);
        secondChanceContainer.SetActive(false);
    }

    void start() {
        player.freeze(false);
        Debug.Log("secondChanceScore " + secondChanceScore);
        currentScore.Clear(secondChanceScore);     
        secondChanceScore = -1;
        CancelInvoke();
        InvokeRepeating("addScore", 0f, scoreDelta);

        spikeGenerator.onStart();
        myCamera.isMoving = true;
        player.firstJump();
    }

    void OnEnable() {
		GameActions.onPause += (bool paused) => { 
                handlePause(paused);
			};
        GameActions.onRestart += restart;
        GameActions.onStart += start;
        GameActions.onScreenPass += handleScreenPass;
        GameActions.onSecondChance += secondChance;
        GameActions.onDeath += handleDeath;
	}
    
	void OnDisable() {
		GameActions.onPause -= (bool paused) => { 
                handlePause(paused);
			};
        GameActions.onRestart -= restart;
        GameActions.onStart -= start;
        GameActions.onScreenPass -= handleScreenPass;
        GameActions.onSecondChance -= secondChance;
        GameActions.onDeath -= handleDeath;
	}
}