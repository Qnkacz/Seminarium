using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInfo : MonoBehaviour
{
    public int x;
    public int y;

    public void setCoords(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
