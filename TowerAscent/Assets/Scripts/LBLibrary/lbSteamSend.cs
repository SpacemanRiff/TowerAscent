using UnityEngine;
using System.Collections;
using Steamworks;
using System;

public class lbSteamSend : MonoBehaviour {

    private SteamLeaderboard_t m_SteamLeaderboard;
    private SteamLeaderboardEntries_t m_SteamLeaderboardEntries;
    private CallResult<LeaderboardFindResult_t> LeaderboardFindResult;
    private CallResult<LeaderboardScoresDownloaded_t> LeaderboardScoresDownloaded;
    private CallResult<LeaderboardScoreUploaded_t> LeaderboardScoreUploaded;
    private CallResult<LeaderboardUGCSet_t> LeaderboardUGCSet;

    // Use this for initialization
    void Start() {
        LeaderboardFindResult = CallResult<LeaderboardFindResult_t>.Create(OnLeaderboardFindResult);
        LeaderboardScoresDownloaded = CallResult<LeaderboardScoresDownloaded_t>.Create(OnLeaderboardScoresDownloaded);
        LeaderboardScoreUploaded = CallResult<LeaderboardScoreUploaded_t>.Create(OnLeaderboardScoreUploaded);
        LeaderboardUGCSet = CallResult<LeaderboardUGCSet_t>.Create(OnLeaderboardUGCSet);

        SteamAPICall_t handleThree = SteamUserStats.FindOrCreateLeaderboard("Feet Traveled", ELeaderboardSortMethod.k_ELeaderboardSortMethodAscending, ELeaderboardDisplayType.k_ELeaderboardDisplayTypeNumeric);
        LeaderboardFindResult.Set(handleThree);
        print("FindOrCreateLeaderboard(\"Feet Traveled\", ELeaderboardSortMethod.k_ELeaderboardSortMethodAscending, ELeaderboardDisplayType.k_ELeaderboardDisplayTypeNumeric) - " + handleThree);

        SteamAPICall_t handleOne = SteamUserStats.FindLeaderboard("Feet Traveled");
        LeaderboardFindResult.Set(handleOne);
        print("FindLeaderboard(\"Feet Traveled\") - " + handleOne);
        

        SteamAPICall_t handleFour = SteamUserStats.DownloadLeaderboardEntries(m_SteamLeaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, 1, 5);
        LeaderboardScoresDownloaded.Set(handleFour);
        print("DownloadLeaderboardEntries(" + m_SteamLeaderboard + ", ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, 1, 5) - " + handleFour);

        CSteamID[] Users = { SteamUser.GetSteamID() };
        SteamAPICall_t handleFive = SteamUserStats.DownloadLeaderboardEntriesForUsers(m_SteamLeaderboard, Users, Users.Length);
        LeaderboardScoresDownloaded.Set(handleFive);
        print("DownloadLeaderboardEntriesForUsers(" + m_SteamLeaderboard + ", " + Users + ", " + Users.Length + ") - " + handleFive);

        LeaderboardEntry_t LeaderboardEntry;
        bool ret = SteamUserStats.GetDownloadedLeaderboardEntry(m_SteamLeaderboardEntries, 0, out LeaderboardEntry, null, 0);
        print("GetDownloadedLeaderboardEntry(" + m_SteamLeaderboardEntries + ", 0, out LeaderboardEntry, null, 0) - " + ret + " -- " + LeaderboardEntry.m_steamIDUser + " -- " + LeaderboardEntry.m_nGlobalRank + " -- " + LeaderboardEntry.m_nScore + " -- " + LeaderboardEntry.m_cDetails + " -- " + LeaderboardEntry.m_hUGC);

        SteamAPICall_t handleTwo = SteamUserStats.UploadLeaderboardScore(m_SteamLeaderboard, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodForceUpdate, 50, null, 0);
        LeaderboardScoreUploaded.Set(handleTwo);
        print("UploadLeaderboardScore(" + m_SteamLeaderboard + ", ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodForceUpdate, " + 50 + ", null, 0) - " + handleTwo);

        SteamAPICall_t handle = SteamUserStats.AttachLeaderboardUGC(m_SteamLeaderboard, UGCHandle_t.Invalid);
        LeaderboardUGCSet.Set(handle);
        print("SteamUserStats.AttachLeaderboardUGC(" + m_SteamLeaderboard + ", " + UGCHandle_t.Invalid + ") - " + handle);
    }

    private void OnLeaderboardUGCSet(LeaderboardUGCSet_t pCallback, bool bIOFailure) {
        Debug.Log("[" + LeaderboardUGCSet_t.k_iCallback + " - LeaderboardUGCSet] - " + pCallback.m_eResult + " -- " + pCallback.m_hSteamLeaderboard);
    }

    private void OnLeaderboardScoreUploaded(LeaderboardScoreUploaded_t pCallback, bool bIOFailure) {
        Debug.Log("[" + LeaderboardScoreUploaded_t.k_iCallback + " - LeaderboardScoreUploaded] - " + pCallback.m_bSuccess + " -- " + pCallback.m_hSteamLeaderboard + " -- " + pCallback.m_nScore + " -- " + pCallback.m_bScoreChanged + " -- " + pCallback.m_nGlobalRankNew + " -- " + pCallback.m_nGlobalRankPrevious + "--" + bIOFailure);
    }

    private void OnLeaderboardScoresDownloaded(LeaderboardScoresDownloaded_t pCallback, bool bIOFailure) {
        Debug.Log("[" + LeaderboardScoresDownloaded_t.k_iCallback + " - LeaderboardScoresDownloaded] - " + pCallback.m_hSteamLeaderboard + " -- " + pCallback.m_hSteamLeaderboardEntries + " -- " + pCallback.m_cEntryCount + "--" + bIOFailure);
        m_SteamLeaderboardEntries = pCallback.m_hSteamLeaderboardEntries;
    }

    private void OnLeaderboardFindResult(LeaderboardFindResult_t pCallback, bool bIOFailure) {
        Debug.Log("[" + LeaderboardFindResult_t.k_iCallback + " - LeaderboardFindResult] - " + pCallback.m_hSteamLeaderboard + " -- " + pCallback.m_bLeaderboardFound +"---"+bIOFailure);

        if (pCallback.m_bLeaderboardFound != 0) {
            m_SteamLeaderboard = pCallback.m_hSteamLeaderboard;
        }
    }

    // Update is called once per frame
    void Update () {
        SteamAPI.RunCallbacks();
    }
}
