using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{

    public GameObject storage;
    public float currSoilGrade;
    public int minSoilgrade = 0;
    public int maxSoilGrade = 4;
    public GameObject child;
    public CrateBuilding asignedCrateBuilding;
    void Start()
    {
        child=gameObject.transform.GetChild(0).gameObject;
    }
    public void SpawnStorage()
    {

        Instantiate(storage, this.gameObject.transform.position, Quaternion.identity);
        asignedCrateBuilding.Storage_add();
    }
}
