using UnityEngine;
using System.Collections;

using System.Net;
using System.Text;
using System.IO;
using UnityEngine.Networking;
using Steamworks;


public class StopTimeAndWinTest : MonoBehaviour {
    public Timer Timer;
    //public GameObject leaderboardManager;
    public AudioClip victorySound;
    private AudioSource audioSource;
    private CallResult<LeaderboardScoresDownloaded_t> LeaderboardScoresDownloaded;
    private SteamLeaderboardEntries_t m_SteamLeaderboardEntries;

    void OnEnable() {
        LeaderboardScoresDownloaded = CallResult<LeaderboardScoresDownloaded_t>.Create(OnLeaderboardScoresDownloaded);
    }

    void Start() {
        SteamLeaderboards.Init();
        //Set leaderboard name here
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (Input.GetKeyDown("space")) {
            GetLeaderBoardThings();
        }
    }

    void OnTriggerEnter(Collider other){
		//LeaderboardEntry_t leaderboardEntry;
		//SteamLeaderboardEntries_t testEntry;
		//LeaderboardScoresDownloaded_t fuck = new LeaderboardScoresDownloaded_t();



		int[] details = {0,0,0};
        if (other.tag == "VRController") {
            Timer.StopTime();
            print(Timer.getScore());
            SteamLeaderboards.UpdateScore(Timer.getScore());
            SteamAPICall_t handle = SteamUserStats.DownloadLeaderboardEntries(SteamLeaderboards.s_currentLeaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, 1, 5);
            LeaderboardScoresDownloaded.Set(handle);
            print("DownloadLeaderboardEntries(" + SteamLeaderboards.s_currentLeaderboard + ", ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, 1, 5) - " + handle);




            //for( int index = 0; index < pDownloadedScores.m_cEntryCount; index++ )
            //SteamUserStats.DownloadLeaderboardEntries(SteamLeaderboards.s_currentLeaderboard,ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, 1, 5);
            //SteamAPICall_t handle = SteamUserStats.DownloadLeaderboardEntries(SteamLeaderboards.s_currentLeaderboard,ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser,-1,1);
            //LeaderboardScoresDownloaded_
            //for (int index = 0; index < 2; index++) {
            //fuck = (LeaderboardScoresDownloaded_t)SteamUserStats.DownloadLeaderboardEntries(SteamLeaderboards.s_currentLeaderboard,ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal,1,1);
            //testEntry.m_SteamLeaderboardEntries = (ulong)SteamUserStats.DownloadLeaderboardEntries(SteamLeaderboards.s_currentLeaderboard,ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal,1,1);
            //SteamUserStats.GetDownloadedLeaderboardEntry (fuck.m_hSteamLeaderboardEntries, index, out  leaderboardEntry, details, 3);
            //print ("Rank: " + leaderboardEntry.m_nGlobalRank);
            //print ("Score?: " + leaderboardEntry.m_nScore);
            //print ("Name: " + leaderboardEntry.m_steamIDUser);
            //print (SteamUserStats.GetLeaderboardName (SteamLeaderboards.s_currentLeaderboard));
            //print (SteamUserStats.GetLeaderboardEntryCount (SteamLeaderboards.s_currentLeaderboard));
            //print (SteamUserStats.GetLeaderboardDisplayType (SteamLeaderboards.s_currentLeaderboard));
            //}
            audioSource.PlayOneShot(victorySound, 0.15f);
			print ("Sent score");
            Timer.FinishLevel();
        }
    }

    private void GetLeaderBoardThings() {
        SteamAPICall_t handle = SteamUserStats.DownloadLeaderboardEntries(SteamLeaderboards.s_currentLeaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, 0, 5);
        LeaderboardScoresDownloaded.Set(handle);
        print("DownloadLeaderboardEntries(" + SteamLeaderboards.s_currentLeaderboard + ", ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, 1, 5) - " + handle);
    }

    private void ShowLeaderboardVariables() {
        LeaderboardEntry_t LeaderboardEntry;
        bool ret = SteamUserStats.GetDownloadedLeaderboardEntry(m_SteamLeaderboardEntries, 0, out LeaderboardEntry, null, 0);
        print("GetDownloadedLeaderboardEntry(" + m_SteamLeaderboardEntries + ", 0, out LeaderboardEntry, null, 0) - " + ret + " -- " + LeaderboardEntry.m_steamIDUser + " -- " + LeaderboardEntry.m_nGlobalRank + " -- " + LeaderboardEntry.m_nScore + " -- " + LeaderboardEntry.m_cDetails + " -- " + LeaderboardEntry.m_hUGC);

    }

    private void OnLeaderboardScoresDownloaded(LeaderboardScoresDownloaded_t pCallback, bool bIOFailure) {
        Debug.Log("[" + LeaderboardScoresDownloaded_t.k_iCallback + " - LeaderboardScoresDownloaded] - " + pCallback.m_hSteamLeaderboard + " -- " + pCallback.m_hSteamLeaderboardEntries + " -- " + pCallback.m_cEntryCount);
        m_SteamLeaderboardEntries = pCallback.m_hSteamLeaderboardEntries;

        for (int i = 0; i < pCallback.m_cEntryCount; i++) {
            LeaderboardEntry_t LeaderboardEntry;
            bool ret = SteamUserStats.GetDownloadedLeaderboardEntry(m_SteamLeaderboardEntries, i, out LeaderboardEntry, null, 0);
            print("GetDownloadedLeaderboardEntry(" + m_SteamLeaderboardEntries + ", 0, out LeaderboardEntry, null, 0) - " + ret + " -- " + LeaderboardEntry.m_steamIDUser + " -- " + LeaderboardEntry.m_nGlobalRank + " -- " + LeaderboardEntry.m_nScore + " -- " + LeaderboardEntry.m_cDetails + " -- " + LeaderboardEntry.m_hUGC);
        }

        //ShowLeaderboardVariables();
    }
}
