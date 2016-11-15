using UnityEngine;
using System.Collections;

public class HandMovementLeft : MonoBehaviour {
    public GameObject thumb, thumbKnuckle, thumbWrist;
    public GameObject[] gripfingers, GripKnucklesTwo, GripKnucklesOne;
    public AudioClip grabSound;
    public AudioClip hoverSound;
    private AudioSource audioSource;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    public HandMovementRight otherHand;

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    public bool gripButtonDown = false;
    public bool gripButtonUp = false;
    public bool gripButtonPressed = false;

    public Timer timer;
    private Valve.VR.EVRButtonId restartButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
    public bool restartButtonDown = false;
    public bool restartButtonUp = false;
    public bool restartButtonPressed = false;

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerButtonDown = false;
    public bool triggerButtonUp = false;
    public bool triggerButtonPressed = false;
    private Quaternion gripRotation, thumbRotation, thumbKnuckleRotation, 
        gripKnuckleOneRotation, gripKnuckleTwoRotation, thumbWristRotation;

    // Use this for initialization
    void Start () {
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
	void Update () {
        //check for grip button, then close grip fingers and thumb
        
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

        restartButtonDown = controller.GetPressDown(restartButton);

        if (restartButtonDown && !gripButtonPressed && !otherHand.gripButtonPressed)
        {
            Debug.Log("Bloop");
            timer.reset();
        }

        gripButtonDown = controller.GetPressDown(gripButton);
        gripButtonUp = controller.GetPressUp(gripButton);
        gripButtonPressed = controller.GetPress(gripButton);

        triggerButtonDown = controller.GetPressDown(triggerButton);
        triggerButtonUp = controller.GetPressUp(triggerButton);
        triggerButtonPressed = controller.GetPress(triggerButton);

        if (gripButtonDown)
        {
            Quaternion Rotation;
           
            foreach (GameObject finger in gripfingers)
            {
                Rotation = finger.transform.localRotation;
                Rotation.eulerAngles = new Vector3(Rotation.x - 60, Rotation.y, Rotation.z);
                finger.transform.localRotation = Rotation;
            }
            foreach (GameObject knuckle in GripKnucklesOne) {
                Rotation = knuckle.transform.localRotation;
                Rotation.eulerAngles = new Vector3(Rotation.x - 60, Rotation.y, Rotation.z);
                knuckle.transform.localRotation = Rotation;
            }
            foreach (GameObject knuckle in GripKnucklesTwo)
            {
                Rotation = knuckle.transform.localRotation;
                Rotation.eulerAngles = new Vector3(Rotation.x - 60, Rotation.y, Rotation.z);
                knuckle.transform.localRotation = Rotation;
            }

            Rotation = thumb.transform.localRotation;
            Rotation.eulerAngles = new Vector3(-9.894f, -64.667f, -5.960f);
            thumb.transform.localRotation = Rotation;

            Rotation = thumbKnuckle.transform.localRotation;
            Rotation.eulerAngles = new Vector3(-35.716f, -61.774f, 23.527f);
            thumbKnuckle.transform.localRotation = Rotation;

            Rotation = thumbWrist.transform.localRotation;
            Rotation.eulerAngles = new Vector3(-25.32f, 2.127f, -31.233f);
            thumbWrist.transform.localRotation = Rotation;

            //Debug.Log("Grip was pressed");
        }

        if (gripButtonUp)
        {
            Quaternion Rotation;
            foreach (GameObject finger in gripfingers)
            {
                Rotation = finger.transform.localRotation;
                Rotation.eulerAngles = new Vector3(Rotation.x, Rotation.y, Rotation.z);
                finger.transform.localRotation = Rotation;
            }
            foreach (GameObject knuckle in GripKnucklesOne) {
                Rotation = knuckle.transform.localRotation;
                Rotation.eulerAngles = new Vector3(Rotation.x, Rotation.y, Rotation.z);
                knuckle.transform.localRotation = Rotation;
            }
            foreach (GameObject knuckle in GripKnucklesTwo)
            {
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
        if (other.tag == "Climbable" && gripButtonDown) {
            audioSource.PlayOneShot(grabSound, 0.15f);
        }
        else if (other.tag == "Climbable" && !gripButtonDown)
        {
            audioSource.PlayOneShot(hoverSound, 0.05f);
        }
    }
}
