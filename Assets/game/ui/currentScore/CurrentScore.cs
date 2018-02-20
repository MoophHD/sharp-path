using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//handles saving
public class CurrentScore : MonoBehaviour {
    public Text Label;
    private int score;
    private const float STREAK_RESET = 2.5f; //in secs
    private const int MAX_MULTIPLIER = 8; //4 6 8 10 12 14 16 20
    private int multiplier = 1;
    private int perDelta = 1;
    private int perClose = 2;

    public void addDelta() {
        score+=perDelta;

        handleChange();
    }

    public void tryAddClose() {
        if (GameController.closeAreas == 0) return;
        //reset pow reset timer
        CancelInvoke();
        score = score + perClose + multiplier * 2;
        
        multiplier = Mathf.Min(multiplier + 1, MAX_MULTIPLIER);

        Invoke("multiplierReset", STREAK_RESET);
        handleChange();
    }
    
    void OnEnable() {
        GameActions.onJump += tryAddClose;
    }

    void OnDisable() {
        GameActions.onJump -= tryAddClose;
    }
    
    public void Clear() {
        multiplierReset();
        State.instance.highScore = score;
        score = 0;
        handleChange();
    }

    private void handleChange() {
        Label.text =  score.ToString();
    }

    void multiplierReset() {
        multiplier = 1;
    }
}    