using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds all player data
/// </summary>
public class PlayerData : MonoBehaviour
{

    private int totalPoints = 0;
    private float timePlayed = 0.0f;


    private PlayerUI playerUI;
    private GameHandler gameHandler;

    private void Awake()
    {
        playerUI = GameObject.FindObjectOfType<PlayerUI>();
        gameHandler = GameObject.FindObjectOfType<GameHandler>();
    }

    private void Update()
    {
        if (playerUI != null)
        {
            if(gameHandler != null && gameHandler.IsPlayingGame())
            {
                timePlayed += Time.deltaTime;
                playerUI.UpdateTimePlayed(timePlayed);
            }
        }
    }


    #region Data Management
    /// <summary>
    /// Add points to the player
    /// </summary>
    public void AddPoints(int addedPoints)
    {
        totalPoints += addedPoints;
        playerUI.UpdatePoints(totalPoints);
    }

    /// <summary>
    /// Remove points from the player
    /// </summary>
    public void RemovePoints(int removedPoints)
    {
        totalPoints -= removedPoints;
        playerUI.UpdatePoints(totalPoints);
    }

    #endregion Data Management

    #region Getters
    /// <summary>
    /// Gets the total number of points the player has accumulated
    /// </summary>
    public int GetTotalPoints()
    {
        return totalPoints;
    }

    #endregion Getters
}
