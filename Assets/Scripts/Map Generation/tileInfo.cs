﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileInfo : MonoBehaviour
{
    public int x;
    public int y;
   
    public void setCoords(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if(other.gameObject.tag=="tree" || other.gameObject.tag=="boulder")
        {
            other.gameObject.transform.parent = this.transform;
        }
        
    }
}
