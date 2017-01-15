using UnityEngine;
using System.Collections;

using System.Net;
using System.Text;
using System.IO;
using UnityEngine.Networking;


public class StopTimeAndWinTest : MonoBehaviour {
    public Timer Timer;
    public GameObject leaderboardManager;
    public AudioClip victorySound;
    private AudioSource audioSource;

    void Start() {
        SteamLeaderboards.Init();
        //Set leaderboard name here
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other){
        if (other.tag == "VRController") {
            Timer.StopTime();
            print(Timer.getScore());
            SteamLeaderboards.UpdateScore(Timer.getScore());
            audioSource.PlayOneShot(victorySound, 0.15f);
			print ("Sent score");
            Timer.FinishLevel();
        }
    }
}
