
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingToRoad : MonoBehaviour
{
    bool iseverythingallright = false;
    Vector3 offset;
    public GameObject targetTile;
    public bool canFunction=false;
    public tileInfo tileInfo;
    public tileInfo[] adjtiles = new tileInfo[4];
    public tileInfo dummy;
    public GameObject exclamation;
    public GameObject childExclamation;
    public GameObject AOE;
    [Header("Transport")]
    public CrateBuilding crateBuilding;
    public TruckManager TM;
    public float DistanceToMain;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="tile")
        {
            targetTile = other.gameObject;
        }
    }
    private void Start()
    {
            targetTile = this.gameObject.transform.parent.gameObject;
            Soil s = targetTile.GetComponent<Soil>();
            tileInfo = targetTile.GetComponent<tileInfo>();
            if (s.child == null || s.child.tag == "Storage" || s.child.tag == "tree")
            {
            Destroy(s.child);
            s.child = this.gameObject;
                GetAdjTiles();
            if(!canFunction) InvokeRepeating("SeeifFunctional", .1f, 3f);
            if (this.gameObject.tag == "crate")
                {
                    if (Vector3.Distance(this.gameObject.transform.position, GlobalVariables.g.MainBuilding.transform.position) > DistanceToMain)
                    {
                    this.gameObject.GetComponentInChildren<CrateBuilding>().gameObject.SetActive(true);
                        AOE.SetActive(true);
                        AOE.layer = this.gameObject.layer;
                        crateBuilding = AOE.GetComponent<CrateBuilding>();
                        TM = this.gameObject.GetComponent<TruckManager>();
                        TM.getCrateComponents(crateBuilding, this);
                        iseverythingallright = true;
                        GlobalMoneymanager.GMM.ChangeMoney(GlobalMoneymanager.GMM.cost_Crate);
                    }

                }
                if (this.gameObject.tag == "MainBuilding")
                {
                    offset = new Vector3(0, .02f, .6f);
                    GlobalVariables.g.MainBuilding = this.gameObject;
                    BuildingActivate.BA.BuildingsButtons[1].interactable = false;
                    SetDestination();
                    GlobalMoneymanager.GMM.ChangeMoney(GlobalMoneymanager.GMM.cost_Main);
                }
            }
    }
    public void SeeifFunctional()
    {
            if (adjtiles[0].hasRoad == true|| adjtiles[1].hasRoad == true || adjtiles[2].hasRoad == true || adjtiles[3].hasRoad == true)
            {
                canFunction = true;
                //Debug.Log("no i chuj jest");
            }

            else
            {
            //Debug.Log("no chuj nie ma");
            canFunction = false;
            }
        ShowExclamation(canFunction);
        
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
        for (int i = 0; i < adjtiles.Length; i++)
        {
            if(adjtiles[i]==null)
            {
                adjtiles[i] = dummy;
            }
        }
        foreach (var item in adjtiles)
        {
            //Debug.Log(item.transform.gameObject.name);
        }
    }

    public void ShowExclamation(bool b)
    {
            exclamation.SetActive(!b);
    }
    public void SetDestination()
    {
        for (int i = 0; i < adjtiles.Length; i++)
        {
            if (adjtiles[i].GetComponent<tileInfo>().hasRoad == true)
            {
                GlobalVariables.g.Destination = adjtiles[i].GetComponent<Soil>().child.transform.position;
                adjtiles[i].GetComponent<Soil>().child.GetComponent<RoadSnap>().DestinationRaod.SetActive(true);
            }
        }
    }
}
