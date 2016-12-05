using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Steamworks;

public class PlayerName : MonoBehaviour {

    [Header("UI Components")]

    public Text NameText;

    public static string playerName = "";

    // Use this for initialization
    void Start () {
        if (!SteamManager.Initialized) {
            return;
        }

        NameText.text = SteamFriends.GetPersonaName();
		playerName = NameText.text;
	}

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public string getName() {
        return playerName;
    }

    // Update is called once per frame
    /*void Update () {
        if(SceneManager.GetActiveScene().name =="MainMenu")
            NameText.text = playerName;
    }*/
    

    void OnGUI()
    {
        //playerName = GUI.TextField(new Rect(25, 25, 100, 30), playerName,25);
    }

}
