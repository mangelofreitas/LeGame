using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour {

	bool isPaused;
	
	void Start()
	{
		isPaused = false;
	}
	
	void OnGUI()
	{ 
		if (!isPaused)
		{
			if (GUI.Button(new Rect(10,10,50,50), "Pause"))
			{
				Time.timeScale = 0f;
				isPaused = true;
			}
		}
		if (isPaused)
		{
			if (GUI.Button(new Rect(10,10,50,50), "Play"))
			{
				Time.timeScale = 1.0f;
				isPaused = false;
			}
		}
	}
}