using UnityEngine;
using System;
using System.Collections.Generic;
using UniRx;

public class Sample : MonoBehaviour
{
    public UIButton button;
    public UISlider slider;

    void Start()
    {
        button.onClick.AsObservable().Subscribe(_ => {
            
            Debug.Log("button is clicked!");

            slider.value -= 0.1f;
        });

        slider.OnValueChangedAsObservable().Subscribe( _=> Debug.Log("slider is moved") );
    }

    public void OnClickMe()
    {
        Debug.Log("Clicked me!");
    }
}
