using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keepUp : MonoBehaviour {
    public Text timeText;
    public GameObject hand;
    public Vector3 offset = new Vector3(0.0f, 2.0f, 0.0f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = hand.transform.position + offset;
		
	}
}
