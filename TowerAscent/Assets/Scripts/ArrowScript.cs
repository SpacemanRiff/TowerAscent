using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {
	
	private string controllerTag = "VRController";
	private string towerTag = "Tower";
    private string bowTag = "Bow";
    private string arrowName = "Arrow(Clone)";
    private string headName = "Camera (eye)";
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnCollisionEnter(Collision collision) {
        print(collision.gameObject.name);
        if (collision.gameObject.name.Equals(headName)) {
            print("Hit head");
            SteamManager.HeadshotAchievement();
        }
        if (!collision.gameObject.name.Equals(arrowName)) {
            rb.useGravity = false;
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
        }
	}

    void OnCollisionExit(Collision collision)
    {
        print("Left Collider");
        rb.useGravity = true;
        rb.isKinematic = false;
    }



    private void StartDegrading() {
		//Slowly start rotating down for X ammount of time
		//Once time has been reached the arrow should snap and fall to ground
		//If player is holding the arrow then it should stay in players hand until he lets go
	}
}
