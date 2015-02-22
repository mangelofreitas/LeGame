using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class ColorManagement : MonoBehaviour {
	
	public Text scoreText;
	public int score;
	
	public Text countRed;
	public Text countGreen;
	public Text countBlue;
	
	
	void Start () {
		score = 0;
		countRed.text = "Red: ";
		countRed.color = Color.red;
		countGreen.text = "Green: ";
		countGreen.color = Color.green;
		countBlue.text = "Blue: ";
		countBlue.color = Color.blue;
		UpdateScore ();
	}
	
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}
	
	public void addContador (Color cor,int value)
	{
		if (cor == Color.red) {
			if(value==1){
				countRed.text+="|";
			}
			else if(value == -3){
				countRed.text = countRed.text.Replace("|","");
			}
		}
		else if (cor == Color.blue) {
			if(value==1){
				countBlue.text+="|";
			}
			else if(value == -3){
				countBlue.text = countBlue.text.Replace("|","");
			}		
		}
		else if(cor == Color.green){
			if(value==1){
				countGreen.text+="|";
			}
			else if(value == -3){
				countGreen.text = countGreen.text.Replace("|","");
			}
		}
	}
}
