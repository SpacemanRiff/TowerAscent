using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class FileInputOutput : MonoBehaviour {

	//CHECKLIST:
	//ATTACH THIS SCRIPT TO EMPTY GAMEOBJECT
	//ATTACH TIMER SCRIPT TO THIS SCRIPT
	//ATTACH THIS SCRIPT TO TIMER SCRIPT
	//ATTACH GHOSTHAND GAMEOBJECTS TO THIS SCRIPT

	bool noGhostData = false;
	List<Vector3> positionArray = new List<Vector3>();
	List<Vector3> newPositionArray = new List<Vector3>();
	List<Quaternion> rotationArray = new List<Quaternion> ();
	List<Quaternion> newRotationArray = new List<Quaternion> ();
	public GameObject leftHand;
	public GameObject rightHand;
	int arrayWritingIndex = 0;
	int arrayReadingIndex = 0;
	public Timer Timer;
	public bool PlayerJustReset = false;
	bool playGhost = true;
	public GameObject leftHandGhost;
	public GameObject rightHandGhost;
	char[] Delimters = {' ' };

	// Use this for initialization
	void Start () {
	}

	void haveGhostAppear() {
		if (playGhost == true) {
            Debug.Log("playGhost = true");
			//Set ghost hand positions
			if (positionArray.Count >= 1) {
                Debug.Log("positionArray.Count >= 1");
				if (arrayReadingIndex < positionArray.Count) {
					leftHandGhost.transform.position = positionArray [arrayReadingIndex];
					leftHandGhost.transform.rotation = rotationArray [arrayReadingIndex];
					arrayReadingIndex++;
					rightHandGhost.transform.position = positionArray [arrayReadingIndex];
					rightHandGhost.transform.rotation = rotationArray [arrayReadingIndex];
					arrayReadingIndex++;
				} else {
					playGhost = false;
				}
			} 
		}
	}

	void Update () {
		//Debug.Log ("Timer.stopped = " + Timer.stopped);
		if (Timer.stopped == false) {
			//Set ghost hand positions
			if (positionArray.Count >= 1) {
				haveGhostAppear ();
			}
			//Ghost data recording
			//Position
			Vector3 leftHandPosition = leftHand.transform.position;
			Vector3 rightHandPosition = rightHand.transform.position;
			//Rotation
			var leftEuler = leftHand.transform.rotation.eulerAngles;
			var rightEuler = rightHand.transform.rotation.eulerAngles;
			Quaternion leftHandRotation = Quaternion.Euler (leftEuler.x, leftEuler.y, leftEuler.z);
			Quaternion rightHandRotation = Quaternion.Euler (rightEuler.x, rightEuler.y, rightEuler.z);

			//Debug.Log ("leftHandPosition = " + leftHandPosition);
			newPositionArray.Add (leftHandPosition);
			newPositionArray.Add (rightHandPosition);
			newRotationArray.Add (leftHandRotation);
			newRotationArray.Add (rightHandRotation);
		}
			//If no previous ghost exists
			else {
			leftHandGhost.transform.position = leftHand.transform.position + new Vector3 (-1000f, -1000f, -1000f);
			rightHandGhost.transform.position = rightHand.transform.position + new Vector3 (-1000f, -1000f, -1000f);
		}
		//Save ghost data on reset
		if (PlayerJustReset == true) {
            Debug.Log("PlayerJustReset = true");
            //Remove ghost hands from vision
            leftHandGhost.transform.position = leftHand.transform.position + new Vector3 (-1000f, -1000f, -1000f);
			rightHandGhost.transform.position = rightHand.transform.position + new Vector3 (-1000f, -1000f, -1000f);

			//Record ghost data and reset values
			foreach (Vector3 position in newPositionArray) {
				positionArray.Add (position);
			}
			foreach (Quaternion rotation in newRotationArray) {
				rotationArray.Add (rotation);
			}

			//for (int x = 0; x < positionArray.Count; x++) {
			//	Debug.Log ("Position array at index " + x + " = " + positionArray [x]);
			//}

			newPositionArray.Clear ();
			newRotationArray.Clear ();
			arrayReadingIndex = 0;
            playGhost = true;
            Debug.Log("playGhost = true");
			PlayerJustReset = false;
		}
	}
}
