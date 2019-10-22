using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "State/State", order = 1)]
/// <summary>
/// A state in the United States of America
/// </summary>
public class State : ScriptableObject
{
    public States USState;
    public string StateNameEN;
    public List<County> Counties;

    /// <summary>
    /// Checks if this state has any counties
    /// </summary>
    public bool HasCounties()
    {
        if (Counties != null & Counties.Count > 0)
            return true;
        else
            return false;
    }

}
