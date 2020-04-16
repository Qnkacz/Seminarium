using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{
    public float currSoilGrade;
    public int minSoilgrade = 0;
    public int maxSoilGrade = 4;
    public GameObject child;

    void Start()
    {
        child=gameObject.transform.GetChild(0).gameObject;
    }
}
