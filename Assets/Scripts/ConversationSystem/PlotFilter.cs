using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotFilter : MonoBehaviour
{
    [SerializeField]
    Plot plot;

    [SerializeField]
    string expectedValue;

    public bool isActive()
    {
        return this.plot.getPlotValue() == this.expectedValue;
    }
}