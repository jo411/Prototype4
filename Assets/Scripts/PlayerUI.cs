using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/// <summary>
/// The player's UI
/// </summary>
public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI TimePlayedText;
    public TextMeshProUGUI PointsText;

    public CanvasGroup Heart1;
    public CanvasGroup Heart2;
    public CanvasGroup Heart3;
    public CanvasGroup Heart4;
    public CanvasGroup Heart5;

    public void UpdateTimePlayed(float totalTime)
    {
        var ss = Convert.ToInt32(totalTime % 60).ToString("00");
        var mm = (Math.Floor(totalTime / 60)).ToString("00");
        TimePlayedText.text = "Time Played: " + mm + ":" + ss;
    }

    /// <summary>
    /// Update points in the UI
    /// </summary>
    public void UpdatePoints(int totalPoints)
    {
        PointsText.text = "Points: " + totalPoints.ToString();
    }

    /// <summary>
    /// Update lives in the UI
    /// </summary>
    public void UpdateLives(int lives)
    {
        if (lives >= 1)
            Heart1.alpha = 1;
        else
            Heart1.alpha = 0;

        if (lives >= 2)
            Heart2.alpha = 1;
        else
            Heart2.alpha = 0;

        if (lives >= 3)
            Heart3.alpha = 1;
        else
            Heart3.alpha = 0;

        if (lives >= 4)
            Heart4.alpha = 1;
        else
            Heart4.alpha = 0;

        if (lives >= 5)
            Heart5.alpha = 1;
        else
            Heart5.alpha = 0;
    }

}
