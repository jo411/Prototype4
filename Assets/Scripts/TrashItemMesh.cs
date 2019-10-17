using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashItemMesh : MonoBehaviour
{
    private TrashItem correspondingTrashItem;

    private void Awake()
    {
        correspondingTrashItem = this.GetComponentInParent<TrashItem>();

        if (correspondingTrashItem == null)
            Debug.LogWarning("NO CORRESPONDING TRASH ITEM");
    }

    /// <summary>
    /// Gets the corresponding trash item.
    /// </summary>
    public TrashItem GetCorrespondingTrashItem()
    {
        return correspondingTrashItem;
    }
}
