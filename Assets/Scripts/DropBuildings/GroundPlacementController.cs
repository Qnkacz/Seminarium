using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlacementController : MonoBehaviour
{
    public BuildingActivate BA;
    public GameObject[] PlacableObjectPrefab;
    public GameObject[] PlacableSaplings;
    public enum Buildings
    {
        road, shack,mainBuilding,storage
    }
    public enum Saplings
    {
        oak, birch, spruce
    }
    public GameObject currPlacableObject;

    void LateUpdate()
    {
        
        if(currPlacableObject!=null)
        {
            MoveCurrObjectToMouse();
            ReleaseIfClicked();
        }
        else
        {
            Click();
        }
    }

    public void Click()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.tag == "tile")
                {
                    Soil s = hit.transform.gameObject.GetComponent<Soil>();
                    if(s.child!=null)
                    {
                        if(s.child.tag=="crate")
                        {
                            if (s.child.GetComponent<BuildingToRoad>().canFunction) s.child.GetComponent<TruckManager>().sendTruck();
                        }
                        if(s.child.tag=="obstacle")
                        {
                            s.child.GetComponent<ObstacleInfo>().AwakeCanvas();
                        }
                    }
                }
            }
        }
       
    }

    public void ReleaseIfClicked()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            if (currPlacableObject.GetComponent<RoadSnap>() != null)
            {
                currPlacableObject.GetComponent<RoadSnap>().Snap();
                BA.button_ActivateMainBuildingButton();
                currPlacableObject.gameObject.layer = 14;
            }
            if(currPlacableObject.GetComponent<BuildingToRoad>()!=null)
            {
                currPlacableObject.GetComponent<BuildingToRoad>().Snap();
                BA.button_ActivateShackBuildingButton();
            }
            if(currPlacableObject.GetComponentInChildren<StorageSnap>()!=null)
            {
                currPlacableObject.GetComponentInChildren<StorageSnap>().Snap();
            }
            if(currPlacableObject.GetComponentInChildren<SaplingSnap>() != null)
            {
                currPlacableObject.GetComponentInChildren<SaplingSnap>().Snap();
            }
            currPlacableObject = null;
            
        }
    }
    private void MoveCurrObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if(Physics.Raycast(ray,out hitInfo))
        {
           
            if (hitInfo.transform.gameObject.layer!=12 )
            {
                currPlacableObject.transform.position = hitInfo.point;
                
            }
            if(currPlacableObject==null)
            {
                Debug.Log("currplacable is null");
                    if (hitInfo.transform.gameObject.tag == "tile")
                    {
                    Debug.Log("hit a tile");
                        Soil s = hitInfo.transform.gameObject.GetComponent<Soil>();
                        if (s.child.tag == "crate")
                        {

                            s.child.GetComponent<TruckManager>().sendTruck();

                        }
                    }
                
            }
        }
    }

 
    public void button_RoadButton()
    {
        if (currPlacableObject == null)
        {
            currPlacableObject = Instantiate(PlacableObjectPrefab[(int)Buildings.road]);
        }
        else Destroy(currPlacableObject);
       
    }
    public void button_ShackButton()
    {
        if (currPlacableObject == null) currPlacableObject = Instantiate(PlacableObjectPrefab[(int)Buildings.shack]);
        else Destroy(currPlacableObject);
    }
    public void button_MainBuildingButton()
    {
        if (currPlacableObject == null) currPlacableObject = Instantiate(PlacableObjectPrefab[(int)Buildings.mainBuilding]);
        else Destroy(currPlacableObject);
    }
    public void button_StorageButton()
    {
        if (currPlacableObject == null)
        {
            currPlacableObject = Instantiate(PlacableObjectPrefab[(int)Buildings.storage]);
        }
        else Destroy(currPlacableObject);
    }
    public void button_sapling_Birch()
    {
        if(currPlacableObject==null)
        {
            currPlacableObject = Instantiate(PlacableSaplings[(int)Saplings.birch]);
        }
        else Destroy(currPlacableObject);
    }
    public void button_sapling_Oak()
    {
        if (currPlacableObject == null)
        {
            currPlacableObject = Instantiate(PlacableSaplings[(int)Saplings.oak]);
        }
        else Destroy(currPlacableObject);
    }
    public void button_sapling_Spruce()
    {
        if (currPlacableObject == null)
        {
            currPlacableObject = Instantiate(PlacableSaplings[(int)Saplings.spruce]);
        }
        else Destroy(currPlacableObject);
    }
}

