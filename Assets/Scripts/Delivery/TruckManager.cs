using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoBehaviour
{
    public BuildingToRoad thisBtr;
    public CrateBuilding thisCB;
    public Vector3 StartLocation;
    private RoadSnap spawnroad;
    public GameObject truck;
    public List<GameObject> spawnedtrucks = new List<GameObject>();
    public GameObject t;
    Truck tt;
    public void sendTruck()
    {
        if (StartLocation == null || spawnroad==null)
        {
            for (int i = 0; i < thisBtr.adjtiles.Length; i++)
            {
                if (thisBtr.adjtiles[i].GetComponent<tileInfo>().hasRoad == true)
                {
                    StartLocation = thisBtr.adjtiles[i].GetComponent<Soil>().child.transform.position;
                    spawnroad = thisBtr.adjtiles[i].GetComponent<Soil>().child.GetComponent<RoadSnap>();
                }
            }
        }
        if (!spawnroad.hasTruck)
        {
            t = GameObject.Instantiate(truck, StartLocation + new Vector3(0, .5f, 0), Quaternion.identity);
            spawnedtrucks.Add(t);
            t.GetComponent<Truck>().DriveTo(GlobalVariables.g.Destination);
        }
            
        if(t!=null)
        {
            
            tt = t.GetComponent<Truck>();
            if (tt.currCapacity >= thisCB.WoodStored)
            {
                tt.currLoad = thisCB.WoodStored;
                thisCB.WoodStored = 0;
            }
            else
            {
                tt.currLoad = tt.currCapacity;
                thisCB.WoodStored = thisCB.WoodStored - tt.currCapacity;
            }
            tt.ActiaveBlocks();
            GlobalVariables.g.WoodValue -= tt.currLoad;
            BossScript.BS.ChangeWood();
        }
        t = null;
        tt = null;
    }
    public void getCrateComponents(CrateBuilding cb, BuildingToRoad btr)
    {
        thisBtr = btr;
        thisCB = cb;
    }
    public void AddToTruck(int value)
    {
        thisCB.WoodStored -= value;
        tt.currLoad = value;
    }
}
