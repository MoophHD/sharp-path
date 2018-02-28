using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//handles saving
public class CurrentScore : MonoBehaviour {
    public Text Label;
    public Text streakLabel;
    private int score;
    private float lastJumpTm = -1f;
    
    private int perStreak = 15;
    private int streak = 0;
    private float streakCooldown = 0f;
    private const float STREAK_RESET = 0.5f; //in secs
    private const int MAX_MULTIPLIER = 8; //4 6 8 10 12 14 16 20
    private int perDelta = 1;


    
    void LateUpdate() {
        streakCooldown += Time.deltaTime;

        if (streakCooldown>STREAK_RESET) resetStreak();
    }
    void handleJump() {
        if (streakCooldown > STREAK_RESET) {
            resetStreak();
        } else {
            //play animation
            addStreak();
        }

        streakCooldown = 0f;
    }
    void addStreak() {
        streak++;
        score += streak * perStreak;

        handleChange();
    }

    public void addDelta() {
        score += perDelta;

        handleChange();
    }

    public void Clear() {
        score = 0;
        
        resetStreak();
        handleChange();
    }

    private void resetStreak() {
        streak = 0;
        handleChange();
    }

    private void handleChange() {
        Label.text = score.ToString();
        streakLabel.text = "(+" + (Mathf.Max(streak, 0)) * perStreak + ")";
        State.instance.highScore = score;
    }
    void OnEnable() {
        GameActions.onJump += handleJump;
    }

    void OnDisable() {
        GameActions.onJump -= handleJump;
    }
}    