using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Holds all player data
/// </summary>
public class PlayerData : MonoBehaviour
{
    private int totalPoints = 0;
    private float timePlayed = 0.0f;
    private int lives = 5;

    private const int PLAYER_LIVES = 5;

    public GameOverUI GameOverCanvas;

    private PlayerUI playerUI;
    private GameHandler gameHandler;
    private ParticleSystem snowParticleSystem;
    private LeaderBoardManager scoreManager;

    private void Awake()
    {
        playerUI = GameObject.FindObjectOfType<PlayerUI>();
        gameHandler = GameObject.FindObjectOfType<GameHandler>();
        GameObject sps = GameObject.Find("Snow"); //TODO fix or something
        scoreManager = GetComponent<LeaderBoardManager>();
        
        if (sps != null)
        {
            snowParticleSystem = sps.GetComponent<ParticleSystem>();
            if(snowParticleSystem != null)
                snowParticleSystem.Stop();
        }
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
    /// <summary>
    /// Restarts the game by resetting parameters
    /// </summary>
    public void RestartGame()
    {
        Debug.Log("player data restarting");
        GameOverCanvas.TurnOnGameOverCanvas(false); //turn off once we have hit the restart button
        gameHandler.RestartGame();

        totalPoints = 0;
        timePlayed = 0.0f;
        lives = PLAYER_LIVES; //TODO change to max lives with a const variable or modifiable value

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

        if (lives <= 0)
            EndGame();
    }

    /// <summary>
    /// Ends the game
    /// </summary>
    private void EndGame()
    {
        GameOverCanvas.TurnOnGameOverCanvas(true); //turn on the game over canvas
        //OnGameOver.Invoke();
        gameHandler.EndGame();
        foreach(ConveyorBelt cb in GameObject.FindObjectsOfType<ConveyorBelt>())
        {
            cb.ClearAllObjects();
        }

        if(scoreManager.isHighScore(totalPoints))
        {
            promptPlayerName();
        }
    }

    void promptPlayerName()
    {

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
            {
                snowParticleSystem.GetComponent<snowController>().onSnowAppear();
                snowParticleSystem.Play();
            }
               
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

    /// <summary>
    /// Gets the amount of time played so far
    /// </summary>
    public float GetTimePlayed()
    {
        return timePlayed;
    }
    #endregion Getters
}
