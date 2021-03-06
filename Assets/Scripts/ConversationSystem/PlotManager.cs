﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    [SerializeField]
    List<Plot> actOnePlots;

    [SerializeField]
    List<Plot> actTwoPlots;

    [SerializeField]
    List<Plot> actThreePlots;

    [SerializeField]
    Plot currentAct;

    [SerializeField]
    Plot playerMoved;

    [SerializeField]
    SpawnManager SpawnManager;

    public static PlotManager instance = null;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {

            //if not, set instance to this
            instance = this;

            //If instance already exists and it's not this:
        }
        else if (instance != this)
        {

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        bool actOneDone = PlotManager.actIsDone(actOnePlots);
        bool actTwoDone = PlotManager.actIsDone(actTwoPlots);
        bool actThreeDone = PlotManager.actIsDone(actThreePlots);
        if (actOneDone)
        {
            if (actTwoDone)
            {
                if (actThreeDone)
                {
                    // TODO: Roll credits
                }
                else
                {
                    currentAct.setPlotValue("3");
                }
            }
            else
            {
                currentAct.setPlotValue("2");
            }
        }
        else
        {
            currentAct.setPlotValue("1");
        }
        SpawnManager.UpdateNpcs();
    } 

    public static bool actIsComplete(int act){
        if(act == 1){
            return PlotManager.actIsDone(instance.actOnePlots);
        } else if (act == 2){
            return PlotManager.actIsDone(instance.actTwoPlots);
        } else if(act == 3){
            return PlotManager.actIsDone(instance.actThreePlots);
        }
        return false;
    }

    public static bool actIsDone(List<Plot> actPlots)
    {
        bool actIsDone = false;
        foreach (Plot plot in actPlots)
        {
            if (plot.valueIsNotZero())
            {
                actIsDone = true;
            }
            else
            {
                return false;
            }
        }
        return actIsDone;
    }
}