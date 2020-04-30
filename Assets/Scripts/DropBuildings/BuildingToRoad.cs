
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
    public void Snap()
    {
        if (targetTile != null)
        {
            Soil s = targetTile.GetComponent<Soil>();
            tileInfo = targetTile.GetComponent<tileInfo>();
            if(s.child==null || s.child.tag=="Storage" || s.child.tag=="tree") // sprawdzenie czy na kratce jest drzewo albo storage
            {
                this.gameObject.transform.parent = targetTile.transform;
                
               
                GetAdjTiles();
                StartCoroutine(SeeifFunctional());

                if (this.gameObject.tag == "crate")
                {

                    if (Vector3.Distance(this.gameObject.transform.position, GlobalVariables.g.MainBuilding.transform.position) > DistanceToMain)
                    {

                        AOE.SetActive(true);
                        AOE.layer = this.gameObject.layer;
                        crateBuilding = AOE.GetComponent<CrateBuilding>();
                        TM = this.gameObject.GetComponent<TruckManager>();
                        TM.getCrateComponents(crateBuilding, this);
                        iseverythingallright = true;
                        GlobalMoneymanager.GMM.ChangeMoney(GlobalMoneymanager.GMM.cost_Crate);
                    }
                    else
                    {
                        Destroy(this.gameObject);
                    }
                    
                }
                if (this.gameObject.tag == "MainBuilding")
                {
                    offset = new Vector3(0, .02f, .6f);
                    GlobalVariables.g.MainBuilding = this.gameObject;
                    BuildingActivate.BA.BuildingsButtons[1].interactable = false;
                    SetDestination();
                    iseverythingallright = true;
                    GlobalMoneymanager.GMM.ChangeMoney(GlobalMoneymanager.GMM.cost_Main);
                }
                this.gameObject.transform.position = targetTile.transform.position + offset;
                if(iseverythingallright)
                {
                   if(s.child!=null) Destroy(s.child);
                    s.child = this.gameObject;
                }
            }
            else
            {
                Destroy(this.gameObject);
            }
           
        }
        else Destroy(this.gameObject);
    }
    public IEnumerator SeeifFunctional()
    {
        while(true)
        {
            if (adjtiles[0].hasRoad == true|| adjtiles[1].hasRoad == true || adjtiles[2].hasRoad == true || adjtiles[3].hasRoad == true)
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
        for (int i = 0; i < adjtiles.Length; i++)
        {
            if(adjtiles[i]==null)
            {
                adjtiles[i] = dummy;
            }
        }
        
    }

    public void SpawnExplamationMark()
    {
        if(childExclamation==null)
        {
            childExclamation= Instantiate(exclamation, this.transform);
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
