using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [TagSelector]
    public string[] filterTags = new string[] { };

    private HashSet<string> allowedTags = new HashSet<string>();
    public float moveSpeed = 1f;
    public Vector3 moveDir = Vector3.zero;//direction to move in    
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
        foreach (GameObject current in targets)
        {
            if (current.GetComponent<ObjectData>().gameObjectData["Moving"] == this.gameObject)
            {
                current.transform.position = Vector3.MoveTowards(current.transform.position, new Vector3(transform.position.x + moveDir.x, current.transform.position.y, transform.position.z + moveDir.z), Time.deltaTime * moveSpeed);
            }else if(current.GetComponent<ObjectData>().gameObjectData["Moving"]=null)
            {

            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        targets.Add(collision.gameObject);
        ObjectData data = collision.gameObject.GetComponent<ObjectData>();
        if (data!=null && allowedTags.Contains(collision.gameObject.tag) && data.gameObjectData.ContainsKey("Moving"))
        {
            data.gameObjectData.Add("Moving", this.gameObject);
        }

    }
    private void OnDrawGizmosSelected()
    {
        DrawArrow.ForGizmo(transform.position, moveDir);
    }
}
