using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//handles saving
public class CurrentScore : MonoBehaviour {
    public Text Label;
    public Text streakLabel;
    private int score;
    private int perStreak = 15;
    private int streak = 0;
    private float streakCooldown = 0f;
    private const float STREAK_RESET = 0.5f; //in secs
    private int perDelta = 1;

    public TrailController trail;
    void LateUpdate() {
        streakCooldown += Time.deltaTime;

        if (streakCooldown>STREAK_RESET && streak != 0) resetStreak();
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
        Debug.Log("streak " + streak);
        score += streak * perStreak;
        trail.addStreak();
        trail.streak = streak;
        handleChange();
    }

    public void addDelta() {
        score += perDelta;

        handleChange();
    }

    public void Clear(int startScore) {
        score = startScore;
        
        resetStreak();
        handleChange();
    }

    private void resetStreak() {
        if (trail) trail.clearStreak();
        streak = 0;
        handleChange();
    }

    private void handleChange() {
        Label.text = score.ToString();
        streakLabel.text = "(+" + (Mathf.Max(streak, 0)) * perStreak + ")";
        if (State.instance) State.instance.highScore = score;
    }
    void OnEnable() {
        GameActions.onJump += handleJump;
    }
    void OnDisable() {
        GameActions.onJump -= handleJump;
    }

    public int scoreGetter {
        get {
            return score;
        }
    }
}    