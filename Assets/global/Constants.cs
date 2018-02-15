using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour {
	private static Constants _instance;
	public static Constants instance {
        get {
			print("instance");
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

	public float cameraSpeed = 12.5f;
	public float cameraPassDistance = 5f;

	void Awake() {
		if (_instance == null)
			_instance = this;
		else
			Destroy(gameObject);
	}
}
