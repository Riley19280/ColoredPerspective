using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
	public GameObject optionsMenu;
	public GameObject tiltCheck;
	public GameObject volumeSlider;
	public GameObject musicSlider;

	void Awake() {
		DontDestroyOnLoad(GameObject.Find("AUDIO"));
	}

	// Use this for initialization
	void Start()
	{
		//PlayerPrefs.DeleteAll();
		//PlayerPrefs.Save();
		//firsttime
		if (!PlayerPrefs.HasKey("firsttime"))
		{
			PlayerPrefs.SetInt("firsttime", 1);
			PlayerPrefs.SetInt(gameControl.PP_jumpLevel, 1);
			PlayerPrefs.SetInt(gameControl.PP_jumpStrength, 300);//set init jump strength
			PlayerPrefs.SetFloat(gameControl.PP_sound, 1f);
			PlayerPrefs.SetFloat(gameControl.PP_music, 1f);
			PlayerPrefs.SetInt(gameControl.PP_highscore, 0);
			PlayerPrefs.SetInt(gameControl.PP_totalCoins, 0);
			PlayerPrefs.SetInt(gameControl.PP_totalPlatforms, 0);
			PlayerPrefs.SetInt(gameControl.PP_gameCount, 0);
			PlayerPrefs.SetInt(gameControl.PP_TiltControls, 1);
			PlayerPrefs.SetInt(gameControl.PP_PlatformsBroken, 0);		

			PlayerPrefs.Save();
		}

		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			gameControl.RequestInterstitial();
			gameControl.RequestBanner();
			gameControl.ShowBanner();
		}

		if (PlayerPrefs.GetInt(gameControl.PP_TiltControls) == 1)
		{
			tiltCheck.GetComponent<Toggle>().isOn = true;
		} else
		{
			tiltCheck.GetComponent<Toggle>().isOn = false;
		}

		volumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat(gameControl.PP_sound);
		musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat(gameControl.PP_music);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void play()
	{
		gameControl.coinCount = 0;
		gameControl.lastPlatform = null;
		gameControl.latestPlatform = null;
		gameControl.platformsTouched = 0;
		gameControl.AudioMgr.GetComponent<AudioManager>().playSoundEffect(gameControl.AudioMgr.GetComponent<AudioManager>().click);
		SceneManager.LoadScene("level1");


	}

	public void options()
	{
		gameControl.AudioMgr.GetComponent<AudioManager>().playSoundEffect(gameControl.AudioMgr.GetComponent<AudioManager>().click);
		optionsMenu.SetActive(!optionsMenu.activeSelf);
	}

	public void Exit()
	{
		Application.Quit();
		gameControl.AudioMgr.GetComponent<AudioManager>().playSoundEffect(gameControl.AudioMgr.GetComponent<AudioManager>().click);
	}

	public void shop()
	{
		gameControl.AudioMgr.GetComponent<AudioManager>().playSoundEffect(gameControl.AudioMgr.GetComponent<AudioManager>().click);
		SceneManager.LoadScene("shop");
	}

	public void OnValueChanged(bool value)
	{
		if (tiltCheck.GetComponent<Toggle>().isOn)
		{
			PlayerPrefs.SetInt(gameControl.PP_TiltControls, 1);
		} else
		{
			PlayerPrefs.SetInt(gameControl.PP_TiltControls, 0);
		}
		PlayerPrefs.Save();
	}

	public void VolumeChanged(float f)
	{
		PlayerPrefs.SetFloat(gameControl.PP_sound, f);
		PlayerPrefs.Save();
	}

	public void MusicVolumeChanged(float f)
	{
		PlayerPrefs.SetFloat(gameControl.PP_music, f);
		PlayerPrefs.Save();
		gameControl.AudioMgr.GetComponent<AudioManager>().updateMusicVol();
	}

}
