using UnityEngine;
using System.Net;
using System.Text;
using System.IO;
using UnityEngine.Networking;
using System.Collections;
using System;
using Steamworks;

public class lbSend : MonoBehaviour {
    public PlayerName ObjPlayerName;
    public string playerName;
    public string  playerTime, levelName;
    public Timer timer;
    public bool timeIsLower = false;
    public lbReceive receiver;
    void Start() {
        //StartCoroutine(send());
       
    }
    void Update() {
        playerName = ObjPlayerName.getName();
    }
    //To Do: make another function/class that reads through lb files and detects if name is already present, then update instead of insert 
    public IEnumerator send() {
        SteamAPICall_t handle = SteamUserStats.FindLeaderboard(levelName);

        //StartCoroutine(receiver.receive());
        //levelName = "TOWER_ASCENT_LB_TT";
        Debug.Log("Working on Sending");
        WWWForm form = new WWWForm();
        form.AddField("Name", playerName);
        Debug.Log(playerTime);
        form.AddField("Time", playerTime);
        form.AddField("LevelName", levelName); //To Do: Set levelName = currentLevelName
        Debug.Log(playerExists());

        if (playerExists())
        {
            if (timeIsLower)
            {
                //timeIsLower = false;
                UnityWebRequest www = UnityWebRequest.Post("http://www.nathanpell.net/php/lbupdate.php", form);
                yield return www.Send();

                if (www.isError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form updating complete!");
                }
            }
        }
        else {
            UnityWebRequest www = UnityWebRequest.Post("http://www.nathanpell.net/php/lbsend.php", form);
            yield return www.Send();

            if (www.isError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
        //StartCoroutine(receiver.receive());
    }
    private bool playerExists() {
        string path = Directory.GetCurrentDirectory()+ "/" + levelName + "_leaderboard.txt";
        using (System.IO.StreamReader file = new System.IO.StreamReader(path)) {
            string line;
            while ((line = file.ReadLine()) != null) {
                if (line == playerName) {
                    line = file.ReadLine();
                    line = file.ReadLine();
                    playerTime = timer.timeyTime;
                    if (line.CompareTo(playerTime) > 0)
                    {
                        string allText = File.ReadAllText(path);
                        allText.Replace(playerName + Environment.NewLine + line,
                            playerName + Environment.NewLine + playerTime);
                        timeIsLower = true;
                    }
                    return true;
                }
            }
            return false;
        }   
    }

    public void reset()
    {
        playerTime = timer.timeyTime;
        StartCoroutine(send());
        //playerTime = timer.timeyTime;
        //WaitUntil(send.)
        //StopAllCoroutines();
    }
   
}
