using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TrashItemInfo", menuName = "TrashItems/TrashItemInfo", order = 1)]
public class TrashItemInfo : ScriptableObject
{
    public GameObject TrashItemModel;
    public Mesh AssociatedMesh;

    public string TrashItemNameEN = "";
    public TrashTypes TrashType = TrashTypes.Compost;
    public int PointValue = 1;
}
