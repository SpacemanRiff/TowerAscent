﻿using UnityEngine;
using System.Collections;

public class HelperMover : MonoBehaviour {

	public Transform centerOfRotation;
	public float desiredDistanceFromCenter;
	public float horizontalMoveSpeed = 5;
	public float verticalMoveSpeed = 5;
	public float maxHeight = 30;
	public float lowHeightLimit = 5;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		RotateToFaceCenter();
		float distanceToMoveForward = GetDistanceFromCenter() - desiredDistanceFromCenter;
		transform.position += transform.forward * distanceToMoveForward;
	}

	void FixedUpdate () {
		MovePlayer();
	}

	private void MovePlayer() {
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			//Move player left
			rb.AddForce(transform.right * -1 * horizontalMoveSpeed);
		} else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			//Move player right
			rb.AddForce(transform.right * horizontalMoveSpeed);
		}
		float angle = (transform.localEulerAngles.x > 180) ? transform.localEulerAngles.x - 360 : transform.localEulerAngles.x;
		if((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && angle < 30) {
			//Move player up
			rb.AddForce(transform.up * verticalMoveSpeed);
		} else if((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && transform.localPosition.y > lowHeightLimit) {
			//Move player down
			rb.AddForce(transform.up * -1 * verticalMoveSpeed);
		}

	}

	private void RotateToFaceCenter() {
		transform.LookAt(centerOfRotation.position);
	}

	private float GetDistanceFromCenter() {
		return Vector3.Distance(transform.position, centerOfRotation.position);
	}
}
