using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VRTK.Prefabs.Interactions.Interactables;

[RequireComponent(typeof(InteractableFacade))]
[RequireComponent(typeof(Rigidbody))]
public class ItemGrabCallbacks : MonoBehaviour
{
    public AudioClip grabSound;
    public float throwSpeedMult;

    private Rigidbody rb;
    
   
   
    public void Start()
    {
        GetComponent<InteractableFacade>().Grabbed.AddListener(delegate { onItemGrab(); });
        GetComponent<InteractableFacade>().Ungrabbed.AddListener(delegate { onItemRelease(); });
        rb = GetComponent<Rigidbody>();
    }
    public void onItemGrab()
    {
        AudioManager.Instance.Play(grabSound, transform);
    }

    public void onItemRelease()
    {
        //maybe apply force multiplier here
        rb.velocity *= throwSpeedMult;

    }
}

