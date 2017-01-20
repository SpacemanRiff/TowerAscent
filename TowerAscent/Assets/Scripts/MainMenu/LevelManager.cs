using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManager : MonoBehaviour {

    private Dictionary<string, Level> levelMap = new Dictionary<string, Level>();//String = levelName
    private int currentLevel;
    private int previousLevel;
    private int nextLevel;
    public GameObject[] levelPrefabsInOrder;
    private List<string> levelNames = new List<string>();

	// Use this for initialization
	void Start () {
        InitializeLevelData();
        currentLevel = 0;
        previousLevel = levelNames.Count - 1;
        nextLevel = 1;//Admittedly will break if only 1 level. Just don't do that then :)
	}

    private void InitializeLevelData() {
        foreach (GameObject level in levelPrefabsInOrder) {
            Level i = new Level(level);
            levelMap.Add(i.GetLevelName, i);
            levelNames.Add(i.GetLevelName);
        }
    }

    public GameObject GetPrefab(string name) {
        return levelMap[name].GetPrefab;
    }

    public Level GetLevel(string name) {
        return levelMap[name];
    }

    public string GetSceneName(string name) {
        return levelMap[name].GetSceneName;
    }

    public string GetLeaderboardName(string name) {
        return levelMap[name].GetLeaderboardName;
    }

    public Level GetCurrentLevel() {
        return levelMap[levelNames[currentLevel]];
    }

    public Level GetPreviousLevel() {
        return levelMap[levelNames[previousLevel]];
    }

    public Level GetNextLevel() {
        return levelMap[levelNames[nextLevel]];
    }

    public void GoToNextLevel() {
        int lastLevel = levelNames.Count-1;
        if (currentLevel == 0) {
            currentLevel++;
            previousLevel = 0;
            nextLevel++;//Okay JK this will break if you have less than 3 levels. oops. fix it your damn self
        } else if (currentLevel == lastLevel-1) {
            currentLevel++;
            previousLevel++;
            nextLevel = 0;
        } else if (currentLevel == lastLevel) {
            currentLevel = 0;
            nextLevel++;
            previousLevel++;
        } else {
            currentLevel++;
            previousLevel++;
            nextLevel++;
        }
    }

    public void GoToPreviousLevel() {
        int lastLevel = levelNames.Count-1;
        if (currentLevel == 0) {
            currentLevel = lastLevel;
            previousLevel--;
            nextLevel--;
        } else if (currentLevel == 1) {
            previousLevel = lastLevel;
            currentLevel--;
            nextLevel--;
        } else if (currentLevel == lastLevel) {
            nextLevel = lastLevel;
            currentLevel--;
            previousLevel--;
        } else {
            currentLevel--;
            previousLevel--;
            nextLevel--;
        }
    }
}
