using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI TimePlayedText;
    public TextMeshProUGUI PointsText;

    public void UpdateTimePlayed(float totalTime)
    {
        var ss = Convert.ToInt32(totalTime % 60).ToString("00");
        var mm = (Math.Floor(totalTime / 60)).ToString("00");
        TimePlayedText.text = "Time Played: " + mm + ":" + ss;
    }


    public void UpdatePoints(int totalPoints)
    {
        PointsText.text = "Points: " + totalPoints.ToString();
    }

}
