using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageSnap : MonoBehaviour
{
    public GameObject targetTile;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="tile")
        {
            if(other.gameObject.GetComponent<Soil>().child==null)
            {
                targetTile = other.gameObject;
            }
        }
    }
}
