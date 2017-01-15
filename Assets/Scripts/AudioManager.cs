using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

	public AudioSource sourceS;
	public AudioSource sourceM;

	public AudioClip breaks;
	public AudioClip swoosh;
	public AudioClip coin;
	public AudioClip hit;
	public AudioClip click;
	public AudioClip song;

	void Awake()
	{
		if (gameControl.AudioMgr == null)
			gameControl.AudioMgr = gameObject;
		else
			Destroy(gameObject);
	}

	public void playMusic()
	{
		//changeSong ();

		sourceM.Play();
		updateMusicVol();
	}

	public void playSoundEffect(AudioClip clip)
	{
		sourceS.PlayOneShot(clip, PlayerPrefs.GetFloat(gameControl.PP_sound));
	}

	public void updateMusicVol()
	{
		sourceM.volume = PlayerPrefs.GetFloat(gameControl.PP_music);
	}

}
