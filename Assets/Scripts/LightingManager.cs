using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    public Light DirectionalLight;
    public LightingPreset preset;
    public float TimeOfDay;
    public float sinus;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(preset==null)
        {
            return;
        }
        if(Application.isPlaying)
        {
            TimeOfDay = DayCircle.rday;
            UpdateLighting(TimeOfDay);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.FogColor.Evaluate(timePercent);
        //sinus=-Mathf.Sin(timePercent*Mathf.PI)+1;
        //RenderSettings.ambientSkyColor = sinus;

        DirectionalLight.color = preset.DirectionalColor.Evaluate(timePercent);
        DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
    }
}
