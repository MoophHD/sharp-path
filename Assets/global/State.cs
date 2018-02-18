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
    public Text Label;

    private int _highScore;
    public int highScore {
        get {
            return _highScore;
        }
        set {
            if (value > _highScore) {
                //save score somewhere
                _highScore = value;
                Label.text = value.ToString();
            }
        }
    }

    void Awake() {
        //grab from savings
        _highScore = 0;
        Label.text = _highScore.ToString();
        
        DontDestroyOnLoad (this);
        if (_instance == null) {
            _instance = this;
        } else {
            DestroyObject(gameObject);
        }
	}
}