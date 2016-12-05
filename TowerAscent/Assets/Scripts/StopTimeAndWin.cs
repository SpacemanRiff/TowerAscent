using UnityEngine;
using System.Collections;

using System.Net;
using System.Text;
using System.IO;
using UnityEngine.Networking;


public class StopTimeAndWin : MonoBehaviour {
    public Timer Timer;
    public lbReceive receive;
    public lbSend send;
    public GameObject leaderboardManager;
    public AudioClip victorySound;
    private AudioSource audioSource;


    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other){
        if (other.tag == "VRController") {
            Timer.StopTime();
            audioSource.PlayOneShot(victorySound, 0.15f);
            //send.StartCoroutine(send.send());
            send.reset();
            Timer.FinishLevel();
        }
    }
    
}
