using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenuSliderScript : MonoBehaviour
{
    public Text amountText;
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    private void Awake()
    {
        slider = this.gameObject.GetComponent<Slider>();

    }

    void Update()
    {
        
        amountText.text = slider.value.ToString();
        if(this.gameObject.name== "BoulderCount")
        {
            if (slider.value < .22f) amountText.text = "easy";
            else if (slider.value < .4f) amountText.text = "medium";
            else amountText.text = "hard";
        }
    }
    public void GradientColorChange()
    {
        
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
