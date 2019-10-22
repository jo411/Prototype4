using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// A county in a state with parameters
/// </summary>
public class County
{
    public string CountyNameEN;

    public bool RecyclesAluminum = true;
    public bool RecyclesCompost = true;
    public bool RecyclesElectronic = true;
    public bool RecyclesGlass = true;
    public bool RecyclesNonRecyclable = true;
    public bool RecyclesPaper = true;
    public bool RecyclesPlastic = true;

}
