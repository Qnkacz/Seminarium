using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSnap : MonoBehaviour
{
    public GameObject prevChild;
    public GameObject targetTile;
    public GroundPlacementController GPC;
    public Vector3 offset;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Dotykam: "+other.gameObject.name);
        if(other.gameObject.tag=="GroundBox")
        {
            prevChild = null;
        }
        else
        {
            prevChild = other.gameObject;
        }
        

    }
    public void Snap()
    {
        targetTile= prevChild.GetComponentInParent<tileInfo>().gameObject;
        offset = new Vector3(0, 0, .02f);
        this.gameObject.transform.position = targetTile.transform.position;
        
        Destroy(prevChild);
        prevChild = null;
        
    }
    public void setpos()
    {

    }
}
