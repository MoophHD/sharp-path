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
    public static int closeAreas = 0;
    
    private float scoreDelta = 0.5f;
    //score per scoreDelta

    float temp = 0f;
    void addScore() {
        print(temp += Time.deltaTime);
        currentScore.addDelta();
    }
    void handlePause(bool isPaused) {
        overlay.SetActive(isPaused);
    }

    void Awake() {
        spikeGenerator.playerHeight = player.height;
    }

    void restart() {
        print("restart");
        CancelInvoke();
        closeAreas = 0;
        //play lose animation
        //smooth camera reset pos
        myCamera.reset();
        myCamera.isMoving = false;

        player.reset();
        spikeGenerator.reset();
    }

    void start() {
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
	}
    
	void OnDisable() {
		GameActions.onPause += (bool paused) => { 
                handlePause(paused);
			};
        GameActions.onRestart -= restart;
        GameActions.onStart -= start;
	}
}