using UnityEngine;

public class HandMovementRight : MonoBehaviour {
    public AudioClip grabSound, hoverSound, resetSound;
    private AudioSource audioSource;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    public GameObject rightHand;
    private Animator handAnimator;
    
    public Timer timer;

    public HandMovementLeft otherHand;

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    public bool gripButtonDown = false, gripButtonUp = false, gripButtonPressed = false;
    
    private Valve.VR.EVRButtonId restartButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
    public bool restartButtonDown = false, restartButtonUp = false, restartButtonPressed = false;

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerButtonDown = false, triggerButtonUp = false, triggerButtonPressed = false;

    // Use this for initialization
    void Start() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        audioSource = GetComponent<AudioSource>();
        handAnimator = rightHand.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        if (controller == null) {
            Debug.Log("Controller not initialized");
            return;
        }

        restartButtonDown = controller.GetPressDown(restartButton);

        gripButtonDown = controller.GetPressDown(gripButton);
        gripButtonUp = controller.GetPressUp(gripButton);
        gripButtonPressed = controller.GetPress(gripButton);

        triggerButtonDown = controller.GetPressDown(triggerButton);
        triggerButtonUp = controller.GetPressUp(triggerButton);
        triggerButtonPressed = controller.GetPress(triggerButton);

        if (restartButtonDown && !gripButtonPressed && !otherHand.gripButtonPressed) {
            timer.reset();
            audioSource.PlayOneShot(resetSound, 1.00f);
        }

        if (gripButtonDown) {
            handAnimator.SetBool("grabbing", true);
        }

        if (gripButtonUp) {
            handAnimator.SetBool("grabbing", false);
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
