using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	private static GameManager _instance;
	public static GameManager instance {
        get {
			print("instance");
            if (_instance == null) {
				//finds/creates container for all the global stuff 
                GameObject go = GameObject.Find("global");
                if (go == null)
                    go = new GameObject("global");
                go.AddComponent<GameManager>();
                
            }

            return _instance;
        }
	}

	private GameStates currentState;

	public GameStates CurrentState {
		set {
			if(currentState!=value) {
				currentState = value;

				switch(value) {
					case GameStates.Game:
						break;
					case GameStates.Shop:
						break;
					default:
						break;
				}
			}
		}

	}

	public enum GameStates {
		Game,
		Shop
	}

	void OnEnable() {
		GameActions.onPause += (bool paused) => { 
				if(paused) {
					Time.timeScale = 0;
				} else {
					Time.timeScale = 1;
				}
			};
	}
	
	void OnDisable() {
		GameActions.onPause -= (bool paused) => { 
			if(paused) {
				Time.timeScale = 0;
			} else {
				Time.timeScale = 1;
			}
		};
	}

	void Awake() {
        DontDestroyOnLoad(transform.gameObject);

		if (_instance == null)
			_instance = this;
		else
			Destroy(gameObject);
	}
}
