using UnityEngine;
using System.Collections;

public class spawnpointPlatform : MonoBehaviour
{
	public GameObject platformPrefab;

	// Use this for initialization
	void Start ()
	{
		gameControl.lastPlatform = (GameObject)Instantiate (platformPrefab, transform.position + new Vector3 (0, 0, 6), transform.rotation);
		gameControl.latestPlatform = gameControl.lastPlatform;
			
		for (int i = 0; i < gameControl.maxPlatformCount; i++) {
			gameControl.startPath ();
		}
		
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

}
