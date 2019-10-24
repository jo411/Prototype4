using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private bool spawningTrash = false;

    private List<TrashItemInfo> countySpawnableTrashItemInfos;
    private List<TrashTypes> countyTrashTypes;

    private List<GameObject> spawnedTrash;

    public bool useRandomRotation;
    public float timeMultiplier = 1;

    private void Awake()
    {
        Initialize();
        countySpawnableTrashItemInfos = new List<TrashItemInfo>();
        countyTrashTypes = new List<TrashTypes>();

        spawnedTrash = new List<GameObject>();
    }

    private void Initialize()
    {
        timeOfNextTrashSpawn = GetNextTrashSpawnTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawningTrash)
            return;

        timeSinceLastTrashSpawned += Time.deltaTime*timeMultiplier;
        if(timeSinceLastTrashSpawned >= timeOfNextTrashSpawn)
        {
            SpawnNewTrashItem();
            timeSinceLastTrashSpawned = 0.0f;
            timeOfNextTrashSpawn = GetNextTrashSpawnTime();
        }


    }

    /// <summary>
    /// Starts the trash spawner
    /// </summary>
    public void StartTrashSpawner(County countyInfo)
    {
        countySpawnableTrashItemInfos = new List<TrashItemInfo>();

        if (countyInfo.RecyclesAluminum)
        {
            foreach (TrashItemInfo tii in SpawnableTrashItemInfos.Where(x => x.TrashType == TrashTypes.Aluminum))
                countySpawnableTrashItemInfos.Add(tii);
            countyTrashTypes.Add(TrashTypes.Aluminum);
        }

        if (countyInfo.RecyclesCompost)
        {
            foreach (TrashItemInfo tii in SpawnableTrashItemInfos.Where(x => x.TrashType == TrashTypes.Compost))
                countySpawnableTrashItemInfos.Add(tii);
            countyTrashTypes.Add(TrashTypes.Compost);
        }

        if (countyInfo.RecyclesElectronic)
        {
            foreach (TrashItemInfo tii in SpawnableTrashItemInfos.Where(x => x.TrashType == TrashTypes.Electronic))
                countySpawnableTrashItemInfos.Add(tii);
            countyTrashTypes.Add(TrashTypes.Electronic);
        }

        if (countyInfo.RecyclesGlass)
        {
            foreach (TrashItemInfo tii in SpawnableTrashItemInfos.Where(x => x.TrashType == TrashTypes.Glass))
                countySpawnableTrashItemInfos.Add(tii);
            countyTrashTypes.Add(TrashTypes.Glass);
        }

        if (countyInfo.RecyclesNonRecyclable)
        {
            foreach (TrashItemInfo tii in SpawnableTrashItemInfos.Where(x => x.TrashType == TrashTypes.NonRecyclable))
                countySpawnableTrashItemInfos.Add(tii);
            countyTrashTypes.Add(TrashTypes.NonRecyclable);
        }

        if (countyInfo.RecyclesPaper)
        {
            foreach (TrashItemInfo tii in SpawnableTrashItemInfos.Where(x => x.TrashType == TrashTypes.Paper))
                countySpawnableTrashItemInfos.Add(tii);
            countyTrashTypes.Add(TrashTypes.Paper);
        }

        if (countyInfo.RecyclesPlastic)
        {
            foreach (TrashItemInfo tii in SpawnableTrashItemInfos.Where(x => x.TrashType == TrashTypes.Plastic))
                countySpawnableTrashItemInfos.Add(tii);
            countyTrashTypes.Add(TrashTypes.Plastic);
        }

        if (countySpawnableTrashItemInfos != null && countySpawnableTrashItemInfos.Count > 0)
            spawningTrash = true;
        else
            Debug.LogWarning("No trash to spawn!");

    }

    /// <summary>
    /// Stops the trash spawner
    /// </summary>
    public void StopTrashSpawner()
    {
        spawningTrash = false;

        foreach(GameObject go in spawnedTrash)
        {
            Destroy(go);
        }
        spawnedTrash.Clear();
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
            if(useRandomRotation)
            {
                spawnedTrashItem.transform.rotation = Random.rotation;
            }

            //Set the trash type to non-recyclable if the trash isn't available in this county
            if(!countyTrashTypes.Contains(spawnedTrashItem.GetTrashType()))
            {
                spawnedTrashItem.SetAsNonRecyclable();
            }

            spawnedTrash.Add(spawnedTrashItem.gameObject);
        }
        else
        {
            Debug.LogError("No spawnable trash item infos found in list!");
        }

    }
    public void setSpeed(float percent)
    {
        timeMultiplier = Mathf.Max(.05f,percent);
    }


}
