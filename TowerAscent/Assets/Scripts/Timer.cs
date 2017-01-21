using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using VRTK;
using System.Timers;
using System;

public class Timer : MonoBehaviour {
    
    public float time = 0.0f;
    public float score;
    public decimal upperLimit = 180; //total time in s  = 3min
    public decimal multiplier = 1000;
    public decimal scoreBeforeRound;
    public Text TimerText = null;
	public bool stopped = true;
    bool finished;
    public GameObject rig;
    private Vector3 rigPos;
    public HandMovementLeft LeftHand;
    public HandMovementRight RightHand;
    public VRTK_InteractGrab LeftHandGrasp, RightHandGrasp;
	public float min = 0;
	public string zeroPlace = "0";

    //public FileInputOutput FIO;

    void Start () {
        StartCoroutine(StartTimer());
        rigPos = rig.transform.position;
        stopped = true;
        finished = false;
        time = 0.0f;
	}

    IEnumerator StartTimer() {
        float tempTime = 0;
        while (stopped) {
            tempTime = Time.time;
            yield return new WaitForEndOfFrame();
        }
        while (!stopped) {
            yield return new WaitForSeconds(.05f);
            score = (Time.time - tempTime);
			if (score / 60 >= 1 + min) {
				min++;
			}
			if ((score - (min * 60)) < 10) {
				zeroPlace = "0";
			} 
			else {
				zeroPlace = "";
			}

			//min = Mathf.RoundToInt(score / 120);
			

			TimerText.text = ((min+":"+(zeroPlace+(score-(min*60)).ToString("F2"))).Replace(".",":"));

        }

    }

    void Update () {
        //getScore();
        if (stopped) {
            TimerText.text = "0";
        }
    }
    public void StopTime() {
        stopped = true;
        StartCoroutine(StartTimer());
    }

    public void StartTime() {
        stopped = false;
    }

    public void reset() {
        //Debug.Log("RESET");
        if(LeftHand.gripButtonPressed || RightHand.gripButtonPressed) {
            RightHandGrasp.ForceRelease();
            LeftHandGrasp.ForceRelease();
        }
        //StopTime(); no longer stops time for checkpoint

		//FIO = GameObject.FindGameObjectWithTag ("Ghost").GetComponent<FileInputOutput> ();
		//FIO.PlayerJustReset = true;

		//time = 0.0f; no longer stops time for checkpoint
        rig.transform.position = rigPos;
        
		GameObject helper = GameObject.FindGameObjectWithTag ("Helper");
		helper.GetComponent<ArcherShooting> ().DestroyAllArrows ();
        
        finished = false;
        
    }

    public void FinishLevel() {
        finished = true;
    }
    public int getScore() {
        
		return ((int)(score * 1000));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Climbable")
        {
            StartTime();
        }
    }

	public void updateCameraRigPosition(Vector3 newPos){
		rigPos = newPos;
	
	}
}
