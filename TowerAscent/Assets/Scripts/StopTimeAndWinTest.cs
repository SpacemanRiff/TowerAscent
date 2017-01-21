using UnityEngine;
using System.Collections;

using System.Net;
using System.Text;
using System.IO;
using UnityEngine.Networking;
using Steamworks;


public class StopTimeAndWinTest : MonoBehaviour {
    public Timer Timer;
    public AudioClip victorySound;
    private AudioSource audioSource;
    private CallResult<LeaderboardScoresDownloaded_t> LeaderboardScoresDownloaded;
    private CallResult<PersonaStateChange_t> personaStat;
    private SteamLeaderboardEntries_t m_SteamLeaderboardEntries;
    public LeaderboardPrint leaderboardTextRank1;
	public LeaderboardPrint leaderboardTextRank2;

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



		int[] details = {0,0,0};
        if (other.tag == "VRController") {
            Timer.StopTime();
            print(Timer.getScore());
            SteamLeaderboards.UpdateScore(Timer.getScore());
            SteamAPICall_t handle = SteamUserStats.DownloadLeaderboardEntries(SteamLeaderboards.s_currentLeaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, 1, 5);
            LeaderboardScoresDownloaded.Set(handle);
            print("DownloadLeaderboardEntries(" + SteamLeaderboards.s_currentLeaderboard + ", ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, 1, 5) - " + handle);

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
    
            
            print("NAME!!!!:"+ SteamFriends.GetFriendPersonaName(LeaderboardEntry.m_steamIDUser));
            //timeConverter(LeaderboardEntry.m_nScore);


			leaderboardTextRank1.UpdateRanks(" " + LeaderboardEntry.m_nGlobalRank + ".\n");// + " " + SteamFriends.GetFriendPersonaName(LeaderboardEntry.m_steamIDUser) + " " + timeConverter(LeaderboardEntry.m_nScore)+"\n");
			leaderboardTextRank1.UpdateNames(nameChopper(SteamFriends.GetFriendPersonaName(LeaderboardEntry.m_steamIDUser))+ "\n");
			leaderboardTextRank1.UpdateTimes(timeConverter(LeaderboardEntry.m_nScore) + "\n");
			if (leaderboardTextRank2 != null) {
				leaderboardTextRank2.UpdateRanks(" " + LeaderboardEntry.m_nGlobalRank + ".\n");// + " " + SteamFriends.GetFriendPersonaName(LeaderboardEntry.m_steamIDUser) + " " + timeConverter(LeaderboardEntry.m_nScore)+"\n");
				leaderboardTextRank2.UpdateNames(nameChopper(SteamFriends.GetFriendPersonaName(LeaderboardEntry.m_steamIDUser))+ "\n");
				leaderboardTextRank2.UpdateTimes(timeConverter(LeaderboardEntry.m_nScore) + "\n");
			
			}



        }

        //ShowLeaderboardVariables();
    }

    public string timeConverter(int time) {
        string min = (time / 60000).ToString();
        string sec = ((time % 60000) / 1000).ToString();
        string mill = ((time % 60000) % 1000).ToString();

        if ((time / 60000) < 10) {
            min = ("0"+min);
        }
        if (((time % 60000) / 1000)<10) {
            sec = ("0" + sec);
        }
        if (((time % 60000) % 1000) < 10) {
            mill = ("0" + mill);
        }




        return (min + ":" + sec + ":" + mill.Substring(0,2));
    }

    public string nameChopper(string name) {
        if (name.Length > 11) {
            name = (name.Substring(0, 10) + "...");
        }

        return name;

    }

}
