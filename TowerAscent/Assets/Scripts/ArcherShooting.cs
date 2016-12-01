using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArcherShooting : MonoBehaviour {

	public Texture2D crosshairImage;
	public const float crosshairMaxSize = 50.0f;
	public const float crosshairMinSize = 0.1f;
	public float scalingSpeed = 0.01f;
	public float maxForce = 2.0f;
	public float scaleMinimum = 0.7f;
	public float arrowSpeed = 10.0f;
	public int maxNumberOfArrows = 4, maxNumberOfTracers = 4;
	public int quiverSize = 3;
	private int numberArrowsShot = 0;
    public GameObject arrow, arrowTwo, arrowThree, bow;
    private GameObject arrowShot, bowHold;
    private Camera helperCamera;

	private bool arrowInBow = false;
	private float currentScale;
	private float currentForce;
	private float zOffset = 20.0f;
	private Queue arrowsInGame = new Queue();
    Vector3 placeToLook, currentMousePosition;

    // Use this for initialization
    void Start () {
		currentScale = 1;
		currentForce = 0;
        helperCamera = GameObject.FindGameObjectWithTag("HelperCamera").GetComponent<Camera>();
        bowHold = (GameObject)Instantiate(
                bow,
                helperCamera.transform.position
                + helperCamera.transform.forward * 2
                + helperCamera.transform.right,
                transform.rotation);
    }
	
	// Update is called once per frame
	void Update () {
		placeToLook = helperCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zOffset));

        bowHold.transform.position = helperCamera.transform.position
            + helperCamera.transform.forward * 2
            + helperCamera.transform.right;
		
        bowHold.transform.LookAt(placeToLook);

		if(arrowInBow) {

			arrowShot.transform.LookAt(placeToLook);

		} else if(numberArrowsShot < quiverSize) {
			arrowShot = (GameObject)Instantiate(
				arrow,
				helperCamera.transform.position 
				+ helperCamera.transform.forward * 2 
				+ helperCamera.transform.right,
				transform.rotation);
			arrowsInGame.Enqueue(arrowShot);
			arrowInBow = true;
			numberArrowsShot++;
		}

		if(Input.GetMouseButton(0)) {
			if(currentScale > scaleMinimum) {
				currentScale = currentScale - scalingSpeed;
				currentForce = currentForce + (1 - currentScale) * maxForce;
			}
        }
		if(Input.GetMouseButtonUp(0)) {
            arrowShot.GetComponent<Rigidbody>().velocity = arrowShot.transform.forward * arrowSpeed * currentForce;
			arrowShot.GetComponent<ArrowScript>().shoot();
            currentScale = 1;
			currentForce = 1;
			arrowInBow = false;
		}
	}

	void OnGUI() {
		float xMin = (Input.mousePosition.x) - ((crosshairMaxSize * currentScale) / 2);
		float yMin = (Screen.height - Input.mousePosition.y) - ((crosshairMaxSize * currentScale) / 2);
		GUI.DrawTexture(new Rect(xMin, yMin, crosshairMaxSize * currentScale, crosshairMaxSize * currentScale), crosshairImage);
	}
}
