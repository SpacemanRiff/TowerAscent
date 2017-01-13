using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingScript : MonoBehaviour {
    public HandMovementLeft LeftHand;
    public HandMovementRight RightHand;
    public GameObject Left;
    public GameObject Right;
    bool distanceSet = false;
    public GameObject head;
    float leftDistance;
    Rigidbody cameraRigRigidbody;
    public float forceMultiplier = 10;
    

    // Use this for initialization
    void Start () {
        cameraRigRigidbody = this.GetComponent<Rigidbody>();    
}
	
	// Update is called once per frame
	void FixedUpdate () {
        cameraRigRigidbody = this.GetComponent<Rigidbody>();

        if (LeftHand.triggerButtonPressed) {
            if (!distanceSet) {
                leftDistance = Vector3.Distance(transform.position, Left.transform.position);
                //print(leftDistance);
                distanceSet = true;
            }
            if (!LeftHand.gripButtonPressed) {
                cameraRigRigidbody.useGravity = false;
                cameraRigRigidbody.isKinematic = false;
                cameraRigRigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
                if (Vector3.Distance(transform.position, Left.transform.position) > leftDistance) {
                    //print(Vector3.Distance(transform.position, Left.transform.position));
                    //print("moving");
                    cameraRigRigidbody.AddForce(head.transform.forward * forceMultiplier, ForceMode.Acceleration);
                }
                if (Vector3.Distance(transform.position, Left.transform.position) < leftDistance) {
                   // print(Vector3.Distance(transform.position, Left.transform.position));
                   // print("moving");
                    cameraRigRigidbody.AddForce(head.transform.forward * forceMultiplier * -1, ForceMode.Impulse);
                }
            }

        } 
        else {
            if (!cameraRigRigidbody.useGravity && !cameraRigRigidbody.isKinematic) {
                cameraRigRigidbody.useGravity = true;
                cameraRigRigidbody.isKinematic = true;
                distanceSet = false;
                cameraRigRigidbody.constraints =RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
            }
        }

    }
}
