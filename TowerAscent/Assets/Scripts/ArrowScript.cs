using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {
	
	private string controllerTag = "VRController";
	private string towerTag = "Tower";
    private string bowTag = "Bow";
    private string arrowName = "Arrow(Clone)";
    private string headName = "Camera (eye)";
	private bool hasBeenShot = false;
    private bool hasHit = false;
    public HandMovementLeft leftHand;
    public HandMovementRight rightHand;
    public VRTK.VRTK_InteractGrab lefty;
    public VRTK.VRTK_InteractGrab righty;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(hasBeenShot) {
			transform.forward = Vector3.Slerp(transform.forward, rb.velocity.normalized, Time.deltaTime * 5);
		}
	}


    void OnCollisionEnter(Collision collision) {
        print(collision.gameObject.name);
        if (collision.gameObject.tag.Equals(controllerTag) && !hasHit) {
            if (leftHand.gripButtonPressed && lefty.GetGrabbedObject().Equals(this))
            {
                SteamManager.ArrowGrab();

            }
            else if (rightHand.gripButtonPressed && righty.GetGrabbedObject().Equals(this))
            {
                SteamManager.ArrowGrab();
            }
            print("Grabbed Object: "+ righty.GetGrabbedObject());
            print("Grabbed Object: " + lefty.GetGrabbedObject());
               
        }
        if (collision.gameObject.name.Equals(headName)) {
            print("Hit head");
            SteamManager.HeadshotAchievement();
        }
        if (!collision.gameObject.name.Equals(arrowName)) {
            rb.useGravity = false;
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            hasHit = true;
        }
	}

    void OnCollisionExit(Collision collision)
    {
        print("Left Collider");
        rb.useGravity = true;
        rb.isKinematic = false;
    }

	public void shoot() {
		hasBeenShot = true;
	}
}
