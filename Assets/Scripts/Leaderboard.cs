using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//No Longer Used, all code consolidated with the GUIMenu.cs
public class Leaderboard : MonoBehaviour
{
    private Transform scoreContainer;
    private Transform scoreTemplate;
    

public void LoadLeaderboard()
    {    
        List<Transform> leaderboardEntriesTransforms = new List<Transform>();
        scoreContainer = transform.Find("ScoreContainer");
        scoreTemplate = scoreContainer.Find("ScoreTemplate");
        scoreTemplate.gameObject.SetActive(false);

        //Deletes old clones of the ScoreTemplate. ScoreTemplate survives because it is deactivated
        foreach (Transform child in scoreContainer.transform)
        {
            if (child.gameObject.activeSelf)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        //Adds a leaderboard onto the GUI for each element in the MenuManager's list leaderboardEntires
        foreach (MenuManager.LeaderboardEntry leaderboardEntry in MenuManager.leaderboardEntries)
        {
            CreateLeaderboardEntryTransform(leaderboardEntry, scoreContainer, leaderboardEntriesTransforms);              
        }
    }
    private void CreateLeaderboardEntryTransform(MenuManager.LeaderboardEntry leaderboardEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 20f;
        Transform scoreTransform = Instantiate(scoreTemplate, container);
        RectTransform scoreRectTransform = scoreTransform.GetComponent<RectTransform>();
        scoreRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count - 50);
        scoreTransform.gameObject.SetActive(true);

        //Sets the position text string based on the entry's position within transformList
        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }
        //sets the text for the entry's three text boxes (Position, Username, Score)
        scoreTransform.Find("PosTemp").GetComponent<TMPro.TextMeshProUGUI>().text = rankString;

        string username = leaderboardEntry.userName;
        scoreTransform.Find("UserTemp").GetComponent<TMPro.TextMeshProUGUI>().text = username;

        int score = leaderboardEntry.score;
        scoreTransform.Find("ScoreTemp").GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();

        transformList.Add(scoreTransform);
    }    
}
