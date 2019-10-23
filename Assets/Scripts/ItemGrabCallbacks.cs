using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VRTK.Prefabs.Interactions.Interactables;

[RequireComponent(typeof(InteractableFacade))]
public class ItemGrabCallbacks : MonoBehaviour
{
    public AudioClip grabSound;
    
   
   
    public void Start()
    {
        GetComponent<InteractableFacade>().Grabbed.AddListener(delegate { onItemGrab(); });
    }
    public void onItemGrab()
    {
        AudioManager.Instance.Play(grabSound, transform);
    }

    public void onItemRelease(InteractableFacade facade)
    {
        //maybe apply force multiplier here
    }
}

