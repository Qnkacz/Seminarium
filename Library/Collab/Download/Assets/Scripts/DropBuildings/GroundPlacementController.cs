using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlacementController : MonoBehaviour
{
    public BuildingActivate BA;
    public GameObject[] PlacableObjectPrefab;
    public enum Buildings
    {
        road, shack,mainBuilding
    }
    public GameObject currPlacableObject;

    void LateUpdate()
    {
        
        if(currPlacableObject!=null)
        {
            MoveCurrObjectToMouse();
            ReleaseIfClicked();
        }
    }
    public void ReleaseIfClicked()
    {
        if(Input.GetMouseButtonDown(0))
        {
            currPlacableObject.gameObject.layer=11;
            if (currPlacableObject.GetComponent<RoadSnap>() != null)
            {
                currPlacableObject.GetComponent<RoadSnap>().Snap();
                BA.button_ActivateMainBuildingButton();
            }
            if(currPlacableObject.GetComponent<ObjectToRoadSnap>()!=null)
            {
                BA.button_ActivateShackBuildingButton();
                currPlacableObject.GetComponent<ObjectToRoadSnap>().Snap();
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
            if(hitInfo.transform.gameObject.layer!=12)
            {
                currPlacableObject.transform.position = hitInfo.point;
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
}
