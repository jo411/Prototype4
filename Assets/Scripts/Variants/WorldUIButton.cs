using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorldUIButton : MonoBehaviour
{
    private bool highlighted = false; 
    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " entered");
        highlighted = true;
        SetAsHighlightedWorldUIButton();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name + " exited");
        highlighted = false;
    }

    /// <summary>
    /// Checks to see if we pressed this object by seeing if it's currently highlighted
    /// </summary>
    public bool CheckForPressed()
    {
        if (highlighted)
        {           
            return true;
        }            
        else
            return false;
    }

    /// <summary>
    /// Sets the current object as the highlighted world UI button
    /// </summary>
    protected abstract void SetAsHighlightedWorldUIButton();
}
