using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [SerializeField]
    string plotName;

    [SerializeField]
    string startingValue;

    [SerializeField]
    string plotValue;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        this.plotValue = this.startingValue;
    }

    public string getPlotValue()
    {
        return this.plotValue;
    }

    public void setPlotValue(string newValue)
    {
        this.plotValue = newValue;
    }

    public bool equalToPlotValue(int expectedValue){
        return int.Parse(this.plotValue) == expectedValue;
    }

    public bool valueIsNotZero() {
        return int.Parse(this.plotValue) > 0;
    }
}