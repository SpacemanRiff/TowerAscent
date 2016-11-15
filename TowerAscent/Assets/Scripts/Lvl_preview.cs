using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Lvl_preview : MonoBehaviour {

	public GameObject[] previews;
	public GameObject current;
	static int i;
	public string[] scene;
	static int timer = 0;
	// Use this for initialization
	void Start () {
		current = previews [i];
		current.SetActive (true);
	}

	// Update is called once per frame
	void Update () {
		if (timer != 0){
			timer--;
		}
	}
	void OnTriggerEnter(Collider other) {

		if (tag == "Next" && other.tag == "VRController" && timer == 0) {
			current = previews [i];
			if (i < 4) {//replace with length of array later.
				i++;
			} else {
				i = 0;
			}
			Debug.Log ("only 1");
			current.SetActive (false);
			current = previews [i];
			current.SetActive (true);
			timer = 15;
		}

		if (tag == "Prev" && other.tag == "VRController" && timer == 0) {
			current = previews [i];
			if (i > 0) {
				i--;
			} else {
				i = 4;//replace with length of array later.
			}
			current.SetActive (false);
			current = previews [i];
			current.SetActive (true);
			timer = 15;
		}

		if (tag == "Play" && other.tag == "VRController" && timer == 0) {
			SceneManager.LoadSceneAsync(scene[i]);
			SceneManager.UnloadScene("MainMenu");
			timer = 15;
		}

	}
}
