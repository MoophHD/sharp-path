using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public CameraController myCamera;
    public SpikeGenerator spikeGenerator;
    public GameObject overlay;
    public Player player;


    void handlePause(bool isPaused) {
        overlay.SetActive(isPaused);
    }

    void Awake() {
        spikeGenerator.playerHeight = player.height;
    }

    
    void restart() {
        //play lose animation
        //smooth camera reset pos
        myCamera.reset();
        myCamera.isMoving = false;

        player.reset();
        spikeGenerator.reset();
    }

    void start() {
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