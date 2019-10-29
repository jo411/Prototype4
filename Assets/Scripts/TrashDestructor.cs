using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A trigger box for destroying objects that hit the ground
/// </summary>
public class TrashDestructor : MonoBehaviour
{

    public GameObject effectPrefab;
    public AudioClip missSound;


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
        if (other.GetComponent(typeof(TrashItem)))
        {
            TrashItem tossedTrashItem = other.GetComponent<TrashItem>();

            playerData.RemovePoints(tossedTrashItem.GetPointValue());

            GameObject effect = Instantiate(effectPrefab, transform);
            AudioManager.Instance.Play(missSound, transform);

            Destroy(other.gameObject);
        }

    }


}
