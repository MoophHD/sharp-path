using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SecondChance : MonoBehaviour {
    public Text timer;
    public Button adButton;
    public Button blur;
    private int time; 
    private AdManager adManager;

    private bool stopTimer = false;

    void Awake() {
        CancelInvoke();
        adManager = GameObject.FindWithTag("global").GetComponent<AdManager>();

        adButton.onClick.AddListener(() => { adManager.showRewardedAd(); cancelTimer();});
        blur.onClick.AddListener(handleBlur);
    }  

    void handleBlur() {
        if (!AdManager.instance.playingAd) {
            GameActions.restart(); 
        }
    }

    public void setTimer() { 
        CancelInvoke();
        time = 4;

        InvokeRepeating("tick", 0f, 1f);
    }

    public void cancelTimer() {
        CancelInvoke();
    }

    void tick() {
        if(time < 1 && !stopTimer) {
            GameActions.restart();
            CancelInvoke();
            time = 4;
        }

        time--;
        
        timer.text = time > 0 ? time.ToString() : "0";
    }
}