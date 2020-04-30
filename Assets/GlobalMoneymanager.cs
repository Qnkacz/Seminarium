using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMoneymanager : MonoBehaviour
{
    public GlobalVariables GV;
    public static GlobalMoneymanager GMM;
    [Header("Building Costs")]
    public int cost_Road;
    public int cost_Storage;
    public int cost_Sapling;
    public int cost_Crate;
    public int cost_Main;
    public int cost_obstacle_stone;
    public void Awake()
    {
        GMM = this;
    }
    public void ChangeMoney(int amount)
    {
        GV.currMoney += amount;
        if(amount>0) BossScript.BS.ChangeGoldOnScreen(true);
        else BossScript.BS.ChangeGoldOnScreen(false);
    }
 
}
