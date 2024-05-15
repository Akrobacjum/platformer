using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    
    public Slider slider;
    public void SetCurrentValue(int value)
    {
        
        
        slider.value = value;
    }

    public void SetMaxValue(int value)
    {
        slider.maxValue = value;
    }
}
