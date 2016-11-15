using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class lbReceive : MonoBehaviour {
    private string data, temp = string.Empty;
    private ArrayList username, time;
    public Text TextAreaTower, TextAreaGround;
    public string levelName;

    // Use this for initialization
    void Start() {
        StartCoroutine(receive());
        //loadLeaderboard();
    }

    public IEnumerator receive() {
        WWWForm form = new WWWForm();
        form.AddField("LevelName", levelName);

        UnityWebRequest www = UnityWebRequest.Post("http://www.nathanpell.net/php/lbretreive.php",form);
        yield return www.Send();

        if (www.isError) {
            Debug.Log(www.error);
            Debug.Log("You Aint connected!");
        }
        else {
            data = www.downloadHandler.text;
            Debug.Log(data);
            updateLeaderboard();
            saveLeaderboard();
            Debug.Log("Record download complete!");
            loadLeaderboard();
            Debug.Log("Leaderboard Loaded.");
        }
    }

    public void updateLeaderboard() {
        char c;
        string nameLabel = "[Name] => ", timeLabel = "[Time] => ";
        username = new ArrayList();
        time = new ArrayList();
        bool getUsername = false, getTime = false;

        for (int i = 0, rank = 0; i < data.Length; i++) {
            c = data[i];
            temp += c;
            //check for username label and time label
            if (temp.Length > 10) {
                if (temp.Substring((temp.Length) - (nameLabel.Length)).Equals(nameLabel)) {
                    temp = "";
                    getUsername = true;
                }
                else if (temp.Substring(temp.Length - timeLabel.Length).Equals(timeLabel)) {
                    temp = "";
                    getTime = true;
                }
            }

            if (temp != string.Empty) {
                //store username
                if (getUsername) {
                    if (temp[temp.Length - 1].Equals(' ')) {
                        if (!username.Contains(temp.Substring(0, temp.Length - 1))) {
                            username.Add(temp.Substring(0, temp.Length - 1));
                        }
                        getUsername = false;
                    }
                }

                //store time
                if (getTime) {
                    if (temp[temp.Length - 1].Equals(' ')) {
                        
                        time.Add(temp.Substring(0, temp.Length - 1));
                        rank += 1;
                        getTime = false;
                    }
                }
            }
        }
        for (int j = 0; j < username.Count;j++) {
            //Debug.Log(username[j]);
            //Debug.Log(time[j]);
        }
    }

    public void saveLeaderboard() {
        string path = Directory.GetCurrentDirectory();
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(path + "/" + levelName + "_leaderboard.txt")) {
            for (int i = 0; i < username.Count; i++) {
                file.WriteLine(i + 1);
                file.WriteLine(username[i]);
                file.WriteLine(time[i]);
            }
            Debug.Log(path + "/" + levelName + "_leaderboard.txt");
        }
    }


    public void loadLeaderboard()
    {
        string leaderboardText = "";
        int count = 0, countTwo = 0, countThree = 0, countFour = 0;
        string path = Directory.GetCurrentDirectory() + "/" + levelName + "_leaderboard.txt";
        using (System.IO.StreamReader file = new System.IO.StreamReader(path))
        {
            leaderboardText = file.ReadToEnd();
            leaderboardText = Regex.Replace(leaderboardText, @"\r\n", " ");
            leaderboardText = Regex.Replace(leaderboardText, @"\n", "");
            char[] leaderboard = leaderboardText.ToCharArray();
            leaderboardText = "";
            foreach (char c in leaderboard)
            {
                countTwo++;
                if ( c == ':')
                {
                    count++;
                    if (count % 2 == 0)
                    {
                        leaderboard[countTwo + 2] = '\n';
                        countThree++;
                        
                    }
                }
                leaderboardText += c;
                if (countThree >= 10 && countFour == 2)
                {
                    break;
                }
                else if (countThree == 10)
                {
                    countFour++;
                }
            }
            
            TextAreaTower.text = leaderboardText;
            TextAreaGround.text = leaderboardText;
        }
    }
}
      /*Array
        (
            [0] => Array
                (
                    [Name] => Jack
                    [0] => Jack
                    [Time] => 00:05:00
                    [1] => 00:05:00
                )

            [1] => Array
                (
                    [Name] => Todd
                    [0] => Todd
                    [Time] => 00:06:25
                    [1] => 00:06:25
                )

        )*/

