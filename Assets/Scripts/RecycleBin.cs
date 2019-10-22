using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// A recycle bin that takes objects
/// </summary>
public class RecycleBin : MonoBehaviour
{
    public TrashTypes BinTrashType = TrashTypes.Compost;
    public BoxCollider TrashBoxCollider;

    public Transform SpawnDisplayPointsLocation;
    public GameObject ReceivedPointsPrefabObject_PO;

    public TextMeshProUGUI RecycleTypeText;
    public float textRotateSpeed = .5f;


    private PlayerData playerData;
    private GameObject headsetAlias;

    private void Awake()
    {
        playerData = GameObject.FindObjectOfType(typeof(PlayerData)) as PlayerData;
        headsetAlias = GameObject.FindGameObjectWithTag("Player");
        Initialize(BinTrashType);
    }

    private void Update()
    {
        // RecycleTypeText.transform.LookAt(transform.position - headsetAlias.transform.position);
        Quaternion lookGoal = Quaternion.LookRotation(transform.position - headsetAlias.transform.position);
        RecycleTypeText.transform.rotation = Quaternion.SlerpUnclamped(RecycleTypeText.transform.rotation, lookGoal,textRotateSpeed*Time.deltaTime);
    }

    public void Initialize(TrashTypes trashType)
    {
        BinTrashType = trashType;
        RecycleTypeText.text = trashType.ToString();
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

            TrashItem tossedTrashItem = other.GetComponent<TrashItem>();

            if(tossedTrashItem.GetTrashType() == BinTrashType)
            {
                playerData.AddPoints(tossedTrashItem.GetPointValue());
                Debug.Log("adding points");
                SpawnAndDisplayPoints(true, tossedTrashItem.GetPointValue());
            }
            else
            {
                playerData.RemovePoints(tossedTrashItem.GetPointValue());
                Debug.Log("removing points");
                SpawnAndDisplayPoints(false, tossedTrashItem.GetPointValue());
            }

            Destroy(other.gameObject); //TODO stack up instead?
        }

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
