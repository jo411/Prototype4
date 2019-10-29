using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// The game over canvas UI
/// </summary>
[RequireComponent(typeof(LeaderBoardManager))]
public class GameOverUI : MonoBehaviour
{
    private bool gameOverActive = false;

    private PlayerData playerData;
    private GameHandler GameHandler;
    public LeaderBoardManager leaderBoard;
    public TextMeshProUGUI leaderboardText;

    public AudioClip clickSound;
    private string audioFallbackResourcePath = "sounds/click";

    private void Awake()
    {
        playerData = GameObject.FindObjectOfType<PlayerData>();
        if (clickSound == null)
        {
            clickSound = Resources.Load<AudioClip>(audioFallbackResourcePath);
        }
        
    }

    private void Start()
    {
       
    }

    public void updateLeaderBoard()
    {
         leaderboardText.SetText("High Scores: \n" + leaderBoard.getDisplayStringForAllScores());       
       
    }

    /// <summary>
    /// Turns the game over canvas on or off and affects other parameters
    /// </summary>
    public void TurnOnGameOverCanvas(bool onOff)
    {
        this.gameObject.SetActive(onOff);
        gameOverActive = onOff;
        updateLeaderBoard();
    }

    /// <summary>
    /// Looks for a press on the game over canvas
    /// </summary>
    public void CheckForPressOnGameOverCanvas()
    {
        Debug.Log("check press 1");
        if(gameOverActive)
        {
            Debug.Log("check press 2");
            if (RestartUIButton.GetHighlightedWorldUIButton() != null)
            {
                Debug.Log("check press 3");
                AudioManager.Instance.Play(clickSound, transform);
                playerData.submitScoreAndName();
                RestartGame();
            }
        }
    }

    /// <summary>
    /// Restarts the game
    /// </summary>
    public void RestartGame()
    {
        updateLeaderBoard();
        playerData.RestartGame();
    }


}
