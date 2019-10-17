using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HandAnimator : MonoBehaviour
{
    Animator anim;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnDestroy()
    {

    }

   public void onHandCloseAmmountChange(float axis)
    {
        //Debug.Log(axis);
        anim.SetFloat("GrabBlend", axis);
        
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

    }
}
