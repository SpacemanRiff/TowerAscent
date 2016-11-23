using UnityEngine;
using System.Collections;
using Steamworks;

public class GetNumberOfPlayers : MonoBehaviour {

    private CallResult<NumberOfCurrentPlayers_t> numberOfPlayersCallResult;

    void Start()
    {
        if (SteamManager.Initialized)
        {
            print("Steam initialized");
            numberOfPlayersCallResult = CallResult<NumberOfCurrentPlayers_t>.Create(OnNumberOfCurrentPlayers);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SteamAPICall_t handle = SteamUserStats.GetNumberOfCurrentPlayers();
            numberOfPlayersCallResult.Set(handle);
        }
    }

    private void OnNumberOfCurrentPlayers(NumberOfCurrentPlayers_t callBack, bool bIOfailure)
    {
        if (callBack.m_bSuccess != 1 || bIOfailure)
        {
            Debug.Log("Error getting players playing");
        }
        else
        {
            Debug.Log("Number of people playing: " + callBack.m_cPlayers);
        }
    }
}
