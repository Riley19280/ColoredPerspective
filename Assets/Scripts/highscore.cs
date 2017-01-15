using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class highscore : MonoBehaviour
{

	public GameObject scoreText;
	public GameObject highscoreText;
	public GameObject coinText;

	public GameObject avergeDistText;
	public GameObject avergeCoinsText;


	void Start()
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			gameControl.ShowBanner();


		scoreText.GetComponent<Text>().text = "YOUR TOTAL DISTANCE WAS <b><color=green>" + gameControl.platformsTouched + "</color></b>";

		if (gameControl.platformsTouched > PlayerPrefs.GetInt(gameControl.PP_highscore))
		{
			PlayerPrefs.SetInt(gameControl.PP_highscore, gameControl.platformsTouched);
			highscoreText.GetComponent<Text>().text = "YOU MADE A NEW HIGHSCORE!";
		}

		//updating totals
		PlayerPrefs.SetInt(gameControl.PP_totalPlatforms, PlayerPrefs.GetInt(gameControl.PP_totalPlatforms) + gameControl.platformsTouched);
		PlayerPrefs.SetInt(gameControl.PP_totalCoins, PlayerPrefs.GetInt(gameControl.PP_totalCoins) + gameControl.coinCount);
		PlayerPrefs.SetInt(gameControl.PP_gameCount, PlayerPrefs.GetInt(gameControl.PP_gameCount) + 1);

		PlayerPrefs.Save();


		coinText.GetComponent<Text>().text = "YOU EARNED <b><color=yellow>" + gameControl.coinCount + "</color></b> COINS THIS GAME";
		avergeDistText.GetComponent<Text>().text = "Averge Distance: <b><color=blue>" + (PlayerPrefs.GetInt(gameControl.PP_totalPlatforms) / PlayerPrefs.GetInt(gameControl.PP_gameCount)) + "</color></b>";
		avergeCoinsText.GetComponent<Text>().text = "Averge Coins: <b><color=blue>" + (PlayerPrefs.GetInt(gameControl.PP_totalCoins) / PlayerPrefs.GetInt(gameControl.PP_gameCount)) + "</color></b>";

	}

	public void MainMenu()
	{
		gameControl.AudioMgr.GetComponent<AudioManager>().playSoundEffect(gameControl.AudioMgr.GetComponent<AudioManager>().click);
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			gameControl.ShowInterstitial();
		gameControl.platforms = 0;
		gameControl.platformsTouched = 0;
		gameControl.coinCount = 0;
		SceneManager.LoadScene("menuScene");

	}

	public void PlayAgain()
	{
		gameControl.AudioMgr.GetComponent<AudioManager>().playSoundEffect(gameControl.AudioMgr.GetComponent<AudioManager>().click);
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			gameControl.ShowInterstitial();
		gameControl.platforms = 0;
		gameControl.platformsTouched = 0;
		gameControl.coinCount = 0;
		SceneManager.LoadScene("level1");

	}
}
