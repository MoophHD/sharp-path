using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActions : MonoBehaviour {

	public delegate void pauseDel(bool paused);
    public static event pauseDel onPause;
    public static void pause(bool paused) {onPause(paused);}

    public delegate void cameraPassDel();
    public static event cameraPassDel onCameraPass;
    public static void cameraPass() {onCameraPass();}

    public delegate void restartDel();
    public static event restartDel onRestart;
    public static void restart() {onRestart();}
    
    public delegate void startDel();
    public static event startDel onStart;
    public static void start() {onStart();}

    //making sure the script is not duplicated
    private static GameActions gameActionsInstance;

	void Awake() {
        DontDestroyOnLoad (this);
        if (gameActionsInstance == null) {
            gameActionsInstance = this;
        } else {
            DestroyObject(gameObject);
        }
	}
}

