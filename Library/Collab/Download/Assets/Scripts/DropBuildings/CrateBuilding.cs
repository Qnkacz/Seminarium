using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBuilding : MonoBehaviour
{
    private SphereCollider thisCollider;
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
                if(!TreesInarea.Contains(other.gameObject))
                {
                other.gameObject.GetComponent<Tree>().SetAbleToCut();
                other.gameObject.GetComponent<Tree>().asignedCrateBuilding = this;
                TreesInarea.Add(other.gameObject);
                }
                
            }
            if(other.gameObject.tag=="tile")
            {
                other.gameObject.GetComponent<Soil>().asignedCrateBuilding = this;
            }
    }
    private void Start()
    {
        thisCollider = this.gameObject.GetComponent<SphereCollider>();
        thisCollider.radius = 80f;
        ExtendedStorage = baseWoodStorage;
        StorageBuildingsCount = 0;
        StartCoroutine(SwitchCollider());
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
    IEnumerator SwitchCollider()
    {
        while(true)
        {
            if (thisCollider.enabled == true)
            {
                thisCollider.enabled = false;
                yield return new WaitForSecondsRealtime(5f);
            }
            else
            {
                thisCollider.enabled = true;
                yield return new WaitForEndOfFrame();
            }
        }
    }
    IEnumerator GrowCollider()
    {
        while(thisCollider.radius<80)
        {
            thisCollider.radius += .1f;
            yield return new WaitForSeconds(.1f);
        }
        if(thisCollider.radius>=80)
        {
            thisCollider.radius = 80f;
            StopCoroutine(GrowCollider());
        }
        
    }
}
