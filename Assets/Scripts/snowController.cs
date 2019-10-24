using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowController : MonoBehaviour
{
    public AudioClip snowSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onSnowAppear()
    {
        AudioManager.Instance.Play(snowSound,transform);
    }
}
