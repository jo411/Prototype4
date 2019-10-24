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
    private int lives = 3;

    private PlayerUI playerUI;
    private GameHandler gameHandler;
    private ParticleSystem snowParticleSystem;

    private void Awake()
    {
        playerUI = GameObject.FindObjectOfType<PlayerUI>();
        gameHandler = GameObject.FindObjectOfType<GameHandler>();
        snowParticleSystem = GameObject.Find("Snow").GetComponent<ParticleSystem>(); //TODO should probably fix
        if (snowParticleSystem != null)
            snowParticleSystem.Stop();
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


    #region Restart
    public void RestartGame()
    {
        totalPoints = 0;
        timePlayed = 0.0f;
        lives = 3; //TODO change to max lives with a const variable or modifiable value

        playerUI.UpdatePoints(totalPoints);
        playerUI.UpdateTimePlayed(timePlayed);
        playerUI.UpdateLives(lives);

        if (snowParticleSystem != null)
            snowParticleSystem.Stop();
    }

    #endregion Restart


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

        lives--;
        playerUI.UpdateLives(lives);
        AdjustEnvironment();
    }

    #endregion Data Management


    #region Environment
    /// <summary>
    /// Adjusts the environment based on player lives
    /// </summary>
    public void AdjustEnvironment()
    {
        if(lives == 2)
        {
            if (snowParticleSystem != null)
                snowParticleSystem.Play();
        }
        else if(lives == 1)
        {

        }
        else if(lives == 0)
        {

        }
    }

    #endregion Environment

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
