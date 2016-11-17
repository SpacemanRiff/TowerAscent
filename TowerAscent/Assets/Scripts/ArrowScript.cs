using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {

	private string controllerTag = "VRController";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag(controllerTag)) {
			//if player is grabbing me then do this
			StartDegrading();
		}
	}

	private void StartDegrading() {
		//Slowly start rotating down for X ammount of time
		//Once time has been reached the arrow should snap and fall to ground
		//If player is holding the arrow then it should stay in players hand until he lets go
	}
}
