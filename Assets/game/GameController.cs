using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public CameraController myCamera;
    public GameObject overlay;
    public Player player;
    public GameObject spike;
    public List<GameObject> spikes;

    void handlePause(bool isPaused) {
        overlay.SetActive(isPaused);
    }

    void gen() {

    }
    
    void restart() {
        //play animation
        //smooth camera reset pos
        myCamera.resetPos();
        player.reset();
        spikes.Clear();
    }
    void start() {
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