using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToRoadSnap : MonoBehaviour
{
    public bool isInAir=true;
    public GameObject roadToSnap;
    public bool snapped = false;
    public Vector3 offset;
    public Vector3 directionVector;
    private Vector3 normalizedDirectionVector;
    int snapto;
    [Header("parenting")]
    public GameObject prevChild;
    public GameObject targetTile;
    public enum SnapDirection
    {
        top, left, bot ,right
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="road")
        {
            snapped = true;
            roadToSnap = other.gameObject;
            
            //Debug.Log(snapto);
            //offset = other.gameObject.transform.position + new Vector3(1, 0, 0);
            //transform.position = offset;
        }
       if(isInAir)
        {
            if (other.gameObject.tag == "tree" || other.gameObject.tag=="building")
            {
                prevChild = other.gameObject;
            }
            else
            {
                prevChild = null;
            }
        }
    }
    private void LateUpdate()
    {
        if(isInAir)
        {
            if(snapped)
            {
                directionVector = this.gameObject.transform.position - roadToSnap.gameObject.transform.position;
                normalizedDirectionVector = new Vector3(directionVector.x, directionVector.y).normalized;

                //Debug.Log(normalizedDirectionVector);
                if (normalizedDirectionVector.x > 0 && normalizedDirectionVector.y < 1)
                {
                    snapto = (int)SnapDirection.right;
                }
                if (normalizedDirectionVector.x > 0 && normalizedDirectionVector.z == 1.0f)
                {
                    snapto = (int)SnapDirection.top; // nie dziala
                }
                if (normalizedDirectionVector.x < 0 && normalizedDirectionVector.y < 1)
                {
                    snapto = (int)SnapDirection.left;
                }
                switch(snapto)
                {
                    case 1:
                        offset = roadToSnap.gameObject.transform.position + new Vector3(-1, 0, 0);
                        transform.position = offset;
                        break;
                    case 3:
                        offset = roadToSnap.gameObject.transform.position + new Vector3(1, 0, 0);
                        transform.position = offset;
                        break;
                }
            }
        }
    }
    public void setInair()
    {
        isInAir = false;
        transform.position = offset;
    }
    public void Snap()
    {
        targetTile = prevChild.GetComponentInParent<tileInfo>().gameObject;
        Destroy(prevChild);
        this.gameObject.transform.parent = targetTile.transform;
        prevChild = null;
        isInAir = false;
        transform.position = offset;
    }
}
