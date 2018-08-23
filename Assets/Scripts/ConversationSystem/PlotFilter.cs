using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotFilter : MonoBehaviour
{
    [SerializeField]
    Plot plot;

    [SerializeField]
    List<int> expectedValues;

    public bool isActive()
    {
        foreach (int value in this.expectedValues)
        {
            if (this.plot.equalToPlotValue(value))
            {
                return true;
            }
        }
        return false;
    }
}