using UnityEngine;
using System.Collections;

public class YOYOUBEENSPOTTED : MonoBehaviour {

	//YO THIS SCRIPT REQUIRES THE USE OF LAYERS. DONT FORGET WHEN IMPLEMENTING
	//YO THIS SCRIPT USES COLLIDERS. DONT FORGET TO SET THOSE COLLIDERS TO TRIGGERS WHEN IMPLEMENTING
	//YO, YOU CAN CHANGE THE VALUE IN RAYCASTADJUSTSETTINGS (IN INSPECTOR OR THE START FUNCTION) TO ADJUST WHERE RAYCAST IS SHOT FROM
	//YO, YOU CAN CHANGE WHAT OBJECT THIS SCRIPT LOOKS FOR (IN INSPECTOR OR THE START FUNCTION)

	int layerMask;
	public Vector3 raycastAdjustSettings;
	public string tagToCheckAgainst;
    public bool playerHasBeenSpotted = false;
    public Timer timer;
    public AudioClip alertSound;

    private AudioSource audioSource;
    // Use this for initialization
    void Start () {
        //raycastAdjustSettings = new Vector3(INSERTXHERE, INSERTYHERE, INSERTZHERE);
        //tagToCheckAgainst = "INSERTSTRINGHERE";
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		//FOLLOWING IS NECESSARY FOR LAYERMASK FILTERING OF RAYCAST

		// Bit shift the index of the layer (8) to get a bit mask
		layerMask = 1 << 2;

		// This would cast rays only against colliders in layer 8.
		// But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
		layerMask = ~layerMask;
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == tagToCheckAgainst) {
			//CODE FOR WHEN THE PLAYER STAYS IN THIS OBJECTS FIELD OF VISION
			//CODE ALSO ENSURES THAT THIS OBJECT CAN NOT "SEE" THE PLAYER THROUGH OBSTACLES SUCH AS WALLS
			RaycastHit hit = new RaycastHit();
			Vector3 thisPosition = this.transform.position;
			thisPosition = this.transform.position + raycastAdjustSettings;

			if (Physics.Raycast (thisPosition, -(this.transform.position - other.transform.position), out hit, Mathf.Infinity, layerMask)) {
				Debug.DrawLine (thisPosition, other.transform.position, Color.cyan, 10000);

					if (hit.transform.gameObject.tag == tagToCheckAgainst  ) {
                    //CODE TO EXECUTE IF THE PLAYER IS CURRENTLY SEEN BY THIS OBJECT
                        Debug.Log ("YO " + other.gameObject.name + ", YOU BEIN WATCHED BY" + this.name);
                    //timer.reset();
                    //audioSource.PlayOneShot(alertSound, 1.00f);
                } else {
						//WHEN PLAYER IS IN THIS OBJECTS FIELD OF VIEW BUT IS NOT ACTUALLY "SEEN"
						Debug.Log ("YO " + other.gameObject.name + " IS IN " + this.name + "'S FIELD OF VISION. HOWEVER, " + other.gameObject.name + " IS NOT SEEN BECAUSE VISION IS BLOCKED BY " + hit.transform.name);
					}
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == tagToCheckAgainst) {
			//CODE FOR WHEN THE PLAYER HAS INITIALLY ENTERED THIS OBJECTS FIELD OF VIEW
			//Debug.Log (other.name + " HAS ENTERED " + this.gameObject.name + "'S FIELD OF VIEW");
			playerHasBeenSpotted = true;
            audioSource.PlayOneShot(alertSound, 1.00f);
            timer.reset();
        }
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == tagToCheckAgainst) {
			//CODE FOR WHEN THE PLAYER HAS LEFT THIS OBJECTS FIELD OF VIEW
			//Debug.Log (other.name +  " HAS LEFT " + this.gameObject.name + "'S FIELD OF VIEW");
		}
	}

	public bool getSpotted(){
		return playerHasBeenSpotted;
	}

	public void setSpotted(bool spot){
		playerHasBeenSpotted = spot;
	}
}
