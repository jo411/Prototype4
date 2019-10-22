using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [TagSelector]
    public string[] filterTags = new string[] { };

    private HashSet<string> allowedTags = new HashSet<string>();

    public float moveSpeed=1f;
    public float minSpeed=1f;
    public float maxSpeed=1f;

    public Vector3 moveTo = Vector3.zero;//direction to move in    
    public HashSet<GameObject> targets = new HashSet<GameObject>();   
    private List<GameObject> deleteMark = new List<GameObject>();

    Renderer rend;
    float shaderPanSpeedMult = 10f;

    // Start is called before the first frame update
    void Start()
    {
        foreach(string current in filterTags)
        {
            allowedTags.Add(current);
        }       
        rend = GetComponent<Renderer>();
        setSpeed(.5f);//start at 50% speed
    }

    // Update is called once per frame
    void Update()
    {
        processRemoveBuffer();
        foreach (GameObject current in targets)
        {
            current.transform.position = Vector3.MoveTowards(current.transform.position, new Vector3(transform.position.x + moveTo.x, current.transform.position.y, transform.position.z + moveTo.z), Time.deltaTime * moveSpeed);            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {       
        if(allowedTags.Contains(collision.gameObject.tag))
        {
            targets.Add(collision.gameObject);        }
      
    }

    private void OnCollisionStay(Collision collision)
    {
        //if(!collision.gameObject.GetComponent<ObjectData>().gameObjectData.ContainsKey("Moving"))
        //{
        //    Debug.Log("Bad Stuff");
        //}
        //this.OnCollisionEnter(collision);//TODO: Look into alternatives? This fixes a bug where things may stop moving after bouncing on a conveyer. I don't love this soluition.
    }

    private void OnCollisionExit(Collision collision)
    {
        ObjectData data = collision.gameObject.GetComponent<ObjectData>();
        if (allowedTags.Contains(collision.gameObject.tag))
        {
           deleteMark.Add(collision.gameObject);
        }
        processRemoveBuffer();
    }

    void processRemoveBuffer()
    {
        foreach (GameObject current in deleteMark)
        {
            targets.Remove(current);              
        }
        deleteMark.Clear();
    }
    private void OnDrawGizmosSelected()
    {
        DrawArrow.ForGizmo(transform.position, moveTo);
    }

    public void setSpeed(float percent)
    {
        float scaledSpeed = minSpeed + ((maxSpeed - minSpeed) * percent);        
        this.moveSpeed = scaledSpeed;
        rend.material.SetFloat("_myXSpeed", scaledSpeed * shaderPanSpeedMult);
    }
}
