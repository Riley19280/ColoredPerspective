using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class playerMain : MonoBehaviour
{

	bool isGrounded = true;
	public int moveSpeed = 100;
	public AudioClip swoosh;
	public AudioClip hit;
	int colorNum = 0;

	public Material[] material = new Material[3];

	Dictionary<int, Vector2> startpos = new Dictionary<int, Vector2>();
	Dictionary<int, int> starttime = new Dictionary<int, int>();

	void Start()
	{
		GetComponent<Renderer>().material = material [0];
		gameControl.playerColor = "red";

		if (PlayerPrefs.GetInt(gameControl.PP_TiltControls) == 1)
			tilt = true;		
	}

	int leftIndex = -1, rightIndex = -1, swipeIndex = -1;

	bool tilt = false;
	// Update is called once per frame
	void Update()
	{
		if (transform.position.y <= -20)
		{
			SceneManager.LoadScene("highscore");
		}



		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || 1 == 1)
		{
			if (tilt)
			{
				TiltControls();
			} else
			{
				TouchControls();
			}
		} else
		{
			
		}
		PcControls();
	}


	void TiltControls()
	{
		GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(-Input.acceleration.x * -moveSpeed * Time.deltaTime, 0, 0));

		GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(-Input.acceleration.z * moveSpeed * Time.deltaTime, 0, 0));

		foreach (Touch t in Input.touches)
		{

			if (t.phase == TouchPhase.Began)
			{
				if (!startpos.ContainsKey(t.fingerId))
					startpos.Add(t.fingerId, t.position);
			}

			//swipe
			if (t.phase == TouchPhase.Moved && isGrounded == true)
			{
				if (t.position.y - startpos [t.fingerId].y > Screen.height / 3)
				{
					swipeIndex = t.fingerId;
					GetComponent<Rigidbody>().AddForce(new Vector3(0, PlayerPrefs.GetInt(gameControl.PP_jumpStrength), 0));
					isGrounded = false;
					gameControl.AudioMgr.GetComponent<AudioManager>().playSoundEffect(gameControl.AudioMgr.GetComponent<AudioManager>().swoosh);

				}
			}

			if (t.phase == TouchPhase.Ended && t.fingerId != swipeIndex)
			{
				switchColor(1);
			}


			if (t.phase == TouchPhase.Ended && t.fingerId == swipeIndex)
			{
				swipeIndex = -1;
			}

		}

		////removing unused startpos keys
		//Dictionary<int, Vector2> remKeys = startpos;
		//foreach (Touch t in Input.touches)
		//{
		//	if (remKeys.ContainsKey(t.fingerId))
		//		remKeys.Remove(t.fingerId);
		//}
		//foreach (var item in remKeys)
		//{
		//	startpos.Remove(item.Key);
		//}
		//remKeys.Clear();

	}

	void TouchControls()
	{
		Vector2 dims = new Vector2(Screen.width, Screen.height);
		Rect leftRect = new Rect(0, 0, Screen.width / 2, Screen.height);
		Rect rightRect = new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height);

		foreach (Touch t in Input.touches)
		{
			bool swiped = false;
			if (t.phase == TouchPhase.Began)
			{
				if (!startpos.ContainsKey(t.fingerId))
					startpos.Add(t.fingerId, t.position);

				if (!starttime.ContainsKey(t.fingerId))
					starttime.Add(t.fingerId, DateTime.Now.Millisecond);
			}

			//swipe
			if (isGrounded)
			{
				if (t.position.y - startpos [t.fingerId].y > Screen.height / 3)
				{
					swipeIndex = t.fingerId;
					GetComponent<Rigidbody>().AddForce(new Vector3(0, PlayerPrefs.GetInt(gameControl.PP_jumpStrength), 0));
					isGrounded = false;
					gameControl.AudioMgr.GetComponent<AudioManager>().playSoundEffect(gameControl.AudioMgr.GetComponent<AudioManager>().swoosh);
					swiped = true;
				}
			}

			//getting touch ids
			if (leftRect.Contains(t.position) && !swiped)
			{
				if (t.phase == TouchPhase.Stationary)
				{
					leftIndex = t.fingerId;
					GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
				}
			}

			if (rightRect.Contains(t.position) && !swiped)
			{
				if (t.phase == TouchPhase.Stationary)
				{
					rightIndex = t.fingerId;
					GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(moveSpeed * Time.deltaTime, 0, 0));

				}
			}

			if (t.phase == TouchPhase.Ended)
			{
				if (DateTime.Now.Millisecond - starttime [t.fingerId] > 350)
				{
					break;
				}


				if (t.fingerId != swipeIndex)
					switchColor(1);

				if (t.fingerId == swipeIndex)
				{
					swipeIndex = -1;
					starttime.Remove(t.fingerId);
				}
				if (t.fingerId == leftIndex)
				{
					leftIndex = -1;
					starttime.Remove(t.fingerId);
				}
				if (t.fingerId == rightIndex)
				{
					rightIndex = -1;
					starttime.Remove(t.fingerId);
				}
			}
		}
	}

	void PcControls()
	{
		if (Input.GetKeyDown(KeyCode.W))
			switchColor(1);

		if (Input.GetKeyDown(KeyCode.S))
			switchColor(-1);

		if (Input.GetKey(KeyCode.A))
		{
			GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
		}
		if (Input.GetKey(KeyCode.D))
		{
			GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
		}
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
		{
			GetComponent<Rigidbody>().AddForce(new Vector3(0, PlayerPrefs.GetInt(gameControl.PP_jumpStrength), 0));
			isGrounded = false;
			gameControl.AudioMgr.GetComponent<AudioManager>().playSoundEffect(gameControl.AudioMgr.GetComponent<AudioManager>().swoosh);
		}
	}

	public void switchColor(int val)
	{
		colorNum += val;

		switch (Math.Abs(colorNum % 3))
		{
			case 0:
				gameControl.playerColor = "red";
				GetComponent<Renderer>().material = material [0];
				break;
			case 1:
				gameControl.playerColor = "green";
				GetComponent<Renderer>().material = material [1];
				break;
			case 2:
				gameControl.playerColor = "blue";
				GetComponent<Renderer>().material = material [2];
				break;
		}
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.tag == "platform" || other.tag == "platformR" || other.tag == "platformG" || other.tag == "platformB" || other.tag == "Respawn")
		{
			isGrounded = true;
			gameControl.AudioMgr.GetComponent<AudioManager>().playSoundEffect(gameControl.AudioMgr.GetComponent<AudioManager>().hit);
		}

	}
}