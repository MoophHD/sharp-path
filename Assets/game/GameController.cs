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
        myCamera.resetPos();
        player.resetPos();
        spikes.Clear();
    }

    void OnEnable() {
		GameActions.onPause += (bool paused) => { 
                handlePause(paused);
			};
        GameActions.onCameraPass += gen;
        GameActions.onRestart += restart;
	}
    
	void OnDisable() {
		GameActions.onPause += (bool paused) => { 
                handlePause(paused);
			};
        GameActions.onCameraPass -= gen;
        GameActions.onRestart -= restart;
	}
}