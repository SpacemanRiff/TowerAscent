using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSaying : MonoBehaviour {
	public bool grabbed = false;
	public HandMovementLeft leftHand;
	public HandMovementRight rightHand;
	public string[] sayingList;
	public Text sayingText;
	int randomNum;

	void Start () {
		randomNum = Random.Range (0, sayingList.Length); 
	}
	

	void Update () {
		//get rid of this, just for testing in inspector
		//if (grabbed){
			//grabbed = false;
			//randomSaying();
		//}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "VRController") {
			if (!grabbed && (leftHand.gripButtonDown || rightHand.gripButtonDown)) {
				grabbed = true;
			}
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "VRController" && grabbed) {
			grabbed = false;
			randomSaying();
		}
	}

	void randomSaying(){
		randomNum = Random.Range (0, sayingList.Length);
		if (sayingList != null) {
			sayingText.text = sayingList [randomNum].ToString();
		}
	}
}
