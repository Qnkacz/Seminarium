using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    /// <summary>
    /// PRAWDZIWY CZAS W SEKUNDACH BRZOZY OD 0 DO 10 LAT TO 31556926
    /// </summary>
    public Soil soil;
    public GameObject axe;
    public GameObject childAxe;
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
    public float RefreshTime=100f;
    public bool setToCut;
    public bool ableToCut; //TODO

    [Header("Tree cut")]
    public bool cutSignal;
    public int CutDownTime;
    public LightingManager LM;
    void Start()
    {

        CutDownTime = 4;
        cutSignal = true;
        setToCut = false;
        ableToCut = false;
        RefreshTime = 100f;
        TreeTimersSetter();
        SetStartingStage();
        SetWoodYield();
        StartCoroutine(TreeGrowthTick(RefreshTime));
        
    }
    private void Update()
    {
        if(ableToCut&&setToCut&&cutSignal)
        {
            cutSignal = false;
            StartCoroutine(startCutting());
        }
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
                timeToAdult = 3155;
                timeToOld = timeToAdult * 5;
                baseWoodYield = 200;
                break;
            case treeClass.dab:
                timeToAdult = 3155 * 10;
                timeToOld = timeToAdult * 10;
                baseWoodYield = 800;
                break;
            case treeClass.iglak:
                timeToAdult = 3155 / 2;
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
    public IEnumerator TreeGrowthTick(float time)
    {
        while (true)
        {
            
            yield return new WaitForSeconds(time);
            currAge += time;
            //Debug.Log("Kappa po: " + time);
        }
    }
    public void SetToCut()
    {
        setToCut = true;
        SpawnAxe();
    }
    public void SetAbleToCut()
    {
        ableToCut = true;
    }
    public void SpawnAxe()
    {
        if (childAxe == null)
        {
            Instantiate(axe, this.transform);
            childAxe = this.gameObject.GetComponentInChildren<ObjectFloat>().gameObject;
            childAxe.transform.position = new Vector3(0, 8, 0);
        }
        else
        {
            childAxe.SetActive(true);
        }

    }
    public void DisableAxe()
    {
        if (childAxe != null)
        {
            childAxe.SetActive(false);
        }

    }
    public IEnumerator startCutting()
    {
        yield return new WaitForSeconds(CutDownTime);
        //dodaj drewno
        Destroy(this.gameObject);
    }
}
