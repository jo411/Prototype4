using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// A prefab object of a county
/// </summary>
public class CountyOnMapPrefabObject : WorldUIButton
{
    public TextMeshProUGUI CountyNameText; 

    private County countyInfo;

    /// <summary>
    /// Initializes the county prefab object with county info
    /// </summary>
    public void Initialize(County newCountyInfo)
    {
        countyInfo = newCountyInfo;
        CountyNameText.text = newCountyInfo.CountyNameEN;
    }

    /// <summary>
    /// Gets the attached county info
    /// </summary>
    public County GetCountyInfo()
    {
        return countyInfo;
    }

}
