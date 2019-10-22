using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum USMapConditions
{
    SelectingState = 0,
    SelectingCounty = 1,
    Unselectable = 7,
}


/// <summary>
/// The US Map object
/// </summary>
public class USMap : MonoBehaviour
{
    public Transform CountyListLocation;
    public GameObject CountyOnMapPrefabObject_PO;

    private USMapConditions currentUSMapCondition = USMapConditions.SelectingState;
    private State selectedState = null;
    private County selectedCounty = null;

    private List<CountyOnMapPrefabObject> spawnedCountyOnMapPrefabObjects;

    private const float COUNTY_SPAWN_DIFF_Y = 0.6f;

    /// <summary>
    /// Checks for any presses on the map
    /// </summary>
    public void CheckForPressOnMap()
    {
        switch(currentUSMapCondition)
        {
            case (USMapConditions.SelectingState):
                Debug.Log("checking while selecting state");
                foreach (StateOnMap som in GameObject.FindObjectsOfType<StateOnMap>())
                {
                    if (som.CheckForPressed())
                    {
                        SelectState(som);
                        break;
                    }
                }
                break;
            case (USMapConditions.SelectingCounty):
                Debug.Log("checking while selecting county");
                foreach (CountyOnMapPrefabObject com_po in GameObject.FindObjectsOfType<CountyOnMapPrefabObject>())
                {
                    if (com_po.CheckForPressed())
                    {
                        //SelectState(som);
                        break;
                    }
                }
                break;
        }

        
    }


    /// <summary>
    /// Selects the state
    /// </summary>
    private void SelectState(StateOnMap som)
    {
        Debug.Log("Selecting state!");

        selectedState = som.StateInfo;
        int spawnedObjects = 0;
        
        if(som.StateInfo.HasCounties())
        {
            foreach (County county in som.StateInfo.Counties)
            {
                Debug.Log("spawning county");
                CountyOnMapPrefabObject com_po = Instantiate(CountyOnMapPrefabObject_PO).GetComponent<CountyOnMapPrefabObject>();
                com_po.transform.SetParent(CountyListLocation, false);
                com_po.transform.position -= new Vector3(0.0f, COUNTY_SPAWN_DIFF_Y * spawnedObjects, 0.0f); //moves down the county list
                com_po.Initialize(county);
                spawnedObjects++;
            }
        }
        else
        {
            Debug.LogWarning("There were no counties for the selected state!");
        }

    }

    /// <summary>
    /// Goes back to the selecting state screen
    /// </summary>
    public void GoBackToSelectState()
    {
        currentUSMapCondition = USMapConditions.SelectingState;
    }

}
