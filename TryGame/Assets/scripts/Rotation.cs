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
	public bool moveLeft = false;
	public bool moveRight = false;


	void Update(){
		if (moving) {
			if (transform.rotation == destination) {
				moving = false;	
			}
			else{
				transform.rotation = Quaternion.RotateTowards(transform.rotation,destination,rotationSpeed*Time.deltaTime);
			}
		}
		else{
			if (moveLeft) {
				currentposition++;
				if(currentposition==4) currentposition=0;
				destination = Quaternion.Euler(posicoes[currentposition]);
				moving = true;
				moveLeft = false;
			}
			else if (moveRight){
				currentposition--;
				if(currentposition==-1) currentposition=3;
				destination = Quaternion.Euler(posicoes[currentposition]);
				moving = true;
				moveRight = false;
			}
		}
	}
	
}	