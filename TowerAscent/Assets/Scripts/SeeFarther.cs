using UnityEngine;
using System.Collections;

public class SeeFarther : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Terrain.activeTerrain.detailObjectDistance = 500;
	}
	
	// Update is called once per frame
	void Update () {
		//print(Terrain.activeTerrain.detailObjectDistance);
	}
}
