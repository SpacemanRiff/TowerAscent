using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level {

	private string levelName;
	private GameObject prefab;
	private string sceneName;
	private string leaderboardName;
	private Vector3 prefabDefaultScale;

	public Level(GameObject prefab) {
		this.prefab = prefab;
		this.levelName = ConvertPrefabNameToLevelName(prefab.name);
		this.sceneName = ConvertPrefabNameToSceneName(prefab.name);
		this.leaderboardName = ConvertPrefabNameToLeaderboardName(prefab.name);
		this.prefabDefaultScale = prefab.transform.localScale;
	}

	public string GetLevelName {
		get {
			return levelName;
		}
	}

	public string GetSceneName {
		get {
			return sceneName;
		}
	}

	public string GetLeaderboardName {
		get {
			return leaderboardName;
		}
	}

    public GameObject GetPrefab {
        get {
            return prefab;
        }
    }

	public Vector3 GetPrefabDefaultScale {
		get {
			return prefabDefaultScale;
		}
	}

	private string ConvertPrefabNameToLevelName(string prefabName) {
        //Level_1_Prefab
        prefabName = RemoveEndOfPrefabString(prefabName);
        //Level_1
        prefabName = prefabName.Replace('_', ' ');
        //Level 1
		return prefabName;
	}
	private string ConvertPrefabNameToSceneName(string prefabName) {
        prefabName = RemoveEndOfPrefabString(prefabName);
        prefabName += "_Scene";
        return prefabName;
	}
	private string ConvertPrefabNameToLeaderboardName(string prefabName) {
        prefabName = RemoveEndOfPrefabString(prefabName);
        prefabName += "_LB";
		return prefabName;
	}

    private string RemoveEndOfPrefabString(string prefabName) {
        return prefabName.Remove(prefabName.Length - 7);//7 could be wrong. need to test
    }

}
