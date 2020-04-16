using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLight : MonoBehaviour
{
    public Camera mainCam;
    public Light Spotlight;
    public LightingManager LM;
    private float t;
    private float timetoReach = 1f;
   
    void Update()
    {
        Spotlight.transform.LookAt(mainCam.transform);
        t = Time.time / timetoReach;
        if( .3f<LM.TimeOfDay && LM.TimeOfDay < .74f)
        {
            LightOff();
        }
        else
        {
            LightOn();
        }
        
    }
    public void LightOn()
    {
        Spotlight.intensity = 100;
    }
    public void LightOff()
    {
        Spotlight.intensity = 10;
    }
}
