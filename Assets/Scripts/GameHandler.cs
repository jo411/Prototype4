using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private RecycleBin aluminumRB;
    private RecycleBin compostRB;
    private RecycleBin electronicRB;
    private RecycleBin glassRB;
    private RecycleBin nonRecyclableRB;
    private RecycleBin paperRB;
    private RecycleBin plasticRB;

    private bool initialized = false;
    private bool playingGame = false;

    private void Awake()
    {
        aluminumRB = GameObject.Find("AluminumRB").GetComponent<RecycleBin>();
        compostRB = GameObject.Find("CompostRB").GetComponent<RecycleBin>();
        electronicRB = GameObject.Find("ElectronicRB").GetComponent<RecycleBin>();
        glassRB = GameObject.Find("GlassRB").GetComponent<RecycleBin>();
        nonRecyclableRB = GameObject.Find("NonRecyclableRB").GetComponent<RecycleBin>();
        paperRB = GameObject.Find("PaperRB").GetComponent<RecycleBin>();
        plasticRB = GameObject.Find("PlasticRB").GetComponent<RecycleBin>();
    }

    /// <summary>
    /// Sets up the game by county info
    /// </summary>
    public void SetupGame(County countyInfo)
    {
        if (!countyInfo.RecyclesAluminum)
            aluminumRB.gameObject.SetActive(false);
        if (!countyInfo.RecyclesCompost)
            compostRB.gameObject.SetActive(false);
        if (!countyInfo.RecyclesElectronic)
            electronicRB.gameObject.SetActive(false);
        if (!countyInfo.RecyclesGlass)
            glassRB.gameObject.SetActive(false);
        if (!countyInfo.RecyclesNonRecyclable)
            nonRecyclableRB.gameObject.SetActive(false);
        if (!countyInfo.RecyclesPaper)
            paperRB.gameObject.SetActive(false);
        if (!countyInfo.RecyclesPlastic)
            plasticRB.gameObject.SetActive(false);

        GameObject.FindObjectOfType<TrashSpawner>().StartTrashSpawner(countyInfo);

        initialized = true;
        playingGame = true;
    }


    /// <summary>
    /// Checks to see if the game handler is initialized
    /// </summary>
    public bool IsInitialized()
    {
        return initialized;
    }

    /// <summary>
    /// Checks to see if the game handler is in the playing game state
    /// </summary>
    public bool IsPlayingGame()
    {
        return playingGame;
    }

}
