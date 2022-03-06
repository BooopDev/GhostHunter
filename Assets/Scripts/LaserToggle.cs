using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserToggle : MonoBehaviour
{
    public ParticleSystem[] ps;
    private float[] oldValue;
    [HideInInspector]
    public static bool overHeated = false;

    private void Awake()
    {
        oldValue = new float[ps.Length];
        for (int i = 0; i < ps.Length; i++)
        {
            oldValue[i] = ps[i].emission.rateOverTime.constant;
        }
    }

    private void Update()
    {
        if(!GameOver.hasGameEnded)
        {
            if(!PauseMenu.m_Paused)
            {
                if (value > 0.99f && !overHeated)
                {
                    overHeated = true;
                }
                if (value < 0.01f && overHeated)
                {
                    overHeated = false;
                }
                if (Input.GetMouseButton(0) && !overHeated)
                {
                    ChangeSliderValue(true);
                    for (int i = 0; i < ps.Length; i++)
                    {
                        var emission = ps[i].emission;
                        emission.rateOverTime = oldValue[i];
                    }
                }
                else
                {
                    ChangeSliderValue(false);
                    for (int i = 0; i < ps.Length; i++)
                    {
                        var emission = ps[i].emission;
                        emission.rateOverTime = 0f;
                    }
                }
            }
        }
        else
        {
            ChangeSliderValue(false);
            for (int i = 0; i < ps.Length; i++)
            {
                var emission = ps[i].emission;
                emission.rateOverTime = 0f;
            }
        }
    }

    public Image fill;
    public Slider slider;
    public Color startColour;
    public Color endColour;
    private float value;
    public float overHeatSpeed;
    public float cooldownSpeed;

    public void ChangeSliderValue(bool isIncreasing)
    {
        float multiplier = 1;
        if (GhostHitCheck.hasGhostBeenHit)
        {
            multiplier = 2f;
        }

        if (isIncreasing)
        {
            value = Mathf.Clamp(value + ((overHeatSpeed * 0.1f) / multiplier) * Time.deltaTime, 0, 1);
        }
        else if(overHeated)
        {
            value = Mathf.Clamp(value - (cooldownSpeed * 0.1f) * Time.deltaTime, 0, 1);
        }
        else
        {
            value = Mathf.Clamp(value - (cooldownSpeed * 0.15f) * Time.deltaTime, 0, 1);
        }
        slider.value = value;
        fill.color = Color.Lerp(startColour, endColour, value);
    }

}
