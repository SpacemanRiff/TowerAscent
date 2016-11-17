using UnityEngine;
using System.Collections;

public class PivitBallScript : MonoBehaviour {

	public Transform keepUpTransform;
	public float maxHeight = 10;

	// Use this for initialization
	void Start () {
		print("First: " + transform.position.y);
		print("Second: " + keepUpTransform.position.y);
		print("Third: " + transform.localPosition.y);
	}
	
	// Update is called once per frame
	void Update () {
		print("First: " + transform.position.x);
		print("Second: " + keepUpTransform.position.y);

		float heightNeeded = keepUpTransform.position.y - transform.position.y;

		if(transform.position.y < maxHeight || heightNeeded < 0) {
			transform.Translate(0,heightNeeded,0);
		}
	}
}