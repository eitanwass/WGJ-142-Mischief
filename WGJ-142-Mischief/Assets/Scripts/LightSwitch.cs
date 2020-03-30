using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactble
{
    public List<Light> lights;

    public override void Interact()
    {
        foreach (Light light in lights)
        {
            light.enabled = !light.enabled;
        }
    }
}
