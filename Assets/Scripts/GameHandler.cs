using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public RecycleBin AluminumRB;
    public RecycleBin CompostRB;
    public RecycleBin ElectronicRB;
    public RecycleBin GlassRB;
    public RecycleBin NonRecyclableRB;
    public RecycleBin PaperRB;
    public RecycleBin PlasticRB;


    // Start is called before the first frame update
    void Awake()
    {
        
    }



    /// <summary>
    /// Sets up the game by county info
    /// </summary>
    public void SetupGame(County countyInfo)
    {
        if (countyInfo.RecyclesAluminum)
            AluminumRB.gameObject.SetActive(true);
        if (countyInfo.RecyclesCompost)
            AluminumRB.gameObject.SetActive(true);
        if (countyInfo.RecyclesElectronic)
            AluminumRB.gameObject.SetActive(true);
        if (countyInfo.RecyclesGlass)
            AluminumRB.gameObject.SetActive(true);
        if (countyInfo.RecyclesNonRecyclable)
            AluminumRB.gameObject.SetActive(true);
        if (countyInfo.RecyclesPaper)
            AluminumRB.gameObject.SetActive(true);
        if (countyInfo.RecyclesPlastic)
            AluminumRB.gameObject.SetActive(true);
    }

}
