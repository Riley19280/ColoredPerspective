using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class shop : MonoBehaviour {

	public GameObject coinsText;
	public GameObject jumpUpgradeBtn;
	public GameObject jumpUpgradeColor;

	int maxJumpLvl = 5;
	int cost = 50;

	// Use this for initialization
	void Start () {
		coinsText.GetComponent<Text>().text = "COINS: " + PlayerPrefs.GetInt(gameControl.PP_totalCoins);
		jumpUpgradeColor.GetComponent<Image>().fillAmount = .2f * PlayerPrefs.GetInt(gameControl.PP_jumpLevel);

		//check qualified to upgrade, otherwise grey out
		if (PlayerPrefs.GetInt(gameControl.PP_jumpLevel) == maxJumpLvl || PlayerPrefs.GetInt(gameControl.PP_totalCoins) < cost)
			jumpUpgradeBtn.GetComponent<Button>().interactable = false;

	}

	public void UpgradeJump() {
		gameControl.AudioMgr.GetComponent<AudioManager>().playSoundEffect(gameControl.AudioMgr.GetComponent<AudioManager>().click);
		if (PlayerPrefs.GetInt(gameControl.PP_jumpLevel) < maxJumpLvl && PlayerPrefs.GetInt(gameControl.PP_totalCoins)>= cost) {

			PlayerPrefs.SetInt(gameControl.PP_jumpLevel, PlayerPrefs.GetInt(gameControl.PP_jumpLevel) + 1);
			PlayerPrefs.Save();
			PlayerPrefs.SetInt(gameControl.PP_jumpStrength, (PlayerPrefs.GetInt(gameControl.PP_jumpLevel) * 25) + 300);
			PlayerPrefs.Save();
			jumpUpgradeColor.GetComponent<Image>().fillAmount = .2f * PlayerPrefs.GetInt(gameControl.PP_jumpLevel);

			PlayerPrefs.SetInt(gameControl.PP_totalCoins, PlayerPrefs.GetInt(gameControl.PP_totalCoins) - cost);
			coinsText.GetComponent<Text>().text = "COINS: " + PlayerPrefs.GetInt(gameControl.PP_totalCoins);
		}

	}

	public void Back() {
		SceneManager.LoadScene("menuScene");
	}


}
