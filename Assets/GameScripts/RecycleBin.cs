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

        if(other.GetComponent(typeof(TrashItem)))
        {
            Debug.Log("we got trash");

            TrashItem tossedTrashItem = other.GetComponent(typeof(TrashItem)) as TrashItem;

            if(tossedTrashItem.TrashType == BinTrashType)
            {
                playerData.AddPoints(tossedTrashItem.PointValue);
                Debug.Log("adding points");
            }
            else
            {
                playerData.RemovePoints(tossedTrashItem.PointValue);
                Debug.Log("removing points");
            }

            Destroy(other.gameObject); //TODO stack up instead?
        }


        //if(other.gameObject.GetType() == typeof(TrashItem))
        //{
        //    Debug.Log("is trash");
        //    //TrashItem trash = (other.gameObject as TrashItem);
        //}
    }


}
