using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSnap : MonoBehaviour
{
    public GameObject prevChild;
    public GameObject targetTile;
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
        targetTile= prevChild.GetComponentInParent<tileInfo>().gameObject;
        offset = new Vector3(0, .02f, 0);
        this.gameObject.transform.position = targetTile.transform.position+offset;
        
        Destroy(prevChild);
        prevChild = null;
        isInair = false;
        this.gameObject.transform.parent = targetTile.transform;
    }
}
