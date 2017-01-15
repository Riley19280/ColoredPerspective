using UnityEngine;
using System.Collections;

public class coin : MonoBehaviour {
	public Transform coinEffect;
	public int rotationSpeed = 4;

	// Update is called once per frame
	void Update () {
		transform.Rotate(0,rotationSpeed * Time.deltaTime,0);


	}
		void OnTriggerEnter(Collider other) {
			if(other.tag == "Player"){
			Instantiate(coinEffect,transform.position,transform.rotation);
			gameControl.AudioMgr.GetComponent<AudioManager>().playSoundEffect(gameControl.AudioMgr.GetComponent<AudioManager>().coin);
			gameControl.coinCount ++;


				Destroy(gameObject);
		}
	}
		
		
		
		
	}
