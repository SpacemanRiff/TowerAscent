using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lvl_preview : MonoBehaviour {

	public GameObject[] previews;
	public GameObject current;
	static int i = 0;
	public string[] scene;
	static int timer = 0;
	public Text LevelText;
	public static string levelName = "";
    public lbReceive reciever;
    public string[] levelNames;

	// Use this for initialization
	void Start() {
		current = previews[i];
		current.SetActive (true);
        reciever.levelName = levelNames[i];
        reciever.loadLeaderboard();
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
			
			current.SetActive (false);
			current = previews [i];
			current.SetActive (true);
            reciever.levelName = levelNames[i];
            reciever.loadLeaderboard();
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
            reciever.levelName = levelNames[i];
            reciever.loadLeaderboard();
            timer = 15;
		}

		if (tag == "Play" && other.tag == "VRController" && timer == 0) {
			SceneManager.LoadSceneAsync(scene[i]);
			SceneManager.UnloadScene("MainMenu");
			timer = 15;
		}

		levelName = scene [i];
		LevelText.text = levelName;

	}
}
