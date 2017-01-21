using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardPrint : MonoBehaviour {
    public Text LeaderboardText;
    // Use this for initialization
    void Start () {
        LeaderboardText.text = "";
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateLeaderboard(string results) {
        LeaderboardText.text = LeaderboardText.text + results;

    }
}
