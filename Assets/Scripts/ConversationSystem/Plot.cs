using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour {
    [SerializeField]
    string plotName;

    [SerializeField]
    string startingValue;

    [SerializeField]
    string plotValue;

    public string getPlotValue () {
        return this.plotValue;
    }

    public void setPlotValue (string newValue) {
        this.plotValue = newValue;
    }
}