using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TableTurner : MonoBehaviour {

    public GameObject[] cubeSides = new GameObject[4];
    private LinkedList<GameObject> sides = new LinkedList<GameObject>();
    public LevelManager levelManager;

    private void Start() {
        sides = new LinkedList<GameObject>(cubeSides);
        StartCoroutine(InitializeFaces());
    }

    IEnumerator InitializeFaces() {
        yield return new WaitForSeconds(2);
        PutLevelOnFace(levelManager.GetCurrentLevel(), sides.ElementAt(0));
        PutLevelOnFace(levelManager.GetNextLevel(), sides.ElementAt(1));
        PutLevelOnFace(levelManager.GetPreviousLevel(), sides.ElementAt(3));
    }

    void Update() {
        if (Input.GetKeyDown("space")) {
            TurnLeft();
        }

    }

    private void TurnLeft() {//Currently that is next level
        levelManager.GoToNextLevel();
        Level nextLevel = levelManager.GetNextLevel();
        PutLevelOnBottom(nextLevel);
        RotateLeft();
        sides.AddLast(sides.ElementAt(0));
        sides.RemoveFirst();
        DestroyBottomPrefab();
    }

    private void TurnRight() {//Previous

    }

    private void PutLevelOnBottom(Level bottomLevel) {
        PutLevelOnFace(bottomLevel, sides.ElementAt(2));
    }

    private void PutLevelOnFace(Level level, GameObject face) {
        GameObject.Instantiate(level.GetPrefab, face.transform, false);
    }

    private void RotateLeft() {
        transform.Rotate(-90, 0, 0);
    }

    private void RotateRight() {
        transform.Rotate(90, 0, 0);
    }

    private void DestroyBottomPrefab() {
        //Bottom side should always be index 2 of sides
        GameObject bottomPrefab = sides.ElementAt(2).transform.GetChild(0).gameObject;
        GameObject.Destroy(bottomPrefab);
    }
}
