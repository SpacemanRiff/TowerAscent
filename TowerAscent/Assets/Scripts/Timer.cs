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
            TimerText.text = score.ToString("F3") + " s";

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
        StopTime();

		//FIO = GameObject.FindGameObjectWithTag ("Ghost").GetComponent<FileInputOutput> ();
		//FIO.PlayerJustReset = true;

        time = 0.0f;
        rig.transform.position = rigPos;
        
		GameObject helper = GameObject.FindGameObjectWithTag ("Helper");
		helper.GetComponent<ArcherShooting> ().DestroyAllArrows ();
        
        finished = false;
        
    }

    public void FinishLevel() {
        finished = true;
    }
    public int getScore() {
        print("Score: " + score);
        print((decimal)score/upperLimit);
        print((1 - ((decimal)score / upperLimit)));
        print((1 - ((decimal)score / upperLimit)) * 100 * multiplier);
        scoreBeforeRound = ((1 - ((decimal)score / upperLimit)) * 100 * multiplier);
        print(scoreBeforeRound);
        return (int)scoreBeforeRound;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Climbable")
        {
            StartTime();
        }
    }
}
