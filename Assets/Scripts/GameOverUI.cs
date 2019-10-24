using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The game over canvas UI
/// </summary>
public class GameOverUI : MonoBehaviour
{
    private bool gameOverActive = false;

    private PlayerData playerData;


    private void Awake()
    {
        playerData = GameObject.FindObjectOfType<PlayerData>();
    }

    /// <summary>
    /// Turns the game over canvas on or off and affects other parameters
    /// </summary>
    public void TurnOnGameOverCanvas(bool onOff)
    {
        this.gameObject.SetActive(onOff);
        gameOverActive = onOff;
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
                RestartGame();
            }
        }
    }

    /// <summary>
    /// Restarts the game
    /// </summary>
    public void RestartGame()
    {
        playerData.RestartGame();
    }


}
