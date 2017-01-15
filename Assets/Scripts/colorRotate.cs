using UnityEngine;
using System.Collections;

public class colorRotate : MonoBehaviour {
	public Material[] material = new Material[3];

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material = material[0];
	}
	int ctr = 0;

	int color =0;

	// Update is called once per frame
	void FixedUpdate () {

		if(ctr==90){
			ctr = 0;
			color ++;
			GetComponent<Renderer>().material = material[color%3];
		}
		ctr++;
	}
}
