using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingActivate : MonoBehaviour
{
    public GlobalVariables GV;
   public enum BuildingsOrder
   {
        road,main,shack
   }
    public enum Saplings
    {
        birch,oak,spruce
    }
    public Button[] BuildingsButtons;
    public Button[] SaplingsButtons;
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

    public void button_ActivateSaplings()
    {
        check_Sapling(SaplingsButtons, 0, GV.BirchSapling);
        check_Sapling(SaplingsButtons, 1, GV.OakSapling);
        check_Sapling(SaplingsButtons, 2, GV.SpruceSapling);
    }
    private void check_Sapling(Button[] b,int place,int amount)
    {
        if (amount > 0)
        {
            b[place].interactable = true;
        }
        else b[place].interactable = false;
    }
}
