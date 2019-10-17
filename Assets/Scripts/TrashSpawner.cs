using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns trash randomly
/// </summary>
public class TrashSpawner : MonoBehaviour
{
    public List<TrashItemInfo> SpawnableTrashItemInfos;

    public GameObject TrashItem_PO;
    public Transform SpawnLocation;
    public float minTimeBetweenTrashSpawns = 0.2f;
    public float maxTimeBetweenTrashSpawns = 0.8f;


    private float timeSinceLastTrashSpawned = 0.0f;
    private float timeOfNextTrashSpawn = 0.0f;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        timeOfNextTrashSpawn = GetNextTrashSpawnTime();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastTrashSpawned += Time.deltaTime;
        if(timeSinceLastTrashSpawned >= timeOfNextTrashSpawn)
        {
            SpawnNewTrashItem();
            timeSinceLastTrashSpawned = 0.0f;
            timeOfNextTrashSpawn = GetNextTrashSpawnTime();
        }


    }

    /// <summary>
    /// Gets the time we'll have to wait for the next trash to be spawned
    /// </summary>
    private float GetNextTrashSpawnTime()
    {
        return Random.Range(minTimeBetweenTrashSpawns, maxTimeBetweenTrashSpawns);
    }

    /// <summary>
    /// Spawns a new trash item at the designated location from the list of acceptable items
    /// </summary>
    private void SpawnNewTrashItem()
    {
        if (SpawnableTrashItemInfos.Count >= 1)
        {
            TrashItem spawnedTrashItem = Instantiate(TrashItem_PO).GetComponent<TrashItem>();
            spawnedTrashItem.Initialize(SpawnableTrashItemInfos[Random.Range(0, SpawnableTrashItemInfos.Count)]); //spawn from a random choice in the range
            spawnedTrashItem.transform.position = SpawnLocation.position;
        }
        else
        {
            Debug.LogError("No spawnable trash item infos found in list!");
        }

    }

}
