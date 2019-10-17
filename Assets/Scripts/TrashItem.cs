using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A trash item
/// </summary>
public class TrashItem : MonoBehaviour
{
    //object references
    public Transform Meshes;

    private string trashItemNameEN = "";
    private TrashTypes trashType = TrashTypes.Compost;
    private int pointValue = 1;


    public void Initialize(TrashItemInfo trashItemInfo)
    {
        this.name = "TrashItem_" + trashItemInfo.TrashItemNameEN;

        Instantiate(trashItemInfo.TrashItemModel).transform.SetParent(Meshes); //spawns and sets location of model
        this.GetComponent<MeshCollider>().sharedMesh = trashItemInfo.AssociatedMesh; //assign mesh

        trashItemNameEN = trashItemInfo.TrashItemNameEN;
        trashType = trashItemInfo.TrashType;
        pointValue = trashItemInfo.PointValue;
    }


    #region Getters
    public string GetItemNameEN()
    {
        return trashItemNameEN;
    }

    public TrashTypes GetTrashType()
    {
        return trashType;
    }

    public int GetPointValue()
    {
        return pointValue;
    }

    #endregion Getters

}
