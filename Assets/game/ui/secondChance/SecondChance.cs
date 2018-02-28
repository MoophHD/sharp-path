using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SecondChance : MonoBehaviour {
    public AdManager ads;
    public Text timer;
    public Button adButton;
    public Button blur;
    private int time; 

    void Awake() {
        CancelInvoke();
        adButton.onClick.AddListener(() => { ads.showRewardedAd(); });
        blur.onClick.AddListener(() => { GameActions.restart(); });
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
        if(time < 1 && !ads.playingAd) {
            print("restart");
            GameActions.restart();
            CancelInvoke();
            time = 4;
        }

        time--;
        
        timer.text = time > 0 ? time.ToString() : "0";
    }
}