using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReceivedPointsPrefabObject : MonoBehaviour
{
    //object references
    public TextMeshProUGUI ReceivedPointsText;

    private GameObject player;

    //parameters
    private float timeAlive = 0.0f;
    private const float MAX_TIME_ALIVE = 1.0f;


    

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        
    }

    public void Initialize(bool gainedPoints, int pointsValue)
    {
        string displayString = "";

        if(gainedPoints)
        {
            displayString += "+";
            ReceivedPointsText.color = new Color32(0, 255, 0, 255); //green
        }
        else
        {
            displayString += "-";
            ReceivedPointsText.color = new Color32(255, 0, 0, 255); //red
        }

        displayString += pointsValue.ToString();
        ReceivedPointsText.text = displayString;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);

        timeAlive += Time.deltaTime;
        if(timeAlive >= MAX_TIME_ALIVE)
        {
            Destroy(this);
        }
    }
}
