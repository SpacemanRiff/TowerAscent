using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {


	public GameObject CheckpointSpawn;
	public Vector3 newRigPos;
	public Timer timer;

	// Use this for initialization
	void Start () {
		newRigPos = CheckpointSpawn.transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "VRController") {
			timer.updateCameraRigPosition (newRigPos);
		}
	
	}
}
