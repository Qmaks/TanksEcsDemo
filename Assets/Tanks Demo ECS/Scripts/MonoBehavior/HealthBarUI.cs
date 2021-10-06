using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] 
    private Slider slider;

    public void SetHealthBar(float value)
    {
        slider.value = value;
    }
}
