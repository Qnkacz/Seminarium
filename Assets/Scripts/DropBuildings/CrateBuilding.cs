using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBuilding : MonoBehaviour
{
    public List<GameObject> TreesInarea=new List<GameObject>();
    public int WoodStored;
    public int baseWoodStorage = 4000;
    public int ExtendedStorage;
    private void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.tag == "tree")
            {
                other.gameObject.GetComponent<Tree>().SetAbleToCut();
                other.gameObject.GetComponent<Tree>().asignedCrateBuilding = this;
                TreesInarea.Add(other.gameObject);
            }
    }
    
}
