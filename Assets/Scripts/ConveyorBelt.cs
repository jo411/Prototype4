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

    private float currentPercent = 0.5f;
    private float timeModifier = 1.0f;

    public Vector3 moveTo = Vector3.zero;//direction to move in    
    //public HashSet<GameObject> targets = new HashSet<GameObject>();
    public List<GameObject> targets = new List<GameObject>();
    private List<GameObject> deleteMark = new List<GameObject>();

    Renderer rend;
    private List<Renderer> allRends;

    float shaderPanSpeedMult = 10f;


    private static PlayerData playerData;

    private void Awake()
    {
        if (playerData == null)
        {
            playerData = GameObject.FindObjectOfType<PlayerData>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        allRends = new List<Renderer>();
        allRends.AddRange(transform.root.GetComponentsInChildren<Renderer>());
        

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
        currentPercent = percent;

        float scaledSpeed = minSpeed + ((maxSpeed - minSpeed) * percent * timeModifier);        
        this.moveSpeed = scaledSpeed;
        scaledSpeed *= shaderPanSpeedMult;

        //rend.material.SetFloat("_myXSpeed", scaledSpeed * shaderPanSpeedMult);

        foreach (Renderer current in allRends)
        {
            current.material.SetFloat("_myXSpeed", scaledSpeed);
        }
    }

    /// <summary>
    /// Sets the time modifier
    /// </summary>
    public void SetTimeModifier(float newTimeModifier)
    {
        timeModifier = newTimeModifier;
        setSpeed(currentPercent);
    }


    public void ClearAllObjects()
    {
        targets.Clear();
        deleteMark.Clear();
    }
}
