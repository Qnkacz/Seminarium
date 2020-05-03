using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fertilizer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="tile")
        {
            Soil s = other.gameObject.GetComponent<Soil>();
            s.SoilGrade(this.gameObject.transform.position);
        }
    }
}
