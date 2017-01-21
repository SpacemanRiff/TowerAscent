using UnityEngine;

public class HandMovementLeft : MonoBehaviour {
    public AudioClip grabSound, hoverSound;
    private AudioSource audioSource;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    public GameObject leftHand;
    private Animator handAnimator;
    
    public Timer timer;

    public HandMovementRight otherHand;

    public VRTK.VRTK_InteractGrab IGrab;

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    public bool gripButtonDown = false, gripButtonUp = false, gripButtonPressed = false;

    private Valve.VR.EVRButtonId restartButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
    public bool restartButtonDown = false, restartButtonUp = false, restartButtonPressed = false;

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerButtonDown = false, triggerButtonUp = false, triggerButtonPressed = false;

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        handAnimator = leftHand.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        
        if (controller == null) {
            Debug.Log("Controller not initialized");
            return;
        }

        restartButtonDown = controller.GetPressDown(restartButton);

        if (restartButtonDown && !gripButtonPressed && !otherHand.gripButtonPressed) {
            timer.reset();
        }

        gripButtonDown = controller.GetPressDown(gripButton);
        gripButtonUp = controller.GetPressUp(gripButton);
        gripButtonPressed = controller.GetPress(gripButton);

        triggerButtonDown = controller.GetPressDown(triggerButton);
        triggerButtonUp = controller.GetPressUp(triggerButton);
        triggerButtonPressed = controller.GetPress(triggerButton);
        
        if (gripButtonDown) {
            handAnimator.SetBool("grabbing", true);
        }

        if (gripButtonUp) {
            handAnimator.SetBool("grabbing", false);
            //IGrab.ForceRelease();
        }

        if (!gripButtonPressed) {
            //handAnimator.SetBool("grabbing", false);
            //IGrab.ForceRelease();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Climbable" && gripButtonDown) {
            audioSource.PlayOneShot(grabSound, 0.15f);
        }
        else if (other.tag == "Climbable" && !gripButtonDown) {
            audioSource.PlayOneShot(hoverSound, 0.05f);
        }
    }
}
