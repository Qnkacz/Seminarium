using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSnap : MonoBehaviour
{
    public GameObject prevChild;
    public GameObject targetTile;
    public GroundPlacementController GPC;
    public Vector3 offset;
    public bool isInair = true;
    private void OnTriggerEnter(Collider other)
    {
        if(isInair)
        {
            if (other.gameObject.tag == "tree")
            {

                prevChild = other.gameObject;
            }
            else
            {
                prevChild = null;
            }
        }
        else
        {

        }
        

    }
    public void Snap()
    {
        if(prevChild!=null)
        {
            if(prevChild.tag=="tree")
            {
                targetTile = prevChild.GetComponentInParent<tileInfo>().gameObject;
                targetTile.GetComponent<tileInfo>().hasRoad = true;
                offset = new Vector3(0, .02f, 0);
                this.gameObject.transform.position = targetTile.transform.position + offset;

                Destroy(prevChild);
                prevChild = null;
                isInair = false;
                
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
        GlobalVariables.g.surface.BuildNavMesh();
    }
}
