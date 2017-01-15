using UnityEngine;
using System.Collections;

public class rotateAround : MonoBehaviour {

	public GameObject target;
	public int speed=2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(target.transform);
		transform.Translate(Vector3.right * Time.deltaTime*speed);
	}
}
