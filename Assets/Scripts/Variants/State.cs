using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "State/State", order = 1)]
/// <summary>
/// A state in the United States of America
/// </summary>
public class State : ScriptableObject
{
    public string StateNameEN;
    public List<County> Counties;
}
