using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{
    public Material material;
    public Gradient gradient;
    public GameObject storage;
    public float currSoilGrade;
    public int minSoilgrade = 0;
    public int maxSoilGrade = 4;
    public GameObject child;
    public CrateBuilding asignedCrateBuilding;
    public float DistanceToBestFert;
    public float distancenormalized;
    void Start()
    {
        child=gameObject.transform.GetChild(0).gameObject;
    }
    public void SpawnStorage()
    {
        Instantiate(storage, this.gameObject.transform.position, Quaternion.identity);
        asignedCrateBuilding.Storage_add();
    }
    public void SoilGrade(Vector3 fertilizerPos)
    {

        DistanceToBestFert = Vector3.Distance(this.gameObject.transform.position, fertilizerPos);
        distancenormalized = DistanceToBestFert / MapGenerator.mapGenerator.diagonal;
        this.gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", gradient.Evaluate(distancenormalized));
        if (distancenormalized >= 0 && distancenormalized < .25f) currSoilGrade = 4;
        if (distancenormalized >= .25f && distancenormalized < .5f) currSoilGrade = 3;
        if (distancenormalized >= .5f && distancenormalized < .75f) currSoilGrade = 2;
        if (distancenormalized >= .75f && distancenormalized <= 1) currSoilGrade = 1;
        
    }
}
