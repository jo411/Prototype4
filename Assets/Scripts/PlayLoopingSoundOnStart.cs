using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLoopingSoundOnStart : MonoBehaviour
{
    public AudioClip loopingSound;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayLoop(loopingSound, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
