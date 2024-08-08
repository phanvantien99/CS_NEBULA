using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;

    [SerializeField] Image fill;
    [SerializeField] Gradient gradient;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void setMaxValue(float value)
    {
        if (!slider)
        {
            slider = GetComponent<Slider>();
        }
        slider.maxValue = value;

    }

    public void setValue(float value)
    {
        if (!slider)
        {
            slider = GetComponent<Slider>();
        }
        slider.value = value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
