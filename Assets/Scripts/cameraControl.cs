using UnityEngine;
using System.Collections;

public class cameraControl : MonoBehaviour {
	public Transform target;
	public int distance = 70;
	public int lift = 5;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(target);
		if(transform.eulerAngles.y <= 200){
			//Debug.Log ("left");
			transform.Translate(0,0,-distance,Space.World);

		}
		if(transform.eulerAngles.y >= 340){
			//Debug.Log("rigt");
			transform.Translate(0,0,distance,Space.World);
		}
	}
}
