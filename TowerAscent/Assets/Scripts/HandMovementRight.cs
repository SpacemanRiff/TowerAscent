using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HandMovementRight : MonoBehaviour {
    public GameObject thumb, thumbKnuckle, thumbWrist;
    public GameObject[] gripfingers, GripKnucklesTwo, GripKnucklesOne;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;
    public Timer timer;
    public GameObject rightHand;
    public lbReceive reciever;
    public AudioClip grabSound;
    public AudioClip hoverSound;
    public AudioClip resetSound;

    private AudioSource audioSource;

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    public bool gripButtonDown = false;
    public bool gripButtonUp = false;
    public bool gripButtonPressed = false;
    private bool colliding;

    public HandMovementLeft otherHand;



    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private Valve.VR.EVRButtonId restartButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
    public bool restartButtonDown = false;
    public bool restartButtonUp = false;
    public bool restartButtonPressed = false;
    public bool triggerButtonDown = false;
    public bool triggerButtonUp = false;
    public bool triggerButtonPressed = false;
    private Quaternion gripRotation, thumbRotation, thumbKnuckleRotation, 
        gripKnuckleOneRotation, gripKnuckleTwoRotation, thumbWristRotation;

    // Use this for initialization
    void Start() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        gripRotation = gripfingers[0].transform.localRotation;
        gripKnuckleOneRotation = GripKnucklesOne[0].transform.localRotation;
        gripKnuckleTwoRotation = GripKnucklesTwo[0].transform.localRotation;
        thumbRotation = thumb.transform.localRotation;
        thumbKnuckleRotation = thumbKnuckle.transform.localRotation;
        thumbWristRotation = thumbWrist.transform.localRotation;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        //check for grip button, then close grip fingers and thumb

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
            StartCoroutine(reciever.receive());
        }

        if (gripButtonDown) {
            Quaternion Rotation;
            foreach (GameObject finger in gripfingers) {
                Rotation = finger.transform.localRotation;
                Rotation.eulerAngles = new Vector3(Rotation.x - 60, Rotation.y, Rotation.z);
                finger.transform.localRotation = Rotation;
            }
            foreach (GameObject knuckle in GripKnucklesOne) {
                Rotation = knuckle.transform.localRotation;
                Rotation.eulerAngles = new Vector3(Rotation.x - 60, Rotation.y, Rotation.z);
                knuckle.transform.localRotation = Rotation;
            }
            foreach (GameObject knuckle in GripKnucklesTwo) {
                Rotation = knuckle.transform.localRotation;
                Rotation.eulerAngles = new Vector3(Rotation.x - 60, Rotation.y, Rotation.z);
                knuckle.transform.localRotation = Rotation;
            }
            Rotation = thumb.transform.localRotation;
            Rotation.eulerAngles = new Vector3(-18, -12, 37);
            thumb.transform.localRotation = Rotation;

            Rotation = thumbKnuckle.transform.localRotation;
            Rotation.eulerAngles = new Vector3(-.2f, 32, 4);
            thumbKnuckle.transform.localRotation = Rotation;

            Rotation = thumbWrist.transform.localRotation;
            Rotation.eulerAngles = new Vector3(15, 20, -140);
            thumbWrist.transform.localRotation = Rotation;

            //Debug.Log("Grip was pressed");
        }

        if (gripButtonUp) {
            Quaternion Rotation;
            foreach (GameObject finger in gripfingers) {
                Rotation = finger.transform.localRotation;
                Rotation.eulerAngles = new Vector3(Rotation.x, Rotation.y, Rotation.z);
                finger.transform.localRotation = Rotation;
            }
            foreach (GameObject knuckle in GripKnucklesOne) {
                Rotation = knuckle.transform.localRotation;
                Rotation.eulerAngles = new Vector3(Rotation.x, Rotation.y, Rotation.z);
                knuckle.transform.localRotation = Rotation;
            }
            foreach (GameObject knuckle in GripKnucklesTwo) {
                Rotation = knuckle.transform.localRotation;
                Rotation.eulerAngles = new Vector3(Rotation.x, Rotation.y, Rotation.z);
                knuckle.transform.localRotation = Rotation;
            }
            Rotation = thumb.transform.localRotation;
            Rotation.eulerAngles = new Vector3(Rotation.x, Rotation.y, Rotation.z);
            thumb.transform.localRotation = Rotation;

            Rotation = thumbKnuckle.transform.localRotation;
            Rotation.eulerAngles = new Vector3(Rotation.x, Rotation.y, Rotation.z);
            thumbKnuckle.transform.localRotation = Rotation;

            Rotation = thumbWrist.transform.localRotation;
            Rotation.eulerAngles = new Vector3(Rotation.x, Rotation.y, Rotation.z);
            thumbWrist.transform.localRotation = thumbWristRotation;

            //Debug.Log("Grip was released");
        }

        

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Climbable" && gripButtonDown)
        {
            audioSource.PlayOneShot(grabSound, 0.15f);
        }
        else if (other.tag == "Climbable" && !gripButtonDown)
        {
            audioSource.PlayOneShot(hoverSound, 0.05f);
        }
    }


}
