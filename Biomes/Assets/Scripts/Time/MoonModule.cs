using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonModule : DayNightModuleBase
{
    [SerializeField]
    private Light moon;
    [SerializeField]
    private Gradient moonColor;
    [SerializeField]
    private float baseIntensity;

    public override void UpdateModule(float intensity)
    {
        moon.color = moonColor.Evaluate(1 - intensity);
        moon.intensity = (1 - intensity) * baseIntensity + 0.05f;
    }
}
