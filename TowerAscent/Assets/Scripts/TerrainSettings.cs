using UnityEngine;
using System.Collections;

public class TerrainSettings : MonoBehaviour {
    Terrain terrain;
	// Use this for initialization
	void Start () {
        terrain = Terrain.activeTerrain;
        //terrain.terrainData.SetDetailResolution(496, 8);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
