using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSnap : MonoBehaviour
{
    public Soil dummy;
    public tileInfo tileInfo;
    public GameObject targetTile;
    public bool hasTruck;
    public Soil s;
    public GameObject DestinationRaod;
    public Soil[] adjtiles = new Soil[4];
    bool canbuild;
    private void OnTriggerEnter(Collider other)
    {
            if(other.gameObject.tag=="truck")
            {
            hasTruck = true;
            }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "truck")
        {
            hasTruck = false;
        }
    }

    private void Start()
    {
        hasTruck = false;
        canbuild = false;
        targetTile = this.gameObject.transform.parent.gameObject;
        if (targetTile!=null)
        {
            tileInfo = targetTile.GetComponent<tileInfo>();
            s = targetTile.GetComponent<Soil>();
            canPut();
            if (s.child == null || s.child.tag == "Storage" || s.child.tag == "tree")
            {
                if (canbuild)
                {
                    this.transform.position = targetTile.transform.position;
                    if (s.child != null) Destroy(s.child);
                    s.child = this.gameObject;
                    targetTile.GetComponent<tileInfo>().hasRoad = true;
                    GlobalMoneymanager.GMM.ChangeMoney(GlobalMoneymanager.GMM.cost_Road);
                    this.gameObject.SetActive(true);
                    buildnavmesh();
                    Invoke("buildnavmesh", .03f);
                    GlobalVariables.g.roadCount++;
                }
                else Destroy(this.gameObject);
            }
            else Destroy(this.gameObject);
        }
        else
        {
          Destroy(this.gameObject);
        }

        
    }

    private void buildnavmesh()
    {
        GlobalVariables.g.surface.BuildNavMesh();
    }

   
    IEnumerator buildroad()
    {
        yield return new WaitForSeconds(.05f);
        GlobalVariables.g.surface.BuildNavMesh();
    }
    public void canPut()
    {
        if (tileInfo.myArrayX + 1 <= MapGenerator.mapGenerator.tilesArr.GetLength(0)) //prawo
        {
            adjtiles[0] = MapGenerator.mapGenerator.tilesArr[tileInfo.myArrayX + 1, tileInfo.myArrayY].GetComponent<Soil>();
        }
        if (tileInfo.myArrayX - 1 > 0) //lewo
        {
            adjtiles[1] = MapGenerator.mapGenerator.tilesArr[tileInfo.myArrayX - 1, tileInfo.myArrayY].GetComponent<Soil>();
        }
        if (tileInfo.myArrayY + 1 <= MapGenerator.mapGenerator.tilesArr.GetLength(1)) //gora
        {
            adjtiles[2] = MapGenerator.mapGenerator.tilesArr[tileInfo.myArrayX, tileInfo.myArrayY + 1].GetComponent<Soil>();
        }
        if (tileInfo.myArrayY - 1 >= 0) //dol
        {
            adjtiles[3] = MapGenerator.mapGenerator.tilesArr[tileInfo.myArrayX, tileInfo.myArrayY - 1].GetComponent<Soil>();
        }

        for (int i = 0; i < adjtiles.Length; i++)
        {
            if (adjtiles[i] == null)
            {
                adjtiles[i] = dummy;
            }
        }
        if (GlobalVariables.g.roadCount == 0) canbuild = true;
        else
        {
            for (int i = 0; i < adjtiles.Length; i++)
            {
                if(adjtiles[i]==null)
                {
                    adjtiles[i] = dummy;
                }
                if(adjtiles[i].child==null)
                {
                    if (canbuild == false) canbuild = false;
                }
                else if(adjtiles[i].child.tag=="road" || adjtiles[i].child.tag=="tree")
                {
                    canbuild = true;
                }
                else
                {
                    if(canbuild==false)canbuild = false;
                }
            }
        }
        
    }
}
