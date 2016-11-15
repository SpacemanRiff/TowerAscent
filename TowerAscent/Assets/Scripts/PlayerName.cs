using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerName : MonoBehaviour {


    public Text NameText;

    public static string playerName = "";

    // Use this for initialization
    void Start () {
	}
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public string getName() {
        return playerName;
    }

    // Update is called once per frame
    void Update () {
        if(SceneManager.GetActiveScene().name =="MainMenu" && Input.anyKeyDown)
            NameText.text = playerName;
    }
    

    void OnGUI()
    {
        playerName = GUI.TextField(new Rect(25, 25, 100, 30), playerName,25);
    }

}
