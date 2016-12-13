using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public Transform[] placesToGo;
	public float speed;

	private int currentTarget;
	private float distanceToTurnAroundAt = 1;
	private int numberOfPlaces;

	// Use this for initialization
	void Start () {
		numberOfPlaces = placesToGo.Length;
		currentTarget = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (IShouldSwitchTargets()) {
			if (currentTarget == numberOfPlaces - 1) {
				currentTarget = 0;
			} else {
				currentTarget++;
			}
		} else {
			MoveTowardsTheTarget();
		}
	}

	private void MoveTowardsTheTarget() {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, placesToGo[currentTarget].position, step);
	}

	private bool IShouldSwitchTargets() {
		return Vector3.Distance(transform.position, placesToGo[currentTarget].position) < distanceToTurnAroundAt;
	}


}
