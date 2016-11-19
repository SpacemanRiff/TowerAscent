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
    public GameObject arrow, arrowTwo, arrowThree, bow;
    private GameObject arrowShot, bowHold;
    private Camera helperCamera;

	private float currentScale;
	private float currentForce;
	private float zOffset = 20.0f;
	private Queue arrowsInGame = new Queue();
    Vector3 placeToGo, currentMousePosition;

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
        bowHold.transform.position = helperCamera.transform.position
            + helperCamera.transform.forward * 2
            + helperCamera.transform.right;
        bowHold.transform.LookAt(placeToGo);

        if (Input.GetMouseButtonDown(0)) {
            arrowShot = (GameObject)Instantiate(
                arrow,
                helperCamera.transform.position 
                + helperCamera.transform.forward * 2 
                + helperCamera.transform.right,
                transform.rotation);
			arrowsInGame.Enqueue(arrowShot);
            currentMousePosition = Input.mousePosition;
            placeToGo = helperCamera.ScreenToWorldPoint(new Vector3(currentMousePosition.x, currentMousePosition.y, zOffset));
            arrowShot.transform.LookAt(placeToGo);
        }
		if(Input.GetMouseButton(0)) {
			if(currentScale > scaleMinimum) {
				currentScale = currentScale - scalingSpeed;
				currentForce = currentForce + (1 - currentScale) * maxForce;
			}
            arrowShot.transform.position = helperCamera.transform.position 
                + helperCamera.transform.forward * 2
                + helperCamera.transform.right;
            
            currentMousePosition = Input.mousePosition;
            placeToGo = helperCamera.ScreenToWorldPoint(new Vector3(currentMousePosition.x, currentMousePosition.y, zOffset));
            arrowShot.transform.LookAt(placeToGo);
        }
		if(Input.GetMouseButtonUp(0)) {
			if(arrowsInGame.Count > maxNumberOfArrows) {
				GameObject arrowToDestroy = (GameObject)arrowsInGame.Dequeue();
				GameObject.Destroy(arrowToDestroy);
			}
            currentMousePosition = Input.mousePosition;
            placeToGo = helperCamera.ScreenToWorldPoint(new Vector3(currentMousePosition.x, currentMousePosition.y, zOffset));
            arrowShot.transform.LookAt(placeToGo);
			arrowShot.GetComponent<Rigidbody>().AddForce(arrowShot.transform.forward * arrowSpeed * currentForce, ForceMode.Impulse);
			currentScale = 1;
			currentForce = 1;
		}
	}

	void OnGUI() {
		float xMin = (Input.mousePosition.x) - ((crosshairMaxSize * currentScale) / 2);
		float yMin = (Screen.height - Input.mousePosition.y) - ((crosshairMaxSize * currentScale) / 2);
		GUI.DrawTexture(new Rect(xMin, yMin, crosshairMaxSize * currentScale, crosshairMaxSize * currentScale), crosshairImage);
	}
}
