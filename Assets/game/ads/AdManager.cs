using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour {
	const string appId = "ca-app-pub-1038804138558980~9877584517";

	const string bannerId = "ca-app-pub-1038804138558980/9228995055";
	const string testBannerId = "ca-app-pub-3940256099942544/6300978111";
	const string rewardId = "ca-app-pub-1038804138558980/9283522292";
	const string testRewardID = "ca-app-pub-3940256099942544/5224354917";

	private RewardBasedVideoAd rewardBasedVideo;

	private float lastTime = 0f;
	private float secondsBetweenAds = 15;
	private BannerView bannerView;
	public bool playingAd;

	
	void Start () {
		MobileAds.Initialize(appId);
		this.rewardBasedVideo = RewardBasedVideoAd.Instance;

		rewardBasedVideo.OnAdRewarded += (a, b) => { GameActions.secondChance(); playingAd = false;};
		rewardBasedVideo.OnAdFailedToLoad += (a, b) => { GameActions.restart(); playingAd = false;};

		RequestBanner();
		this.RequestRewardedVideo();
	}

	public void showRewardedAd() {
		if (rewardBasedVideo.IsLoaded()) {
			playingAd = true;
			rewardBasedVideo.Show();
			RequestRewardedVideo();
		}

	}

	public void RequestRewardedVideo() {
		AdRequest request = new AdRequest.Builder().Build();
        this.rewardBasedVideo.LoadAd(request, testRewardID);
	}

	private void RequestBanner() {
		bannerView = new BannerView(testBannerId, AdSize.Banner, 75, 0);
		AdRequest request = new AdRequest.Builder().Build();
		bannerView.LoadAd(request);
	}


	private void tryLoadAd() {
		if (Time.time - lastTime < secondsBetweenAds) return;

	}
	void OnEnable() {
		GameActions.onSecondChance += tryLoadAd;
		GameActions.onRestart += () => {playingAd = false;};
	}
	void OnDisable() {
		GameActions.onSecondChance -= tryLoadAd;
		GameActions.onRestart -= () => {playingAd = false;};
	}
}
