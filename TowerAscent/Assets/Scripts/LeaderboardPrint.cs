using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardPrint : MonoBehaviour {
    public Text ranks;
    public Text names;
    public Text times;
    // Use this for initialization
    void Start () {
        ranks.text = "";
        names.text = "";
        times.text = "";

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateRanks(string results) {
        ranks.text = ranks.text + results;

    }
    public void UpdateNames(string results) {
        names.text = names.text + results;

    }
    public void UpdateTimes(string results) {
        times.text = times.text + results;

    }
}
