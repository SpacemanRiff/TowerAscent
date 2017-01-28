using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TableTurner : MonoBehaviour {

    public GameObject[] cubeSides = new GameObject[4];
    private LinkedList<GameObject> sides = new LinkedList<GameObject>();
    public LevelManager levelManager;

	private float rotationSmoothness = 1f;
	private Quaternion targetRotation;

	private float yScaleWhileFlat = 0.05f;

	private float rotationAmount = 90.0f;

	private float speedOfScaling = 5.0f;
	private GameObject sideToScaleUp;
	private GameObject sideToScaleDown;

    private void Start() {
        sides = new LinkedList<GameObject>(cubeSides);
		InitializeFaces();
		targetRotation = transform.rotation;
    }

	public void InitializeFaces() {
		PutLevelOnFace(levelManager.GetCurrentLevel(), sides.ElementAt(0));
		PutLevelOnFace(levelManager.GetNextLevel(), sides.ElementAt(1));
		PutLevelOnFace(levelManager.GetPreviousLevel(), sides.ElementAt(3));

		sideToScaleUp = sides.ElementAt(0);
	}

    void Update() {
		
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            TurnLeft();
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			TurnRight();
		}
		


    }

    void FixedUpdate() {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * rotationSmoothness * Time.deltaTime);

        if (sideToScaleUp) {
            sideToScaleUp.transform.localScale = Vector3.Lerp(sideToScaleUp.transform.localScale,
                new Vector3(sideToScaleUp.transform.localScale.x, 1, sideToScaleUp.transform.localScale.z),
                speedOfScaling * Time.deltaTime);
        }

        if (sideToScaleDown) {
            sideToScaleDown.transform.localScale = Vector3.Lerp(sideToScaleDown.transform.localScale,
                new Vector3(sideToScaleDown.transform.localScale.x, yScaleWhileFlat, sideToScaleDown.transform.localScale.z),
                speedOfScaling * Time.deltaTime);
        }

    }

    public void TurnLeft() {//Currently that is next level
        levelManager.GoToNextLevel();
        Level nextLevel = levelManager.GetNextLevel();
        PutLevelOnBottom(nextLevel);
        RotateLeft();
		sideToScaleDown = sides.ElementAt(0);
		sideToScaleUp = sides.ElementAt(1);
        sides.AddLast(sides.ElementAt(0));
        sides.RemoveFirst();
        DestroyBottomPrefab();
    }

    public void TurnRight() {//Previous
		levelManager.GoToPreviousLevel();
		Level previousLevel = levelManager.GetPreviousLevel();
		PutLevelOnBottom(previousLevel);
		RotateRight();
		sideToScaleDown = sides.ElementAt(0);
		sideToScaleUp = sides.ElementAt(3);
		sides.AddFirst(sides.ElementAt(sides.Count-1));
		sides.RemoveLast();
		DestroyBottomPrefab();
    }

    private void PutLevelOnBottom(Level bottomLevel) {
        PutLevelOnFace(bottomLevel, sides.ElementAt(2));
    }

    private void PutLevelOnFace(Level level, GameObject face) {
		GameObject newLevel = GameObject.Instantiate(level.GetPrefab, face.transform, false);
		MakePrefabFlatOnFace(face);
    }

	private void MakePrefabFlatOnFace(GameObject side) {
		side.transform.localScale = new Vector3(side.transform.localScale.x, yScaleWhileFlat, side.transform.localScale.z);
	}

    private void RotateLeft() {
		targetRotation *=  Quaternion.AngleAxis(rotationAmount, Vector3.left);
    }

    private void RotateRight() {
		targetRotation *=  Quaternion.AngleAxis(-rotationAmount, Vector3.left);
    }

    private void DestroyBottomPrefab() {
        //Bottom side should always be index 2 of sides
        GameObject bottomPrefab = sides.ElementAt(2).transform.GetChild(0).gameObject;
        GameObject.Destroy(bottomPrefab);
    }
}
