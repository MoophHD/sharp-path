using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//handles saving
public class CurrentScore : MonoBehaviour {
    public Text Label;
    private int score;
    private const float STREAK_RESET = 2.5f; //in secs
    private const int MAX_POW = 4; //16
    private int closePow = 1;
    private int perDelta = 1;
    private int perClose = 2;

    public void addDelta() {
        score+=perDelta;

        handleChange();
    }

    public void tryAddClose() {
        print("try add  " + GameController.closeAreas);
        if (GameController.closeAreas == 0) return;
        //reset pow reset timer
        CancelInvoke();
        score = score + (int)Mathf.Pow(perClose, closePow);
        
        closePow = Mathf.Min(closePow + 1, MAX_POW);

        Invoke("powReset", STREAK_RESET);
        handleChange();
    }
    
    void powReset() {
        closePow = 1;
    }

    void OnEnable() {
        GameActions.onJump += tryAddClose;
    }

    void OnDisable() {
        GameActions.onJump -= tryAddClose;
    }
    
    public void Clear() {
        powReset();
        State.instance.highScore = score;
        score = 0;
        handleChange();
        
    }

    private void handleChange() {
        Label.text =  score.ToString();
    }
}    