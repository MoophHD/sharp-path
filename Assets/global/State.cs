using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                _highScore = value;
                HighScoreLabel.text = value.ToString();
                PlayerPrefs.SetInt("highScore", value);
            }
        }
    }

    void Awake() {
        //grab from savings
        _highScore = PlayerPrefs.HasKey("highScore") ? PlayerPrefs.GetInt("highScore") : 0;

        HighScoreLabel.text = _highScore.ToString();
        
        DontDestroyOnLoad (this);
        if (_instance == null) {
            _instance = this;
        } else {
            DestroyObject(gameObject);
        }
	}
}