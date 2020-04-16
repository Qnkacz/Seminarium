using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingToRoad : MonoBehaviour
{
    public GameObject prevChild;
    public GameObject targetTile;
    public GroundPlacementController GPC;
    public Vector3 offset;
    public bool isInair = true;
    public bool canFunction=false;
    public tileInfo tileInfo;
    public tileInfo[] adjtiles = new tileInfo[4];
    public GameObject exclamation;
    public GameObject childExclamation;
    private void OnTriggerEnter(Collider other)
    {
        if (isInair)
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
        if (prevChild != null)
        {
            tileInfo = prevChild.GetComponentInParent<tileInfo>();
            targetTile = tileInfo.gameObject;
            
            offset = new Vector3(0, .02f, 0);
            if (this.gameObject.tag == "MainBuilding")
            {
                offset = new Vector3(0, .02f, .3f);
            }
            this.gameObject.transform.position = targetTile.transform.position + offset;

            Destroy(prevChild);
            prevChild = null;
            isInair = false;
            GetAdjTiles();
            if(this.gameObject.tag=="crate")
            {
                this.gameObject.GetComponent<SphereCollider>().enabled = true;
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
        

    }
    public IEnumerator SeeifFunctional()
    {
        while(true)
        {
            if (adjtiles[0].hasRoad == true || adjtiles[1].hasRoad == true || adjtiles[2].hasRoad == true || adjtiles[3].hasRoad == true)
            {
                canFunction = true;
                DisableExclamationmark();
            }
           if(!canFunction)
           {
                SpawnExplamationMark();
           }
            
            yield return new WaitForSecondsRealtime(1f);
        }
    }
    public void GetAdjTiles()
    {
        if(tileInfo.myArrayX+1<=MapGenerator.mapGenerator.tilesArr.GetLength(0)) //prawo
        {
            adjtiles[0] = MapGenerator.mapGenerator.tilesArr[tileInfo.myArrayX + 1, tileInfo.myArrayY].GetComponent<tileInfo>();
        }
        if (tileInfo.myArrayX - 1 > 0) //lewo
        {
            adjtiles[1] = MapGenerator.mapGenerator.tilesArr[tileInfo.myArrayX - 1, tileInfo.myArrayY].GetComponent<tileInfo>();
        }
        if (tileInfo.myArrayY + 1 <= MapGenerator.mapGenerator.tilesArr.GetLength(1)) //gora
        {
            adjtiles[2] = MapGenerator.mapGenerator.tilesArr[tileInfo.myArrayX, tileInfo.myArrayY + 1].GetComponent<tileInfo>();
        }
        if (tileInfo.myArrayY - 1 >= 0) //dol
        {
            adjtiles[3] = MapGenerator.mapGenerator.tilesArr[tileInfo.myArrayX, tileInfo.myArrayY - 1].GetComponent<tileInfo>();
        }
        StartCoroutine(SeeifFunctional());
    }

    public void SpawnExplamationMark()
    {
        if(childExclamation==null)
        {
            Instantiate(exclamation, this.transform);
            childExclamation = this.gameObject.GetComponentInChildren<ObjectFloat>().gameObject;
        }
        else
        {
            childExclamation.SetActive(true);
        }

    }
    public void DisableExclamationmark()
    {
        if(childExclamation!=null)
        {
            childExclamation.SetActive(false);
        }
        
    }
    
}
