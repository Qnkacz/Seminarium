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
    }
    public void GradientColorChange()
    {
        
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
