using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [TagSelector]
    public string[] filterTags = new string[] { };

    private HashSet<string> allowedTags = new HashSet<string>();
    public float moveSpeed = 1f;
    public Vector3 moveTo = Vector3.zero;//direction to move in    
    public HashSet<GameObject> targets = new HashSet<GameObject>();   
    public List<GameObject> deleteMark = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(string current in filterTags)
        {
            allowedTags.Add(current);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        processRemoveBuffer();
        foreach (GameObject current in targets)
        {
            ObjectData data = current.GetComponent<ObjectData>();
            if (data == null) { return; }
            if (data.gameObjectData.ContainsKey("Moving") && data.gameObjectData["Moving"] == this.gameObject)
            {
                current.transform.position = Vector3.MoveTowards(current.transform.position, new Vector3(transform.position.x + moveTo.x, current.transform.position.y, transform.position.z + moveTo.z), Time.deltaTime * moveSpeed);
                //if(moveTo.x != 0)
                //{
                //    if (transform.position.x + moveTo.x == current.transform.position.x)
                //    {
                //      // deleteMark.Add(current.gameObject);
                //    }
                //}else
                //{
                //    if (transform.position.z + moveTo.z == current.transform.position.z)
                //    {
                //       // deleteMark.Add(current.gameObject);
                //    }
                //}
            }else if(data.gameObjectData.ContainsKey("Moving"))
            {
               // current.GetComponent<ObjectData>().gameObjectData["Moving"] = this.gameObject;
                current.transform.position = Vector3.MoveTowards(current.transform.position, new Vector3(transform.position.x + moveTo.x, current.transform.position.y, transform.position.z + moveTo.z), Time.deltaTime * moveSpeed);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        targets.Add(collision.gameObject);
        ObjectData data = collision.gameObject.GetComponent<ObjectData>();
        if(data != null && data.gameObjectData.ContainsKey("Moving"))
        {
            if(data.gameObjectData["Moving"]!=this.gameObject)
            {
                data.gameObjectData.Remove("Moving");
                data.gameObjectData.Add("Moving", this.gameObject);                
            }
        }else if (data!=null && allowedTags.Contains(collision.gameObject.tag) && !data.gameObjectData.ContainsKey("Moving"))
        {
            data.gameObjectData.Add("Moving", this.gameObject);
        }
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
            ObjectData data = current.gameObject.GetComponent<ObjectData>();
            if(data.gameObjectData.ContainsKey("Moving") && data.gameObjectData["Moving"]==this.gameObject)
            {
                current.GetComponent<ObjectData>().gameObjectData.Remove("Moving");
            }           
        }
        deleteMark.Clear();
    }
    private void OnDrawGizmosSelected()
    {
        DrawArrow.ForGizmo(transform.position, moveTo);
    }
}
