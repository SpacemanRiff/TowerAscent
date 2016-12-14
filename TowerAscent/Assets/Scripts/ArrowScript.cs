using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {
	
	private string controllerTag = "VRController";
    private string arrowName = "Arrow(Clone)";
    private string headName = "Camera (eye)";
	private bool hasBeenShot = false;
    private bool hasHit = false;
    public HandMovementLeft leftHand;
    public HandMovementRight rightHand;
    public VRTK.VRTK_InteractGrab lefty;
    public VRTK.VRTK_InteractGrab righty;
	private Rigidbody rb;
	private AudioSource arrowAudioSource;
	private Vector3 globalScaleAtStart;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		arrowAudioSource = GetComponent<AudioSource> ();
		globalScaleAtStart = transform.lossyScale;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if(hasBeenShot) {
			transform.forward = Vector3.Slerp(transform.forward, rb.velocity.normalized, Time.deltaTime * 5);
		}
	}


    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag.Equals(controllerTag) && !hasHit) {

			if ((leftHand.gripButtonPressed && lefty.GetGrabbedObject().Equals(this)) || (rightHand.gripButtonPressed && righty.GetGrabbedObject().Equals(this))) {
                SteamManager.ArrowGrabAchievement();
            }
                   
        }
        if (collision.gameObject.name.Equals(headName)) {
            SteamManager.HeadshotAchievement();
        }
        if (!collision.gameObject.name.Equals(arrowName)) {
			CollideWithAnythingButArrow(collision.gameObject);
        }
	}

	private void CollideWithAnythingButArrow(GameObject newParent) {
		//rb.constraints = RigidbodyConstraints.FreezeAll;
		if (!hasHit) {
			if (newParent.GetComponent<Rigidbody>() != null) {
				gameObject.AddComponent<FixedJoint>().connectedBody = newParent.GetComponent<Rigidbody>();
			} else {
				gameObject.AddComponent<FixedJoint>();
			}
			arrowAudioSource.Stop ();
			arrowAudioSource.Play ();
		}
		hasHit = true;
	}

	public void Shoot() {
		rb.useGravity = true;
		transform.SetParent(null);
		rb.constraints = RigidbodyConstraints.None;
		hasBeenShot = true;
	}


}
