using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public Soil soil;
    [Header("Time")]
    public DayCircle Daycircle;
    public enum TreeStates
    {
        young,
        adult,
        old
    }
    public enum treeClass
    {
        brzoza,dab,iglak
    }
    [Header("Varibales")]
    public treeClass treename;
    public TreeStates StartState;
    public TreeStates currTreeState;
    public float currAge;
    public float timeToAdult;
    public float timeToOld;
    public float baseWoodYield;
    public float woodYield;
    public float woodType;
    void Start()
    {
        TreeTimersSetter();
        SetStartingStage();
        SetWoodYield();
    }
    public void SetStartingStage()
    {
        switch (StartState)
        {
            case TreeStates.young:
                currAge = 0;
                break;
            case TreeStates.adult:
                currAge = timeToAdult;
                break;
            case TreeStates.old:
                currAge = timeToOld;
                break;
        }
        currTreeState = StartState;
    }

    public void TreeTimersSetter()
    {
        switch(treename)
        {
            case treeClass.brzoza:
                timeToAdult = 31556926;
                timeToOld = timeToAdult * 5;
                baseWoodYield = 200;
                break;
            case treeClass.dab:
                timeToAdult = 31556926 * 10;
                timeToOld = timeToAdult * 10;
                baseWoodYield = 800;
                break;
            case treeClass.iglak:
                timeToAdult = 31556926 / 2;
                timeToOld=timeToAdult*2.5f;
                baseWoodYield = 400;
                break;
        }
    }
    public void SetWoodYield()
    {
        switch(treename)
        {
            case treeClass.iglak:
                if(currTreeState==TreeStates.adult)
                {
                    woodYield = baseWoodYield;
                }
                else
                {
                    woodYield = baseWoodYield * 1.5f;
                }
                woodType = 0;
                break;
            case treeClass.brzoza:
                if (currTreeState == TreeStates.adult)
                {
                    woodYield = baseWoodYield;
                }
                else
                {
                    woodYield = baseWoodYield * 1.5f;
                }
                woodType = 1;
                break;
            case treeClass.dab:
                if (currTreeState == TreeStates.adult)
                {
                    woodYield = baseWoodYield;
                }
                else
                {
                    woodYield = baseWoodYield * 1.5f;
                }

                woodType = 2;
                break;
        }
    }
    public void TreeGrowthTick(float time)
    {
        
    }
}
