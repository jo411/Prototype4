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

    public GameObject effectPrefab;
    public AudioClip scoreSound;
    public AudioClip missSound;

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
        if(other.GetComponent(typeof(TrashItem)))
        {
            TrashItem tossedTrashItem = other.GetComponent<TrashItem>();

            if(tossedTrashItem.GetTrashType() == BinTrashType)
            {
                playerData.AddPoints(tossedTrashItem.GetPointValue());
                SpawnAndDisplayPoints(true, tossedTrashItem.GetPointValue());
            }
            else
            {
                playerData.RemovePoints(tossedTrashItem.GetPointValue());
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
        if(gainedPoints)
        {
            Vector3 offset = new Vector3(0, 0, .5f);
            GameObject effect = Instantiate(effectPrefab, transform);
            effect.transform.position += offset;
            Destroy(effect, 5);
            AudioManager.Instance.Play(scoreSound, transform);
        }else
        {
            AudioManager.Instance.Play(missSound, transform);
        }

        ReceivedPointsPrefabObject rppo = Instantiate(ReceivedPointsPrefabObject_PO).GetComponent<ReceivedPointsPrefabObject>();
        rppo.transform.position = SpawnDisplayPointsLocation.position;
        rppo.Initialize(gainedPoints, pointValue);
    }

}
