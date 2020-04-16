using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFloat : MonoBehaviour
{
    public float maxTop;
    public float maxBot;
    [Range(0, 10)]
    public float animationspeed;
    public Vector3 startPos;
    public bool goUp;
    Vector3 downPos;
    Vector3 topPos;
    private void Start()
    {
        goUp = true;
        startPos = this.gameObject.transform.position;
        downPos = new Vector3(startPos.x, startPos.y - maxBot, startPos.z);
        topPos = new Vector3(startPos.x, startPos.y + maxBot, startPos.z);
        
    }
    private void Update()
    {
        if(goUp)
        {
            transform.position = Vector3.Lerp(transform.position, topPos, Time.deltaTime* animationspeed);
            if(this.transform.position==topPos)
            {
                goUp = false;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, downPos, Time.deltaTime* animationspeed);
            if(this.transform.position==downPos)
            {
                goUp = true;
            }
        }
    }

}
