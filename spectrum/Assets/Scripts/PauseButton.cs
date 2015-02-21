using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour {
	
	public void pauseSystem(){
		Time.timeScale = 0f;
	}
	public void resumeSystem(){
		Time.timeScale = 1f;
	}
}