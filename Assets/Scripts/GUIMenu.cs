using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GUIMenu : MonoBehaviour
{
    public GameObject leaderboardPanel;
    private Leaderboard leaderboard;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LeaderboardToggle()
    {
        if (leaderboardPanel.activeSelf)
        {
            leaderboardPanel.SetActive(false);
        }
        else
        {
            leaderboardPanel.SetActive(true); 
            leaderboard = leaderboardPanel.GetComponent<Leaderboard>();
            leaderboard.LoadLeaderboard();            
        }
    }
    private void LoadLeaderboard()
    {

    }
}
