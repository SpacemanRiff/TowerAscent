using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewNextLeveler : MonoBehaviour {

	public SteamVR_LoadLevel SteamVRLevel;
	public string levelname;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "VRController") {
			SteamVRLevel.Trigger();
		}
	}
}
