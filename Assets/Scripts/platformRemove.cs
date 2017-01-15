using UnityEngine;
using System.Collections;

public class platformRemove : MonoBehaviour
{
	public Material[] material = new Material[3];
	bool isTouching = false;
	public Transform playSound;
	public GameObject platformPrefab;
	public Transform coinSpawn;
	public AnimationCurve maxPos;
	public GameObject nextSpawnned;
	bool nextGenerated = false;

	void Start()
	{
		gameObject.tag = "platform";
		int defineColor = Random.Range(0, 3);

		if (gameObject.tag == "platform")
		{
			switch (defineColor)
			{
				case 0:
					gameObject.tag = "platformR";
					GetComponent<Renderer>().material = material[0];
					break;
				case 1:
					gameObject.tag = "platformG";
					GetComponent<Renderer>().material = material[1];
					Instantiate(coinSpawn, transform.position + new Vector3(0, 1, 0), transform.rotation);
					break;
				case 2:
					gameObject.tag = "platformB";
					GetComponent<Renderer>().material = material[2];
					break;
			}
		}


	}

	void Update()
	{

		if (isTouching == true)
		{
			if (gameObject.tag == "platformR" && gameControl.playerColor != "red")
			{
				gameControl.AudioMgr.GetComponent<AudioManager>().playSoundEffect(gameControl.AudioMgr.GetComponent<AudioManager>().breaks);
				PlayerPrefs.SetInt(gameControl.PP_PlatformsBroken, PlayerPrefs.GetInt(gameControl.PP_PlatformsBroken) + 1);
				PlayerPrefs.Save();
				Destroy(gameObject);
			}
			if (gameObject.tag == "platformG" && gameControl.playerColor != "green")
			{
				gameControl.AudioMgr.GetComponent<AudioManager>().playSoundEffect(gameControl.AudioMgr.GetComponent<AudioManager>().breaks);
				PlayerPrefs.SetInt(gameControl.PP_PlatformsBroken, PlayerPrefs.GetInt(gameControl.PP_PlatformsBroken) + 1);
				PlayerPrefs.Save();
				Destroy(gameObject);
			}
			if (gameObject.tag == "platformB" && gameControl.playerColor != "blue")
			{
				gameControl.AudioMgr.GetComponent<AudioManager>().playSoundEffect(gameControl.AudioMgr.GetComponent<AudioManager>().breaks);
				PlayerPrefs.SetInt(gameControl.PP_PlatformsBroken, PlayerPrefs.GetInt(gameControl.PP_PlatformsBroken) + 1);
				PlayerPrefs.Save();
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			isTouching = true;

			if (!nextGenerated)
			{
				gameControl.continuePath();
				gameControl.platformsTouched++;
				nextGenerated = true;
			}


			if (gameObject.tag == "platformR" && gameControl.playerColor != "red")
			{
				gameControl.AudioMgr.GetComponent<AudioManager>().playSoundEffect(gameControl.AudioMgr.GetComponent<AudioManager>().breaks);
				PlayerPrefs.SetInt(gameControl.PP_PlatformsBroken, PlayerPrefs.GetInt(gameControl.PP_PlatformsBroken) + 1);
				PlayerPrefs.Save();
				Destroy(gameObject);
			}
			if (gameObject.tag == "platformG" && gameControl.playerColor != "green")
			{
				gameControl.AudioMgr.GetComponent<AudioManager>().playSoundEffect(gameControl.AudioMgr.GetComponent<AudioManager>().breaks);
				PlayerPrefs.SetInt(gameControl.PP_PlatformsBroken, PlayerPrefs.GetInt(gameControl.PP_PlatformsBroken) + 1);
				PlayerPrefs.Save();
				Destroy(gameObject);
			}
			if (gameObject.tag == "platformB" && gameControl.playerColor != "blue")
			{
				gameControl.AudioMgr.GetComponent<AudioManager>().playSoundEffect(gameControl.AudioMgr.GetComponent<AudioManager>().breaks);
				PlayerPrefs.SetInt(gameControl.PP_PlatformsBroken, PlayerPrefs.GetInt(gameControl.PP_PlatformsBroken) + 1);
				PlayerPrefs.Save();
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerExit()
	{
		isTouching = false;
	}

	public GameObject spawnPlatform()
	{
		float MMheight = Random.Range(-1.5f, 1.3f);
		float MMdistance = Random.Range(6f, 8f);
		int MMrotation = Random.Range(-30, 30);
		//TODO:ADD ROTATION RANDOMIZATION HERE.. MAYBE?
		nextSpawnned = (GameObject)Instantiate(platformPrefab, transform.position + new Vector3(0, MMheight, MMdistance), transform.rotation/* + transform.Rotate(MMrotation,0,0)*/);
		gameControl.latestPlatform = nextSpawnned;
		gameControl.platforms++;

		return nextSpawnned;

	}
}