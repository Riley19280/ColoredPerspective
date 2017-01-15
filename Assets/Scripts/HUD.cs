using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

	public GameObject coinsCollected;

	void Start()
	{

	}

	void Update()
	{
		coinsCollected.GetComponent<Text>().text = "Coins Collected: " + gameControl.coinCount;
	}


}
