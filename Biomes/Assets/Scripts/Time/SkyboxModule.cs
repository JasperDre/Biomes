using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxModule : DayNightModuleBase
{
    [SerializeField]
    private Gradient skyColor;
    [SerializeField]
    private Gradient horizonColor;

    public override void UpdateModule(float intensity)
    {
        RenderSettings.skybox.SetColor("_SkyTint", skyColor.Evaluate(intensity));
        RenderSettings.skybox.SetColor("_GroundColor", horizonColor.Evaluate(intensity));
    }
}
