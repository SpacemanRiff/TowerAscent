﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArcherShooting : MonoBehaviour {

	public Texture2D crosshairImage;
	public const float crosshairMaxSize = 100.0f;
	public const float crosshairMinSize = 25.0f;
	public float scalingSpeed = 0.01f;
	public float maxForce = 2.0f;
	public float scaleMinimum = 0.7f;
	public float arrowSpeed = 10.0f;
	public int maxNumberOfArrows = 4;
	public GameObject arrow;

	private float currentScale;
	private float currentForce;
	private float zOffset = 20.0f;
	private Queue arrowsInGame = new Queue();

	// Use this for initialization
	void Start () {
		currentScale = 1;
		currentForce = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)) {
			if(currentScale > scaleMinimum) {
				currentScale = currentScale - scalingSpeed;
				currentForce = currentForce + (1 - currentScale) * maxForce;
			}
		}
		if(Input.GetMouseButtonUp(0)) {
			Vector3 currentMousePosition = Input.mousePosition;
			Vector3 placeToGo = Camera.main.ScreenToWorldPoint(new Vector3(currentMousePosition.x, currentMousePosition.y, zOffset));
			if(arrowsInGame.Count > maxNumberOfArrows) {
				GameObject arrowToDestroy = (GameObject)arrowsInGame.Dequeue();
				GameObject.Destroy(arrowToDestroy);
			}
			GameObject arrowShot = (GameObject)Instantiate(arrow, transform.position, transform.rotation);
			arrowsInGame.Enqueue(arrowShot);
			arrowShot.transform.LookAt(placeToGo);
			arrowShot.GetComponent<Rigidbody>().AddForce(arrowShot.transform.forward * arrowSpeed * currentForce, ForceMode.Impulse);
			currentScale = 1;
			currentForce = 1;
		}
	}

	void OnGUI() {
		float xMin = (Input.mousePosition.x) - ((crosshairMaxSize * currentScale) / 2);
		float yMin = (Screen.height - Input.mousePosition.y) - ((crosshairMaxSize * currentScale) / 2);
		GUI.DrawTexture(new Rect(xMin, yMin, crosshairMaxSize * currentScale, crosshairMaxSize * currentScale), crosshairImage);
	}
}
