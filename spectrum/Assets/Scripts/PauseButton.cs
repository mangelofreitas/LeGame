using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour {

	public GameObject pauseB;
	public GameObject player;
	public Text Counter123;
	public Text text1;
	public Text text2;
	public Text text3;
	public Text text4;
	public Text text5;
	public Text text7;

	public void pauseSystem(){
		player.GetComponent<swipe>().enabled = false;
		text1.enabled = false;
		text2.enabled = false;
		text3.enabled = false;
		text4.enabled = false;
		text5.enabled = false;
		text7.enabled = false;
		Time.timeScale = 0f;
	}
	public void resumeSystem(){
		Counter123.enabled = true;
		text1.enabled = true;
		text2.enabled = true;
		text3.enabled = true;
		text4.enabled = true;
		text5.enabled = true;
		text7.enabled = true;
		StartCoroutine(ResumeAfterSeconds(3));
	}
	
	private IEnumerator ResumeAfterSeconds(int resumetime) // 3
	{
		Time.timeScale = 0.0001f;
		float pauseEndTime = Time.realtimeSinceStartup + resumetime; // 10 + 4 = 13
		
		float number3 = Time.realtimeSinceStartup + 1; // 10 + 1 = 11
		float number2 = Time.realtimeSinceStartup + 2; // 10 + 2 = 12
		float number1 = Time.realtimeSinceStartup + 3; // 10 + 3 = 13
		pauseB.SetActive(false);
		while (Time.realtimeSinceStartup < pauseEndTime) // 10 < 13
		{
			if(Time.realtimeSinceStartup <= number3)      // 10 < 11
				Counter123.text = "3";
			else if(Time.realtimeSinceStartup <= number2) // 11 < 12
				Counter123.text = "2";
			else if(Time.realtimeSinceStartup <= number1) // 12 < 13
				Counter123.text = "1";
			
			yield return null;
		}
		pauseB.SetActive(true);
		Counter123.enabled = false;
		Time.timeScale = 1f;
		player.GetComponent<swipe>().enabled = true;
	}

	public void restartSystem(){
		Application.LoadLevel(Application.loadedLevel);
		print (Time.timeScale);
	}
}