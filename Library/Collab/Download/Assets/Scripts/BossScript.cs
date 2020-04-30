using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    public static BossScript BS;
    public GlobalVariables GV;
    public BuildingActivate BA;
    [Header("Variables")]
    public int lastMoney;
    public int currMoney;
    public int lastSaplings;
    public int currSaplings;
    public int lastGrowRt;
    public int currGrowRT;
    public int GUIRefreshRate;
    public int wood;
    [Header("GUI text objects")]
    public Text GoldText;
    public Text SaplingText;
    public Text GrowRate;
    public Text WoodYield;
    public Text BirchSaplings;
    public Text OakSaplings;
    public Text SpriceSaplings;

    [Header("ObjectReferencess")]
    public GameObject UI;
    public UIManager UIManager;
    public Animator UIAnimator;
    public DayCircle DayCircle;
    void Start()
    {
        BS = this;
        UIManager = UI.GetComponent<UIManager>();
        lastMoney=currMoney = GlobalVariables.startMoneyAmount;
        lastSaplings=currSaplings =GlobalVariables.StarSaplingAmount;
        lastGrowRt = currGrowRT = GlobalVariables.StartGrowRate;
        StartCoroutine(GUIRefresh(4));
    }
    
    
   IEnumerator GUIRefresh(float refreshRate)
   {
 
        while(true)
        {
            if(lastMoney>=currMoney)
            {
                UIAnimator.SetTrigger("G_sub");
            }
            else
            {
                UIAnimator.SetTrigger("G_add");
            }
            if (lastSaplings >= currSaplings)
            {
                UIAnimator.SetTrigger("S_sub");
            }
            else
            {
                UIAnimator.SetTrigger("s_add");
            }
          
            GoldText.text ="Gold: "+ currMoney.ToString();
            SaplingText.text ="Saplings: "+ currSaplings.ToString();
            GrowRate.text ="Grow Rate: "+currGrowRT.ToString();
            WoodYield.text = "Wood: " + wood.ToString();
            SpriceSaplings.text = "Spruce: " + GV.BirchSapling;
            OakSaplings.text = "Oak: " + GV.OakSapling;
            BirchSaplings.text = "Birch: " + GV.BirchSapling;
            BA.button_ActivateSaplings();
            yield return new WaitForSecondsRealtime(refreshRate);


            test_ChangeValues();
        }
    }

    
    void test_ChangeValues()
    {
        addGold(Random.Range(-100, 100));
        AddSaplings(Random.Range(30, 100));
    }
    void addGold(int v)
    {
        GlobalVariables.g.lastMoney=lastMoney = currMoney;
        currMoney += v;
        GlobalVariables.g.currMoney += v;
    }
    
    void AddSaplings(int v)
    {
        GlobalVariables.g.lastSaplings=lastSaplings = currSaplings;
        GlobalVariables.g.currSaplings = currSaplings += v;
    }
    void GameSpeed(float v)
    {
        GlobalVariables.g.currGrowRT = (int)DayCircle.timeScale;
    }
    
}
