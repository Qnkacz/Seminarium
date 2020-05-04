using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class GlobalVariables : MonoBehaviour
{
    public static GlobalVariables g;
    [Header("Things from stettings")]
    public static int startMoneyAmount;
    public static int Start_FertCount;
    public static int Start_mapSize;
    public static int Start_mapSeed;
    public static float start_boulderCount;
    [Header("UIVariables")]
    public int currMoney;
    public int curr_fertCount;
    public int curr_mapsize;
    public int curr_mapseed;

    [Header("Time Variables")]
    public int currDay;
    public int currMonth;
    public int currYear;

    [Header("OptionsSliders")]
    public Slider moneySlider;
    public Slider Fertilize;
    public Slider MapSize;
    public Slider mapSeed;
    public Slider obstacles;
    [Header("navigation")]
    public NavMeshSurface surface;
    [Header("Saplings")]
    public int BirchSapling;
    public int SpruceSapling;
    public int OakSapling;
    [Header("transport")]
    public GameObject MainBuilding;
    public Vector3 Destination;
    public int WoodValue;
    public int roadCount;
    public int FertilizerCount { get; internal set; }

    private 
    void Start()
    {
        g = this;
        if (moneySlider==null || Fertilize == null || MapSize == null)
        {
            currMoney = startMoneyAmount;
            curr_mapseed = Start_mapSeed;
            curr_fertCount = Start_FertCount;
            curr_mapsize = Start_mapSize;
            BossScript.BS.ChangeGoldOnScreen(true);
        }
        else
        {
            startMoneyAmount = (int)moneySlider.value;
            Start_FertCount = (int)Fertilize.value;
            Start_mapSize = (int)MapSize.value;
            Start_mapSeed = (int)mapSeed.value;
        }
        BirchSapling = SpruceSapling = OakSapling = 0;
        roadCount = 0;
    }
    public void ChangeMoney()
    {
        startMoneyAmount = (int)moneySlider.value;
    }
    public void ChangeVertilize()
    {
        Start_FertCount = (int)Fertilize.value;
    }
    public void ChangeMapSize()
    {
        Start_mapSize = (int)MapSize.value;
    }
    public void ChangeSeed()
    {
        Start_mapSeed = (int)mapSeed.value;
    }
    public void ChangeObstacles()
    {
        start_boulderCount =obstacles.value;
    }
}
