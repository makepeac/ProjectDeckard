using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActComplete : MonoBehaviour {
	[SerializeField]
    List<Plot> actOnePlots;

    [SerializeField]
    List<Plot> actTwoPlots;

    [SerializeField]
    List<Plot> actThreePlots;

	[SerializeField]
	Plot endActOne;

	[SerializeField]
	Plot endActTwo;

	[SerializeField]
	Plot endActThree;


	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		bool actOneDone = PlotManager.actIsDone(actOnePlots);
		if(actOneDone){
			endActOne.setPlotValue("1");
			return;
		}
        bool actTwoDone = PlotManager.actIsDone(actTwoPlots);
				if(actTwoDone){
			endActTwo.setPlotValue("1");
			return;
		}
        bool actThreeDone = PlotManager.actIsDone(actThreePlots);
				if(actThreeDone){
			endActThree.setPlotValue("1");
			return;
		}
	}
}
