using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotTrigger : MonoBehaviour
{

    [SerializeField]
    Plot plot;

    [SerializeField]
    string newValue;

    public Plot getPlot()
    {
        return this.plot;
    }

    public string getNewValue()
    {
        return this.newValue;
    }
}
