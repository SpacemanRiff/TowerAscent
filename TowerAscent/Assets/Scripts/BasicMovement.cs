using UnityEngine;
using System.Collections;

public class BasicMovement : MonoBehaviour {

    //public GameObject player;
    public Rigidbody playerRigidBody;
    public float offset = 50000.0f;

	// Use this for initialization
	void Start () {
        playerRigidBody = playerRigidBody.GetComponent<Rigidbody>();
        if (Display.displays.Length > 1)
            Display.displays[1].Activate();

    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.W)){
            //player.transform.position += offset;
            //playerRigidBody.GetComponent<Rigidbody>();
            playerRigidBody.AddForce(transform.up * offset);
        }
	
	}
}
