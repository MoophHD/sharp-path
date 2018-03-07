using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//handles saving
public class State : MonoBehaviour {
    private static State _instance;
	public static State instance {
        get {
            return _instance;
        }
    }
    public Text HighScoreLabel;

    private int _highScore;
    public int highScore {
        get {
            return _highScore;
        }
        set {
            if (value > _highScore) {
                //save score somewhere
            Debug.Log("prefs " + PlayerPrefs.GetInt("highScore"));
                _highScore = value;
            Debug.Log("hscore label " + HighScoreLabel);
                HighScoreLabel.text = value.ToString();
                PlayerPrefs.SetInt("highScore", value);
            }
        }
    }

    private int _runs;
    public int runs {
        get {
            return _runs;
        }
        set {
            _runs = value;
            PlayerPrefs.SetInt("runs", value);
        }
    }

    public bool isGameLoaded;

    void handleSceneLoad(Scene scene, LoadSceneMode mode) {
        if (scene.buildIndex == 0) {
            HighScoreLabel = GameObject.Find("HighScore").GetComponent<Text>();

            
            _highScore = PlayerPrefs.HasKey("highScore") ? PlayerPrefs.GetInt("highScore") : 0;
            _runs = PlayerPrefs.HasKey("runs") ? PlayerPrefs.GetInt("runs") : 0;
            
            HighScoreLabel.text = _highScore.ToString();

            isGameLoaded = true;
        }

    
    }

    void Start() {
        SceneManager.LoadScene(0);

        SceneManager.sceneLoaded += handleSceneLoad;

    }



    void Awake() {
        isGameLoaded = false;

        if (Application.isEditor)  {
            PlayerPrefs.DeleteAll();
        }

     if (_instance == null) {
            _instance = this;
        } 
	}
}