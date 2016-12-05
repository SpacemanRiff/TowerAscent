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

	public SteamLeaderboards SteamLB;

    void Start() {
        audioSource = GetComponent<AudioSource>();
		SteamLeaderboards.Init();
		SteamLeaderboards.UpdateScore(10);
    }

    void OnTriggerEnter(Collider other){
        if (other.tag == "VRController") {
            Timer.StopTime();
            audioSource.PlayOneShot(victorySound, 0.15f);
            //send.StartCoroutine(send.send());
            //send.reset();
			//SteamLeaderboards.Init();
			SteamLeaderboards.UpdateScore(0);
			print ("Sent score");
            Timer.FinishLevel();
        }
    }
    
}
