using UnityEngine;
using System.Collections;

public class PivitBallScript : MonoBehaviour {

	public Transform keepUpTransform;
	public float maxHeight = 10;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		float heightNeeded = keepUpTransform.position.y - transform.position.y;

		if(transform.position.y < maxHeight || heightNeeded < 0) {
			transform.Translate(0,heightNeeded,0);
		}
	}
}