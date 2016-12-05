using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using VRTK;

public class Timer : MonoBehaviour {
    
    public float time = 0.0f;
    public int timeMinute =0 , timeSecond = 0;
    public Text TimerText = null;
	public bool stopped = true;
    bool finished;
    public GameObject rig;
    public GameObject lightArray;
    private Vector3 rigPos;
    public string timeyTime, strMin, strSec, strMilSec;
    public HandMovementLeft LeftHand;
    public HandMovementRight RightHand;
    public VRTK_InteractGrab LeftHandGrasp, RightHandGrasp;
    //public FileInputOutput FIO;

    void Start () {
        rigPos = rig.transform.position;
        stopped = true;
        finished = false;
        time = 0.0f;
        timeMinute = 0;
        timeSecond = 0;
	}
	
	
	void Update () {


        if (!stopped && !finished){
            time += Time.deltaTime;
            TimerText.text = "Timer: " + time.ToString("F2");
            if (time >= .6) {
                timeSecond++;
                time = 0;
            }
            if (timeSecond >= 60) {
                timeSecond = 0;
                timeMinute++;
            }

            TimerText.text = "Timer: " + strMin + ":" + strSec + ":" + strMilSec;

        }else if(stopped && timeSecond < 1 && timeMinute < 1 && !finished)
        {
            TimerText.text = "Timer: " + "0" + ":0" + "0" + ":" + "00";
        }

        getTime();
        
    }
    public void StopTime() {
        stopped = true;
    }

    public void StartTime() {
        stopped = false;
    }

    public void reset() {
        Debug.Log("RESET");
        if(LeftHand.gripButtonPressed || RightHand.gripButtonPressed) {
            RightHandGrasp.ForceRelease();
            LeftHandGrasp.ForceRelease();
        }
        StopTime();

		//FIO = GameObject.FindGameObjectWithTag ("Ghost").GetComponent<FileInputOutput> ();
		//FIO.PlayerJustReset = true;

        time = 0.0f;
        timeMinute = 0;
        timeSecond = 0;
        //RightHand.ForceRelease();
        //LeftHand.ForceRelease();
        rig.transform.position = rigPos;
		timeyTime = "00:00:00";
        
        
        finished = false;
        
    }

    public void FinishLevel() {
        finished = true;
        
    }
    public void getTime() {
        
        //Debug.Log(timeMinute);
        //Debug.Log(timeSecond);
        if (timeMinute < 10)
        {
            strMin = "0" + timeMinute;
        }
        else {
            strMin = "" + timeMinute; 
        }

        if (timeSecond < 10)
        {
            strSec = "0" + timeSecond;
        }
        else
        {
            strSec = "" + timeSecond;
        }

        if (time < .10)
        {
            strMilSec = "0" + Mathf.Round((time * 100)).ToString();
        }
        else
        {
            strMilSec = "" + Mathf.Round((time * 100)).ToString();
        }
        timeyTime = strMin + ":" + strSec + ":" + strMilSec;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Climbable")
        {
            StartTime();
        }
    }
}
