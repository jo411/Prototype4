using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a state on the US map
/// </summary>
public class StateOnMap : WorldUIButton
{
    private static StateOnMap highlightedStateOnMap = null;

    public State StateInfo;

    /// <summary>
    /// Sets this state on mapas the currently highlighted world UI button
    /// </summary>
    protected override void SetAsHighlightedWorldUIButton()
    {
        highlightedStateOnMap = this;
    }

    /// <summary>
    /// Gets the highlighted state on map
    /// </summary>
    public static StateOnMap GetHighlightedWorldUIButton()
    {
        return highlightedStateOnMap;
    }
}
