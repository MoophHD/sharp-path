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
    
    private float scoreDelta = 0.5f;

    public static int passedScreens = 3;
    //score per scoreDelta

    float temp = 0f;
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

        Invoke("unfoldSecondChanceMenu", .75f);
    }

    void unfoldSecondChanceMenu() {
        secondChanceContainer.SetActive(true);

        mySecondChance.setTimer();
    }

    void secondChance() {
        spikeGenerator.deleteLastSpikes();
        player.centerSelf();
        secondChanceContainer.SetActive(false);
    }

    void start() {
        player.freeze(false);
        
        currentScore.Clear();     
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