using UnityEngine;
using System.Collections;

using System.Net;
using System.Text;
using System.IO;
using UnityEngine.Networking;


public class StopTimeAndWinTest : MonoBehaviour {
    public Timer Timer;
    public lbReceive receive;
    public lbSend send;
    public GameObject leaderboardManager;
    public AudioClip victorySound;
    private AudioSource audioSource;
    public GameObject lbuploader;
	public SteamLeaderboards SteamLB;

    void Start() {
        SteamLeaderboards.Init();
        //SteamLB.setLeaderBoardName("ATEST");
        audioSource = GetComponent<AudioSource>();
		//SteamLeaderboards.Init();
        //SteamLeaderboards.UpdateScore(51651);
       // SteamLeaderboards.UpdateScore(100);

    }

    void OnTriggerEnter(Collider other){
        if (other.tag == "VRController") {
            //SteamLeaderboards.Init();
            //SteamLeaderboards.UpdateScore(645);
            Timer.StopTime();
            audioSource.PlayOneShot(victorySound, 0.15f);
            //send.StartCoroutine(send.send());
            //send.reset();
            if (GameObject.FindGameObjectWithTag("LBUploader") == null) {
                Instantiate(lbuploader, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            }
			print ("Sent score");
            Timer.FinishLevel();
        }
    }
    
}
