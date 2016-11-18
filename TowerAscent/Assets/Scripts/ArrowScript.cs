using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {
	
	public Texture2D crosshairImage;
	private string controllerTag = "VRController";
	private string towerTag = "Tower";
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag(controllerTag)) {
			//if player is grabbing me then do this
			StartDegrading();
		}
		if (other.CompareTag(towerTag)) {
			//Stop Arrow Movement Entirely
			rb.velocity = Vector3.zero;
		}
	}

	void OnGUI() {
		float xMin = (Screen.width - Input.mousePosition.x) - (crosshairImage.width / 2);
		float yMin = (Screen.height - Input.mousePosition.y) - (crosshairImage.height / 2);
		GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
	}

	private void StartDegrading() {
		//Slowly start rotating down for X ammount of time
		//Once time has been reached the arrow should snap and fall to ground
		//If player is holding the arrow then it should stay in players hand until he lets go
	}
}
