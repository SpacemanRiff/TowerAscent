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
    public string[] levelNames;

	// Use this for initialization
	void Start() {
		current = previews[i];
		current.SetActive (true);
        //TODO redo leaderboard previews for main menu
        //reciever.levelName = levelNames[i];
        //reciever.loadLeaderboard();
    }

	// Update is called once per frame
	void Update () {
		if (timer != 0){
			timer --;
		}
	}
	void OnTriggerEnter(Collider other) {

		if (tag == "Next" && other.tag == "VRController" && timer == 0) {
			current = previews [i];
			if (i < 7) {//replace with length of array later.
				i++;
			} else {
				i = 0;
			}
			timer = 30;
			current.SetActive (false);
			current = previews [i];
			current.SetActive (true);
            //reciever.levelName = levelNames[i];
            //reciever.loadLeaderboard();
			timer = 30;
			levelName = scene [i];
			LevelText.text = levelName;
		}

		if (tag == "Prev" && other.tag == "VRController" && timer == 0) {
			current = previews [i];
			if (i > 0) {
				i--;
			} else {
				i = 7;//replace with length of array later.
			}
			timer = 30;
			current.SetActive (false);
			current = previews [i];
			current.SetActive (true);
            //reciever.levelName = levelNames[i];
            //reciever.loadLeaderboard();
            timer = 30;
			levelName = scene [i];
			LevelText.text = levelName;
		}

		if (tag == "Play" && other.tag == "VRController" && timer == 0) {
			SceneManager.LoadSceneAsync(scene[i]);
			SceneManager.UnloadSceneAsync("MainMenu");
			timer = 30;
		}

		//levelName = levelNames [i];
		//LevelText.text = levelName;

	}
}
