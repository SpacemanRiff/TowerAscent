using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level {

	private string levelName;
	private GameObject prefab;
	private string sceneName;
	private string leaderboardName;

	public Level(GameObject prefab) {
		this.prefab = prefab;
		this.levelName = ConvertPrefabNameToLevelName(prefab.name);
		this.sceneName = ConvertPrefabNameToSceneName(prefab.name);
		this.leaderboardName = ConvertPrefabNameToLeaderboardName(prefab.name);
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

	private string ConvertPrefabNameToLevelName(string prefabName) {
		return prefabName;
	}
	private string ConvertPrefabNameToSceneName(string prefabName) {
		return prefabName;
	}
	private string ConvertPrefabNameToLeaderboardName(string prefabName) {
		return prefabName;
	}

}
