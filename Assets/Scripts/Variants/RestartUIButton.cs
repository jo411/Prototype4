using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The restart button for the game over screen
/// </summary>
public class RestartUIButton : WorldUIButton
{
    private static RestartUIButton highlightedRestartUIButton = null;


    /// <summary>
    /// Sets the restart UI button
    /// </summary>
    protected override void SetAsHighlightedWorldUIButton()
    {
        highlightedRestartUIButton = this;
    }

    /// <summary>
    /// Sets the restart UI button to null
    /// </summary>
    public static void SetRestartUIButtonToNull()
    {
        highlightedRestartUIButton = null;
    }

    /// <summary>
    /// Gets the currently highlighted restart UI button
    /// </summary>
    public static RestartUIButton GetHighlightedWorldUIButton()
    {
        return highlightedRestartUIButton;
    }
}
