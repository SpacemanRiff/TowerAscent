using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UploadLeaderboard : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SteamLeaderboards.UpdateScore(10);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
