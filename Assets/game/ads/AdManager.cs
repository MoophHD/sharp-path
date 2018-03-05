using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour {
	const string appId = "ca-app-pub-1038804138558980~9877584517";
	// const string testAppId = "ca-app-pub-3940256099942544~3347511713";

	const string bannerId = "ca-app-pub-1038804138558980/9228995055";
	// const string testBannerId = "ca-app-pub-3940256099942544/6300978111";
	const string rewardId = "ca-app-pub-1038804138558980/9283522292";
	// const string testRewardID = "ca-app-pub-3940256099942544/5224354917";

	private RewardBasedVideoAd rewardBasedVideo;

	private BannerView bannerView;
	public bool playingAd;

	
	void Start () {
		MobileAds.Initialize(appId);
		this.rewardBasedVideo = RewardBasedVideoAd.Instance;
		rewardBasedVideo.OnAdRewarded += (a, b) => { GameActions.secondChance(); playingAd = false;};
		rewardBasedVideo.OnAdFailedToLoad += fail;
		rewardBasedVideo.OnAdClosed += fail;

		RequestBanner();
		this.RequestRewardedVideo();
	}

	private void fail(object sender, AdFailedToLoadEventArgs args) {
		GameActions.restart(); playingAd = false;
	}

	private void fail(object sender, EventArgs args) {
		GameActions.restart(); playingAd = false;
	}

	public void showRewardedAd() {
		if (rewardBasedVideo.IsLoaded()) {
			playingAd = true;
			rewardBasedVideo.Show();
			RequestRewardedVideo();
		}
	}

	public void RequestRewardedVideo() {
		GameActions.secondChance();
		
		AdRequest request = new AdRequest.Builder().Build();
        this.rewardBasedVideo.LoadAd(request, rewardId);
	}

	private void RequestBanner() {
		bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);
		AdRequest request = new AdRequest.Builder().Build();
		bannerView.LoadAd(request);
		bannerView.Show();
	}

	void OnEnable() {
		GameActions.onRestart += () => {playingAd = false;};
	}
	void OnDisable() {
		GameActions.onRestart -= () => {playingAd = false;};
	}
}
