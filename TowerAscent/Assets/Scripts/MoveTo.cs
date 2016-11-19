using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NavMeshAgent))]
[RequireComponent (typeof (Animator))]
public class MoveTo : MonoBehaviour {

	[SerializeField]
	Transform[] navPoints;

	[SerializeField]
	NavMeshAgent agent;

	public GameObject player;
	public int destPoint;
	private Animator animator;
	public YOYOUBEENSPOTTED spotScript;
	private SceneManagement manager;
	private Fading fader;
	private bool huntingPlayer;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
        if (navPoints.Length > 0)
        {
            agent.destination = navPoints [destPoint].position; //Go to the first position without waiting.
        }
		animator = GetComponent<Animator>();
		animator.SetBool ("Moving", true);
		huntingPlayer = false;
		manager = GetComponent<SceneManagement> ();
		fader = GetComponent<Fading> ();
	}

	private void MoveToNextPoint(){
        if (navPoints.Length > 0)
        {
            agent.destination = navPoints[destPoint].position;
		    destPoint = (destPoint + 1) % navPoints.Length;
        }
        
	}

	//Stop the patrol route and go look for the player.
	private IEnumerator playerSpotted(){
		Vector3 playerPos = player.transform.position;
		spotScript.setSpotted (false);
		huntingPlayer = true;
		startPointingAnimation ();
		yield return new WaitForSeconds (1);
		fader.BeginFade (1);
		yield return new WaitForSeconds(1);
		manager.ResetLevel ();
	}

	void startIdleAnimation(){
		agent.Stop ();
		animator.SetBool ("Moving", false);
	}

	private void startWalkingAnimation(){
		agent.Resume ();
		animator.SetBool ("Moving", true);
	}

	private void startPointingAnimation(){
		agent.Stop ();
		animator.SetBool ("PlayerSpotted", true);
	}

	void Update(){
		if (spotScript.getSpotted() && !huntingPlayer) {
			StartCoroutine( playerSpotted () );
		}

		if (agent.remainingDistance <= .5f && !huntingPlayer) {
			MoveToNextPoint();
		}
		if (huntingPlayer) {
			if (agent.remainingDistance <= .5f) {
				huntingPlayer = false;
                if (navPoints.Length > 0)
                {
                    agent.destination = navPoints [destPoint].position;
                }
				
			}
		}
	}
}