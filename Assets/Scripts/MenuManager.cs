using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        //for (int i = 0; i < 5; i++)
        //{
        //    LeaderboardEntry tempEntry = new LeaderboardEntry()
        //    {
        //        userName = "Steve" + i,
        //        score = 40 - 5 * i
        //    };
        //    leaderboardEntries.Add(tempEntry);
        //}
    }

    //A serialiazable entry for the leaderboard, able to be converted and stored in json
    [System.Serializable]
    public class LeaderboardEntry
    {
        public string userName;
        public int score;
        //public string date;
    }

    public static void AddToLeaderboard(LeaderboardEntry newEntry)
    {
        leaderboardEntries.Add(newEntry);
        leaderboardEntries = SortList(leaderboardEntries);
        if (leaderboardEntries.Count > 4)
        {
            leaderboardEntries.RemoveRange(5, leaderboardEntries.Count - 5);
        }
    }

    public static void SaveLeaderboard()
    {
        string jsonString = JsonHelper.ToJson<LeaderboardEntry>(leaderboardEntries.ToArray());
        File.WriteAllText(Application.persistentDataPath + "/leaderboardsavefile.json", jsonString);
    }

    public static void LoadLeaderboard()
    {
        string jsonString = File.ReadAllText(Application.persistentDataPath + "/leaderboardsavefile.json");
        LeaderboardEntry[] jsonArray = JsonHelper.FromJson<LeaderboardEntry>(jsonString);
        foreach (LeaderboardEntry entry in jsonArray)
        {
            AddToLeaderboard(entry);
        }
        // leaderboardEntries = jsonArray.ToList();
    }

    //List sorting algorythm, took from code monkey video but made separate method
    public static List<LeaderboardEntry> SortList(List<LeaderboardEntry> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < list.Count; j++)
            {
                if (list[j].score < list[i].score)
                {
                    //if there is a higher score item, replace current item with that item
                    LeaderboardEntry temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;
                }
            }
        }
        return list;
    }

    //Code added from stackoverflow. The JsonHelper aids with getting a list or array serializable for json based saving
    //https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
    //End of stackoverflow code
}
