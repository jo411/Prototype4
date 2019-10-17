using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorPanner : MonoBehaviour
{

    public float xSpeed = 0.5f;
    public float ySpeed = 0f;


    float xOffset;
    float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xOffset = Time.time * xSpeed;
        yOffset = Time.time * ySpeed;
        GetComponent<Renderer>().material.mainTextureOffset=new Vector2(xOffset,yOffset);
     
    }
}
