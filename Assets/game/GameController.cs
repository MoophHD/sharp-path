using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public CameraController myCamera;
    public GameObject overlay;
    public Player player;
    public GameObject spike;
    public List<GameObject> spikes;

    //gen stuff

    private float playerHeight = 0.5f;
    private float jumpHeight;


    void handlePause(bool isPaused) {
        overlay.SetActive(isPaused);
    }

    void Awake() {
        playerHeight = player.height;
        jumpHeight = player.jumpHeight;
    }

    void gen() {

    }
    
    void restart() {
        //play lose animation
        //smooth camera reset pos
        myCamera.reset();
        myCamera.isMoving = false;

        player.reset();
        spikes.Clear();
    }

    void start() {
        myCamera.isMoving = true;
        player.firstJump();
    }

    void OnEnable() {
		GameActions.onPause += (bool paused) => { 
                handlePause(paused);
			};
        GameActions.onCameraPass += gen;
        GameActions.onRestart += restart;
        GameActions.onStart += start;
	}
    
	void OnDisable() {
		GameActions.onPause += (bool paused) => { 
                handlePause(paused);
			};
        GameActions.onCameraPass -= gen;
        GameActions.onRestart -= restart;
        GameActions.onStart -= start;
	}
}