using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A recycle bin that takes objects
/// </summary>
public class RecycleBin : MonoBehaviour
{
    public TrashTypes BinTrashType = TrashTypes.Compost;
    public BoxCollider TrashBoxCollider;

    public Transform SpawnDisplayPointsLocation;
    public GameObject ReceivedPointsPrefabObject_PO;


    private PlayerData playerData;

    private void Awake()
    {
        playerData = GameObject.FindObjectOfType(typeof(PlayerData)) as PlayerData;
    }

    /// <summary>
    /// When an object enters the trigger box
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");

        if(other.GetComponent(typeof(TrashItemMesh)))
        {
            Debug.Log("we got trash");

            TrashItem tossedTrashItem = other.GetComponent<TrashItemMesh>().GetCorrespondingTrashItem();

            if(tossedTrashItem.TrashType == BinTrashType)
            {
                playerData.AddPoints(tossedTrashItem.PointValue);
                Debug.Log("adding points");
                SpawnAndDisplayPoints(true, tossedTrashItem.PointValue);
            }
            else
            {
                playerData.RemovePoints(tossedTrashItem.PointValue);
                Debug.Log("removing points");
                SpawnAndDisplayPoints(false, tossedTrashItem.PointValue);
            }

            Destroy(other.gameObject); //TODO stack up instead?
        }


        //if(other.gameObject.GetType() == typeof(TrashItem))
        //{
        //    Debug.Log("is trash");
        //    //TrashItem trash = (other.gameObject as TrashItem);
        //}
    }

    /// <summary>
    /// Spawns and displays the received points prefab object
    /// </summary>
    private void SpawnAndDisplayPoints(bool gainedPoints, int pointValue)
    {
        ReceivedPointsPrefabObject rppo = Instantiate(ReceivedPointsPrefabObject_PO).GetComponent<ReceivedPointsPrefabObject>();
        rppo.transform.position = SpawnDisplayPointsLocation.position;
        rppo.Initialize(gainedPoints, pointValue);
    }

}
