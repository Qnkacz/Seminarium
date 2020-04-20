using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileInfo : MonoBehaviour
{
    public int x;
    public int y;
    public int myArrayX;
    public int myArrayY;
    public bool hasRoad = false;
   
    public void setCoords(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if(other.gameObject.layer!=10)
        {
            if(other.gameObject.tag!="crate")
            {
                other.gameObject.transform.parent = this.transform;
            }
           
        }
        
    }
    public void MyPlaceInArray(int x, int y)
    {
        myArrayX = x;
        myArrayY = y;
    }
}
