using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {
	
	public bool moving = false;
	public Quaternion destination;
	public float rotationSpeed = 250.0f;
	private static Vector3[] posicoes = {new Vector3(0, 0, 0),new Vector3(90, 0, 0),new Vector3(180, 0, 0),new Vector3(270, 0, 0)};
	private int currentposition = 0;


	private float xStart = 0.0f;
	private float xEnd = 0.0f;
	private float yStart = 0.0f;
	private float yEnd = 0.0f;
	private bool sendCall = true;


	void Update(){
		if (moving) {
			if (transform.rotation == destination) {
				moving = false;	
			}
			else{
				transform.rotation = Quaternion.RotateTowards(transform.rotation,destination,rotationSpeed*Time.deltaTime);
			}
		}/*
		else{ 
			foreach (Touch touch in Input.touches) {
				
				if (touch.phase == TouchPhase.Began) {
					xStart = touch.position.x;
					yStart = touch.position.y;
				}
				if (touch.phase == TouchPhase.Moved) {
					xEnd = touch.position.x;
					yEnd = touch.position.y;
					
					if ((xStart < xEnd) ) {
						//print ("Right Swipe");
						sendCall = false;
						currentposition++;
						if(currentposition==4) currentposition=0;
						destination = Quaternion.Euler(posicoes[currentposition]);
						moving = true;
					}
					if ((xStart > xEnd) ) {
						//print ("Left Swipe");
						sendCall = false;
						currentposition--;
						if(currentposition==-1) currentposition=3;
						destination = Quaternion.Euler(posicoes[currentposition]);
						moving = true;
					}
					
				}
				
				if (touch.phase == TouchPhase.Ended) {
					xStart = 0.0f;    // resetting start and end x position.
					xEnd = 0.0f;
					sendCall = true;      //Reset to send call again after touch has been completed.

				}
				
			} */
			if (Input.GetKeyDown("d")) {
				currentposition++;
				if(currentposition==4) currentposition=0;
				destination = Quaternion.Euler(posicoes[currentposition]);
				moving = true;
			}
			else if (Input.GetKeyDown("a")){
				currentposition--;
				if(currentposition==-1) currentposition=3;
				destination = Quaternion.Euler(posicoes[currentposition]);
				moving = true;
			}
		}
	//}
	
}	