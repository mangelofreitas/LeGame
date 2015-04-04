using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

	public Text Hscore;
	void Start () {
		int _highscore = PlayerPrefs.GetInt("HighScore");
		Hscore.text = "HighScore: " + _highscore;

	}
	

}
