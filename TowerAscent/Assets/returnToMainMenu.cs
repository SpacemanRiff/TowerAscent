using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class returnToMainMenu : MonoBehaviour {
    public string CurrentScene;
    // Use this for initialization
    void Start () {
        CurrentScene = SceneManager.GetActiveScene().name;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "VRController")
        {
            //Debug.Log("Changing Scene To Main Menu!");
            //SceneManager.UnloadScene(CurrentScene);
            SceneManager.LoadSceneAsync("MainMenu");
            SceneManager.UnloadScene(CurrentScene);
        }
    }
}
