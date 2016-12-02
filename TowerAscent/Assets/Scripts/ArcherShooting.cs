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
	public int quiverSize = 3;
	public float timeBetweenArrows = 1000.0f;
	private float timeLeft;
	private int numberArrowsShot = 0;
    public GameObject arrow, arrowTwo, arrowThree, bow;
	private GameObject arrowCurrent, bowHold;
    private Camera helperCamera;
	private Animation bowAnimation;

	private bool arrowInBow = false;
	private float currentScale;
	private float currentForce;
	private float zOffset = 20.0f;
	private Queue arrowsInGame = new Queue();
	private Transform stringOnBow;
    Vector3 mousePositionWorldSpace, currentMousePosition;

	//public float forwardThing = 0.05f;

    // Use this for initialization
    void Start () {
		currentScale = 1;
		currentForce = 0;
		timeLeft = 0.0f;
		helperCamera = GameObject.FindGameObjectWithTag("HelperCamera").GetComponent<Camera>();
        bowHold = (GameObject)Instantiate(
                bow,
                helperCamera.transform.position
                + helperCamera.transform.forward * 2
                + helperCamera.transform.right,
                transform.rotation);
		bowHold.transform.position = helperCamera.transform.position
			+ helperCamera.transform.forward * 2
			+ helperCamera.transform.right;
		bowHold.transform.SetParent(helperCamera.transform);
		bowAnimation = bowHold.GetComponent<Animation>();
		stringOnBow = GameObject.FindGameObjectWithTag("String").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
		mousePositionWorldSpace = helperCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zOffset));
        bowHold.transform.LookAt(mousePositionWorldSpace);
		if(timeLeft >= 0) {
			timeLeft -= Time.deltaTime;
		}

		if(!IsQuiverEmpty()) {

			if(arrowInBow) {

				if(Input.GetMouseButton(0)) {
					DrawBow();
				}
				if(Input.GetMouseButtonUp(0)) {
					ShootArrow();
					timeLeft = timeBetweenArrows;
				}

			} else {
				if(timeLeft <= 0) {
					NockArrow();
				}
			}



		}
	}

	private void DrawBow() {

		if(currentScale > scaleMinimum) {
			currentScale = currentScale - scalingSpeed;
			currentForce = currentForce + (1 - currentScale) * maxForce;
			if(!bowAnimation.isPlaying) {
				bowAnimation["DrawBow"].speed = 1.2f;
				bowAnimation.Play();
			}
		} else {
			bowAnimation.Stop();
		}
	}

	private bool IsQuiverEmpty() {
		return numberArrowsShot >= quiverSize;
	}

	private void NockArrow() {
		arrowCurrent = (GameObject)Instantiate(
			arrow,
			bowHold.transform.position + bowHold.transform.forward * 0.29f + bowHold.transform.right * 0.01f,
			bowHold.transform.rotation,
			stringOnBow);
		arrowCurrent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
		arrowCurrent.GetComponent<Rigidbody>().useGravity = false;
		arrowsInGame.Enqueue(arrowCurrent);
		arrowInBow = true;
	}

	private void ShootArrow() {
		arrowCurrent.GetComponent<ArrowScript>().shoot();
		arrowCurrent.GetComponent<Rigidbody>().velocity = arrowCurrent.transform.forward * arrowSpeed * currentForce;
		bowAnimation.Play();
		bowAnimation["DrawBow"].speed = -2.0f;

		currentScale = 1;
		currentForce = 1;
		arrowInBow = false;
		numberArrowsShot++;
	}

	void OnGUI() {
		float xMin = (Input.mousePosition.x) - ((crosshairMaxSize * currentScale) / 2);
		float yMin = (Screen.height - Input.mousePosition.y) - ((crosshairMaxSize * currentScale) / 2);
		GUI.DrawTexture(new Rect(xMin, yMin, crosshairMaxSize * currentScale, crosshairMaxSize * currentScale), crosshairImage);
	}
}
