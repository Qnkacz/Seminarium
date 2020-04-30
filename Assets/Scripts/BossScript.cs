using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    public static BossScript BS;
    public GlobalVariables GV;
    public BuildingActivate BA;
    public int lastSaplings;
    public int currSaplings;
    public int lastGrowRt;
    public int currGrowRT;
    public int GUIRefreshRate;
    [Header("GUI text objects")]
    public Text GoldText;
    public Text SaplingText;
    public Text GrowRate;
    public Text WoodYield;
    public Text BirchSaplings;
    public Text OakSaplings;
    public Text SpriceSaplings;
    public Button MainBuildingButton;

    [Header("ObjectReferencess")]
    public GameObject UI;
    public UIManager UIManager;
    public Animator UIAnimator;
    public DayCircle DayCircle;
    void Start()
    {
        BS = this;
        UIManager = UI.GetComponent<UIManager>();
        GoldText.text ="Gold: "+ GV.currMoney.ToString();
    }
    public void ChangeGoldOnScreen(bool ifadded)
    {
        GoldText.text = "Gold: " +GlobalVariables.g.currMoney.ToString();
        if(ifadded) UIAnimator.SetTrigger("G_add");
        else UIAnimator.SetTrigger("G_sub");
    }
    public void CheckSaplings()
    {
        BirchSaplings.text = "Birch: " + GV.BirchSapling;
        OakSaplings.text = "Oak: " + GV.OakSapling;
        SpriceSaplings.text = "Spruce: " + GV.SpruceSapling;
        if (GV.BirchSapling > 0) UIManager.Birch.interactable = true;
        else UIManager.Birch.interactable = false;


        if (GV.OakSapling > 0) UIManager.Oak.interactable = true;
        else UIManager.Oak.interactable = false;

        if (GV.SpruceSapling > 0) UIManager.Spruce.interactable = true;
        else UIManager.Spruce.interactable = false;
    }
    public void ChangeWood()
    {
        WoodYield.text = "Wood: " + GV.WoodValue;
    }
    
}
