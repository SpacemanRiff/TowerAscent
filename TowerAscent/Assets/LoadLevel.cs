using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour {
    public SteamVR_LoadLevel VRLoadLevel;
    public string levelName;

	// Use this for initialization
	void Start () {
        VRLoadLevel.levelName = levelName;
    }

    void onTriggerEnter(Collider other) {
        if (other.tag == "VRController") {
            VRLoadLevel.Trigger();
        }
    }
}
