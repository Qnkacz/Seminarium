using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBuilding : MonoBehaviour
{
    public List<GameObject> TreesInarea=new List<GameObject>();
    public List<GameObject> tilesInArea = new List<GameObject>();
    public int WoodStored;
    public int baseWoodStorage = 4000;
    public int ExtendedStorage;
    public int StorageBuildingsCount;
    public int WoodOverflow;
    public int woodperExtention;
    private void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.tag == "tree")
            {
                other.gameObject.GetComponent<Tree>().SetAbleToCut();
                other.gameObject.GetComponent<Tree>().asignedCrateBuilding = this;
                TreesInarea.Add(other.gameObject);
            }
            if(other.gameObject.tag=="tile")
            {
                other.gameObject.GetComponent<Soil>().asignedCrateBuilding = this;
             }
    }
    private void Start()
    {
        ExtendedStorage = baseWoodStorage;
        StorageBuildingsCount = 0;
    }
    public void Storage_add()
    {
        StorageBuildingsCount++;
        ExtendedStorage = baseWoodStorage + StorageBuildingsCount * woodperExtention;
    }
    public void Storage_substr()
    {
        StorageBuildingsCount--;
        ExtendedStorage = baseWoodStorage + StorageBuildingsCount * woodperExtention;
    }
    public IEnumerator overflowWoodDestroy()
    {
        while(WoodOverflow>0)
        {
            yield return new WaitForSeconds(3f);
            WoodOverflow -= 100;
            if (WoodOverflow < 0) WoodOverflow = 0;
        }
    }
    public int GetWoodStored()
    {
        return WoodStored;
    }
}
