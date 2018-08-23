using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	[SerializeField]
	AudioSource actOne;

	[SerializeField]
	AudioSource actTwo;

		[SerializeField]
	AudioSource actThree;

	[SerializeField]
	Plot playerMoved;
	
	// Update is called once per frame
	void Update () {
		if(playerMoved.valueIsNotZero()){
			fadeIn(actOne);
		}
		if(PlotManager.actIsComplete(1)){
			fadeIn(actTwo);
		}
		if(PlotManager.actIsComplete(2)){
			fadeIn(actThree);
		}
	}

	void fadeIn(AudioSource clip){
		while(clip.volume < 1){
			clip.volume += 0.5f * Time.deltaTime;
		}
	}
}
