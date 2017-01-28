using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdollSwitch : MonoBehaviour {
    //public ArrayList rb;
    public Animator animator;

	// Use this for initialization
	void Start () {
       // animator.enabled = false;
       // foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>()) {
       //     rb.isKinematic = true;
        //}
        //animator.enabled = true;
        //animator.Play("Walk Forward");


		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F)) {
            foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>()) {
                rb.isKinematic = false;
            }
            animator.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.H)) {
            foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>()) {
                rb.isKinematic = true;
            }
            animator.enabled = true;

            animator.Play("SmallStep");
        }

    }
    void OnTriggerEnter(Collider other) {
        if (other.name == "Arrow(Clone)") {
            animator.enabled = false;
            print("killed");
        }
    }
}
