using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour {
	private static Constants _instance;
	public static Constants instance {
        get {
            if (_instance == null) {
				//finds/creates container for all the global stuff 
                GameObject go = GameObject.Find("global");
                if (go == null)
                    go = new GameObject("global");
                go.AddComponent<Constants>();
                
            }

            return _instance;
        }
	}

	private Vector2 minCameraBounds;
	private Vector2 maxCameraBounds;


	public float cameraSpeed = 8f;
	public float spikeSideOffset = 2.7025f;
	public float screenHeight;
	public float screenWidth;

	public int maxStreak = 6;

	void Awake() {
		minCameraBounds = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
		maxCameraBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
		screenHeight = maxCameraBounds.y - minCameraBounds.y;
		screenWidth = maxCameraBounds.x - minCameraBounds.x;
		
		if (_instance == null)
			_instance = this;
		else
			Destroy(gameObject);
	}
}
