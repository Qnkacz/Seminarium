using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingActivate : MonoBehaviour
{
   public enum BuildingsOrder
    {
        road,main,shack
    }
    public Button[] BuildingsButtons;
    void Start()
    {
        BuildingsButtons[(int)BuildingsOrder.main].interactable = false;
        BuildingsButtons[(int)BuildingsOrder.shack].interactable = false;
    }

   public void button_ActivateMainBuildingButton(){BuildingsButtons[(int)BuildingsOrder.main].interactable = true;}
    public void button_ActivateShackBuildingButton()
    {
        BuildingsButtons[(int)BuildingsOrder.shack].interactable = true;
    }
}
