using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActions : MonoBehaviour {
	public delegate void pauseDel(bool paused);
    public static event pauseDel onPause;
    public static void pause(bool paused) {onPause(paused);}

	void Awake() {
        DontDestroyOnLoad(transform.gameObject);
	}
}

