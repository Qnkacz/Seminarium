using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageSnap : MonoBehaviour
{
    public GameObject targetTile;
    public Soil targettile_soil;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="tile")
        {
            if(other.gameObject.GetComponent<Soil>().child==null)
            {
                targettile_soil = other.gameObject.GetComponent<Soil>();
                targettile_soil.child = this.gameObject;
                targetTile = other.gameObject;
            }
        }
    }
   
    public void Snap()
    {
        if (targettile_soil!=null) this.gameObject.transform.parent.transform.position = targetTile.transform.position;
        else Destroy(this.gameObject.transform.parent.gameObject);

        GlobalMoneymanager.GMM.ChangeMoney(GlobalMoneymanager.GMM.cost_Storage);
    }
   
}
