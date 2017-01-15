using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using System;

public class gameControl : MonoBehaviour
{
	public static int jumpStrength = 300;

	public static int coinCount = 0;
	public static int platforms;
	public static int maxPlatformCount = 25;
	
	public static string playerColor;

	public static int platformsTouched = 0;
	//used for delaying count of removing platforms

	public static GameObject AudioMgr;


	public static GameObject latestPlatform;
	public static GameObject lastPlatform;

	public static string PP_sound = "sound";
	public static string PP_music = "music";
	public static string PP_highscore = "highscore";
	public static string PP_totalCoins = "totalCoins";
	public static string PP_totalPlatforms = "totalplatforms";
	public static string PP_gameCount = "gamecount";
	public static string PP_TiltControls = "tiltcontrols";
	public static string PP_PlatformsBroken = "platformsBroken";


	public static string PP_jumpLevel = "jumpLevel";
	public static string PP_jumpStrength = "jumpstrength";


	public static string bannerID = "ca-app-pub-2351910116411316/9351459580";
	public static string InterstitialID = "ca-app-pub-2351910116411316/1828192780";
	public static bool ENABLEADS = true;

	void Start()
	{


	}

	void Update()
	{
		
	}

	public static void startPath()
	{
		latestPlatform = latestPlatform.GetComponent<platformRemove>().spawnPlatform();
	}

	public static void continuePath()
	{
		if (platformsTouched > maxPlatformCount / 2)
		{
			latestPlatform = latestPlatform.GetComponent<platformRemove>().spawnPlatform();
			GameObject temp = lastPlatform;
			lastPlatform = lastPlatform.GetComponent<platformRemove>().nextSpawnned;
			Destroy(temp);
		}
	}

	static BannerView bannerView;

	public static void RequestBanner()
	{
		if (ENABLEADS)
		{

			// Create a 320x50 banner at the top of the screen.
			bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);
			// Create an empty ad request.
			var request = new AdRequest.Builder().AddTestDevice("CD0D9F143549110573451D43B4DA9859");
			request.AddTestDevice("77B08A4FA88832D62AF6BBDB31038A05");
			var built = request.Build();
			bannerView.OnAdLoaded += HandleAdLoaded;
			// Load the banner with the request.
			bannerView.LoadAd(built);

		}
	}

	public static void HideBanner()
	{
		bannerView.Hide();
	}

	public static void ShowBanner()
	{
		if (ENABLEADS)
		{
			bannerView.Show();
		}
		}

	public static void DeleteBanner()
	{
		bannerView.Destroy();
	}

	public static void HandleAdLoaded(object sender, EventArgs args)
	{
		if (ENABLEADS)
		{
			ShowBanner();
		}
	}

	static InterstitialAd interstitial;

	public static void RequestInterstitial()
	{
		if (ENABLEADS)
		{
			// Initialize an InterstitialAd.
			interstitial = new InterstitialAd(InterstitialID);
			// Create an empty ad request.
			var request = new AdRequest.Builder().AddTestDevice("CD0D9F143549110573451D43B4DA9859");
			request.AddTestDevice("77B08A4FA88832D62AF6BBDB31038A05");
			var built = request.Build();

			request.Keywords.Add("game");
			// Load the interstitial with the request.

			interstitial.LoadAd(built);
			interstitial.OnAdClosed += interstitialAdClosed;
			interstitial.OnAdLoaded += HandleOnAdLoaded;
			interstitial.OnAdOpening += interstitialAdOpening;
			interstitial.OnAdLeavingApplication += interstitialLeaving;
		}
	}

	public static void ShowInterstitial()
	{
		if (interstitial.IsLoaded() && ENABLEADS)
		{
			interstitial.Show();
		
		}
	}

	public static void DeleteInterstitial()
	{
		interstitial.Destroy();
	}

	public static void HandleOnAdLoaded(object sender, EventArgs args)
	{
		Debug.Log("interstitial loaded");
	}



	public static void interstitialAdClosed(object sender, EventArgs args)
	{
		Time.timeScale = 1;//resume game
		RequestInterstitial();
	}

	public static void interstitialAdOpening(object sender, EventArgs args)
	{
		Time.timeScale = 0;
	}

	public static void interstitialLeaving(object sender, EventArgs args)
	{

		Time.timeScale = 1;
	}
}
