using UnityEngine;
using System.Collections;

public class ArcherShooting : MonoBehaviour {

	public Texture2D crosshairImage;
	public const float crosshairMaxSize = 100.0f;
	public const float crosshairMinSize = 25.0f;
	public float scalingSpeed = 0.01f;
	public float scaleMinimum = 0.7f;
	public GameObject arrow;

	private float currentScale;

	// Use this for initialization
	void Start () {
		currentScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)) {
			if(currentScale > scaleMinimum) {
				currentScale = currentScale - scalingSpeed;
			}
		}
		if(Input.GetMouseButtonUp(0)) {
			currentScale = 1;
		}
	}

	void OnGUI() {
		float xMin = (Input.mousePosition.x) - ((crosshairMaxSize * currentScale) / 2);
		float yMin = (Screen.height - Input.mousePosition.y) - ((crosshairMaxSize * currentScale) / 2);
		GUI.DrawTexture(new Rect(xMin, yMin, crosshairMaxSize * currentScale, crosshairMaxSize * currentScale), crosshairImage);
	}
}
