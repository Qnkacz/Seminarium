using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class GlobalVariables : MonoBehaviour
{
    public static GlobalVariables g;
    [SerializeField]
    public static int startMoneyAmount;
    public static int StarSaplingAmount;
    public static int StartGrowRate;
    [Header("UIVariables")]
    public int lastMoney;
    public int currMoney;
    public int lastSaplings;
    public int currSaplings;
    public int lastGrowRt;
    public int currGrowRT;
    public int GUIRefreshRate;

    [Header("Time Variables")]
    public int currDay;
    public int currMonth;
    public int currYear;

    [Header("OptionsSliders")]
    public Slider moneySlider;
    public Slider SaplingSlider;
    public Slider growRateSlider;
    [Header("navigation")]
    public NavMeshSurface surface;
    [Header("Saplings")]
    public int BirchSapling;
    public int SpruceSapling;
    public int OakSapling;

    private 
    void Start()
    {
        g = this;
        if (moneySlider==null || SaplingSlider==null ||growRateSlider==null)
        {

        }
        else
        {
            startMoneyAmount = (int)moneySlider.value;
            StarSaplingAmount = (int)SaplingSlider.value;
            StartGrowRate = (int)growRateSlider.value;
        }
        BirchSapling = SpruceSapling = OakSapling = 0;
    }
    public void ChangeMoney()
    {
        startMoneyAmount = (int)moneySlider.value;
    }
    public void ChangeSaplings()
    {
        StarSaplingAmount = (int)SaplingSlider.value;
    }
    public void ChangeGrowRate()
    {
        StartGrowRate = (int)growRateSlider.value;
    }


}
