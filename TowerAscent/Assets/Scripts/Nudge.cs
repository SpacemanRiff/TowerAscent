using UnityEngine;
using System.Collections;
using VRTK;

public class Nudge : MonoBehaviour {
	public GameObject rig;
	public Timer Timer;
	// Use this for initialization
	void Start () {
		GetComponent<VRTK_ControllerEvents>().TouchpadPressed += new ControllerInteractionEventHandler(nudgeForward);
		GetComponent<VRTK_ControllerEvents>().TouchpadReleased += new ControllerInteractionEventHandler(empty);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void nudgeForward(object sender, ControllerInteractionEventArgs e)
	{
		if (Timer.stopped == true && rig.transform.position.z > -10 && rig.transform.position.z < 10) {
            rig.transform.position += transform.forward; //new Vector3 (0, 0, 0.1f);
		}
	}
	private void empty(object sender, ControllerInteractionEventArgs e)
	{
		
	}


}
