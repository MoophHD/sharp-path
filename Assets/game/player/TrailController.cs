using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrailController : MonoBehaviour {
    int maxStreak;
    float startWidth = 0.05f;
    float widthPerStreak = 0.005f;

    Color startColor = new Color(0f, 0f, 49/255f);
    Color finColor = new Color(1f, 100/255f, 100/255f);
    public int streak = 0;

    TrailRenderer trail;

    void Awake() {
        trail = GetComponent<TrailRenderer>();
        maxStreak = Constants.instance.maxStreak;
    }

    public void addStreak() {
        trail.widthMultiplier = trail.widthMultiplier + widthPerStreak;
        // Color newColor = mixColors(startColor, finColor, streak/maxStreak); 
        trail.colorGradient = buildGradient(finColor);
    }

    public void clearStreak() {
        trail.widthMultiplier = startWidth;
        trail.colorGradient = buildGradient(startColor);
    }

    Gradient buildGradient(Color cl) {
        Gradient gradient = new Gradient();
        GradientColorKey[] clKey = new GradientColorKey[2];
        GradientAlphaKey[] alKey = new GradientAlphaKey[2];;
        clKey[0].color = cl;
        clKey[0].time = 0.0F;
        clKey[1].color = cl;
        clKey[1].time = 1.0F;
        alKey[0].alpha = 1.0F;
        alKey[0].time = 0.0F;
        alKey[1].alpha = 1.0F;
        alKey[1].time = 1.0F;
        gradient.SetKeys(clKey, alKey);
        return gradient;
    }

    // Color mixColors(Color cl1, Color cl2, float interpolation) {
    //     return new Color(
    //         slerpFloat(cl1[0], cl2[0], interpolation),
    //         slerpFloat(cl1[1], cl2[2], interpolation),
    //         slerpFloat(cl1[1], cl2[2], interpolation));
    // }

    // float slerpFloat(float a, float b, float interpolation) {
    //     return a * (1 - interpolation) + b * interpolation;
    // }
}