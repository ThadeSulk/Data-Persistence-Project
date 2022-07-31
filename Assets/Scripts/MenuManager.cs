using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    
    public static List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        //populate the list of leaderboard entries if savefile exists (currently junk data)
        for (int i = 0; i < 5; i++)
        {
            LeaderboardEntry tempEntry = new LeaderboardEntry()
            {
                userName = "Steve" + i,
                score = 40 - 5 * i
            };
            leaderboardEntries.Add(tempEntry);
        }
    }

    //A serialiazable entry for the leaderboard, able to be converted and stored in json
    [System.Serializable]
    public class LeaderboardEntry
    {
        public string userName;
        public int score;
        //public string date;
    }

    //public void SaveLeaderboard()
    //{
    //string json = JsonUtility.ToJson(leaderboard[0]);
    //}

}
