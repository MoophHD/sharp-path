using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//handles saving
public class CurrentScore : MonoBehaviour {
    
    public Text Label;

    private float score;
    private float closeMultiplier = 1f;
    private float perDelta = 1;
    private float perClose = 2;
    private float perClose2 = 3;

    public void addDelta() {
        score+=perDelta;

        handleChange();
    }

    public void addClose() {
        score = score + perClose * closeMultiplier;
        
        handleChange();
    }

    public void addClose2() {
        handleChange();
    }

    private void handleChange() {
        Label.text = Mathf.Round(score).ToString();
    }

    public void Clear() {
        State.instance.highScore = (int) Mathf.Round(score);
        closeMultiplier = 1f;
        score = 0f;
        handleChange();
    }
}    