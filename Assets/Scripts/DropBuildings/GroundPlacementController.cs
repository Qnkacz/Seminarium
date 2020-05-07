using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroundPlacementController : MonoBehaviour
{
    public static GroundPlacementController GPC;
    public BuildingActivate BA;
    public GameObject[] PlacableObjectPrefab;
    public GameObject[] PlacableSaplings;
    public enum Buildings
    {
        road, shack, mainBuilding, storage
    }
    public enum Saplings
    {
        oak, birch, spruce
    }
    public GameObject currPlacableObject;
    private void Start()
    {
        GPC = this;
    }
    void LateUpdate()
    {
        Click();
    }

    public void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.tag == "tile")
                {
                    Soil s = hit.transform.gameObject.GetComponent<Soil>();

                    if (s.child == null || s.child.tag == "tree" || s.child.tag == "Storage")
                    {
                        PutObject(hit.transform.gameObject);
                    }
                    else if (s.child.tag == "crate")
                        {
                            if (s.child.GetComponent<BuildingToRoad>().canFunction) s.child.GetComponent<TruckManager>().sendTruck();
                        }
                        else if (s.child.tag == "obstacle")
                        {
                            s.child.GetComponent<ObstacleInfo>().AwakeCanvas();
                        }
                    
                }
            }
        }

    }

    public void PutObject(GameObject obj)
    {

        if(currPlacableObject!=null)
        {
            GameObject o;
            if(currPlacableObject.tag=="road")
            {
                o = Instantiate(currPlacableObject, obj.transform.position, Quaternion.Euler(-90,0, 0));
            }
            else if(currPlacableObject.tag=="MainBuilding")
            {
                o = Instantiate(currPlacableObject, obj.transform.position+new Vector3(0,0, .6f), Quaternion.identity);
            }
            else
            {
                o = Instantiate(currPlacableObject, obj.transform.position+new Vector3(0, .3f,0), Quaternion.identity);
            }
            o.transform.parent = obj.transform;
            if (currPlacableObject.GetComponent<RoadSnap>() != null)
            {
                BA.button_ActivateMainBuildingButton();
                currPlacableObject.gameObject.layer = 14;
            }
            if (currPlacableObject.GetComponent<BuildingToRoad>() != null)
            {
                BA.button_ActivateShackBuildingButton();
            }
            if (currPlacableObject.GetComponentInChildren<StorageSnap>() != null)
            {
                currPlacableObject.GetComponentInChildren<StorageSnap>().Snap();
            }
            currPlacableObject = null;
        }
        

    }


    public void button_RoadButton()
    {
        if (currPlacableObject == null)
        {
            currPlacableObject = PlacableObjectPrefab[(int)Buildings.road];
        }
    }
    public void button_ShackButton()
    {
        if (currPlacableObject == null) currPlacableObject = PlacableObjectPrefab[(int)Buildings.shack];
    }
    public void button_MainBuildingButton()
    {
        if (currPlacableObject == null) currPlacableObject = PlacableObjectPrefab[(int)Buildings.mainBuilding];
    }
    public void button_StorageButton()
    {
        if (currPlacableObject == null)
        {
            currPlacableObject = PlacableObjectPrefab[(int)Buildings.storage];
        }
    }
    public void button_sapling_Birch()
    {
        if (currPlacableObject == null)
        {
            currPlacableObject = PlacableSaplings[(int)Saplings.birch];
        }
    }
    public void button_sapling_Oak()
    {
        if (currPlacableObject == null)
        {
            currPlacableObject = PlacableSaplings[(int)Saplings.oak];
        }
    }
    public void button_sapling_Spruce()
    {
        if (currPlacableObject == null)
        {
            currPlacableObject = PlacableSaplings[(int)Saplings.spruce];
        }
    }

}
    
    //private void MoveCurrObjectToMouse()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //        RaycastHit hitInfo;
    //        if (Physics.Raycast(ray, out hitInfo))
    //        {

    //            if (hitInfo.transform.gameObject.layer != 12)
    //            {
    //                currPlacableObject.transform.position = hitInfo.point;

    //            }
    //            if (currPlacableObject == null)
    //            {
    //                Debug.Log("currplacable is null");
    //                if (hitInfo.transform.gameObject.tag == "tile")
    //                {
    //                    Debug.Log("hit a tile");
    //                    Soil s = hitInfo.transform.gameObject.GetComponent<Soil>();
    //                    if (s.child.tag == "crate")
    //                    {
    //                        s.child.GetComponent<TruckManager>().sendTruck();
    //                    }
    //                }

    //            }
    //        }
    //    }
        
    //}

 

