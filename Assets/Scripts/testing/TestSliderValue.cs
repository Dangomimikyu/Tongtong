using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestSliderValue : MonoBehaviour
{
    public Slider slider;
    private TMP_Text selfText;

    void Start()
    {
        selfText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        
    }

    public void UpdateValue()
    {
        selfText.text = slider.value.ToString();
    }
}
