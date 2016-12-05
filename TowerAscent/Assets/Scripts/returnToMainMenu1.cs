using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class returnToMainMenu1 : MonoBehaviour {
    public string CurrentScene;
	public int t = 0;
    // Use this for initialization
    void Start () {
        CurrentScene = SceneManager.GetActiveScene().name;
    }
	
	// Update is called once per frame
	void Update () {
		if (t == 0) {
			SceneManager.LoadSceneAsync ("MainMenu");
			SceneManager.UnloadScene (CurrentScene);
			print ("stuff");
			//t += 1;
		}
		t += 1;
		gameObject.SetActive (false);
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "VRController")
        {
            //Debug.Log("Changing Scene To Main Menu!");
            //SceneManager.UnloadScene(CurrentScene);
            
        }
    }
}
