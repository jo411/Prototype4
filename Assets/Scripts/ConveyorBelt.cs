using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [TagSelector]
    public string[] filterTags = new string[] { };

    public float moveSpeed = 1f;
    public Vector3 moveDir = Vector3.zero;//direction to move in    
    public HashSet<GameObject> targets = new HashSet<GameObject>();   
    public List<GameObject> deleteMark = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        targets.Add(collision.gameObject);
        ObjectData data = collision.gameObject.GetComponent<ObjectData>();
        if (data!=null && data.gameObjectData)
        {
            collision.gameObject.getc
        }

    }
    private void OnDrawGizmosSelected()
    {
        DrawArrow.ForGizmo(transform.position, moveDir);
    }
}
