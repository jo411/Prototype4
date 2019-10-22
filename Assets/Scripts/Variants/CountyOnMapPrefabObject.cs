using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// A prefab object of a county
/// </summary>
public class CountyOnMapPrefabObject : WorldUIButton
{
    private static CountyOnMapPrefabObject highlightedCountyOnMapPrefabObject = null;

    public TextMeshProUGUI CountyNameText;

    private County countyInfo;

    private bool isBackButton = false;

    /// <summary>
    /// Initializes the county prefab object with county info
    /// </summary>
    public void Initialize(County newCountyInfo)
    {
        countyInfo = newCountyInfo;
        CountyNameText.text = newCountyInfo.CountyNameEN;
    }

    public void Initialize(bool backButton)
    {
        if (!backButton)
            return;

        CountyNameText.text = "BACK";
        isBackButton = true;
    }

    /// <summary>
    /// Gets the attached county info
    /// </summary>
    public County GetCountyInfo()
    {
        return countyInfo;
    }

    /// <summary>
    /// Sets this county on map prefab object as the currently highlighted world UI button
    /// </summary>
    protected override void SetAsHighlightedWorldUIButton()
    {
        highlightedCountyOnMapPrefabObject = this;
    }

    /// <summary>
    /// Gets the highlighted county on map prefab object
    /// </summary>
    public static CountyOnMapPrefabObject GetHighlightedWorldUIButton()
    {
        return highlightedCountyOnMapPrefabObject;
    }

    /// <summary>
    /// Checks to see if this is actually just a back button
    /// </summary>
    /// <returns></returns>
    public bool IsBackButton()
    {
        return isBackButton;
    }

}
