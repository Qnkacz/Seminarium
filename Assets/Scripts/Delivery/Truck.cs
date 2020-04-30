using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Truck : MonoBehaviour
{
    public NavMeshAgent agent;
    public int baseCapacity;
    public int currCapacity;
    public int currLoad;
    public GameObject[] WoodBlocks;
    public void Awake()
    {
        currCapacity = baseCapacity;
        currLoad = 0;
    }
    public void ActiaveBlocks()
    {
        float percentage = currLoad / currCapacity;
        if (percentage >= .2) WoodBlocks[0].SetActive(true);

        if (percentage >= .4) WoodBlocks[1].SetActive(true);

        if (percentage >= .6) WoodBlocks[2].SetActive(true);

        if (percentage >= .8) WoodBlocks[3].SetActive(true);

        if (percentage >= .6) WoodBlocks[4].SetActive(true);
    }
    public IEnumerator DriveToo(Vector3 destination)
    {
        yield return new WaitForSeconds(.5f);
        agent.SetDestination(GlobalVariables.g.Destination);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="MainBuilding")
        {
            SellWood();
            
        }
    }
    public void DriveTo(Vector3 destination)
    {
        StartCoroutine(DriveToo(destination));
    }
    public void SellWood()
    {
        GlobalMoneymanager.GMM.ChangeMoney(currLoad * GlobalVariables.g.WoodValue);
        BossScript.BS.ChangeWood();
        Destroy(this.gameObject);
    }
}
