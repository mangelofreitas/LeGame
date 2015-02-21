using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {
	
	public bool moving = false;
	public Quaternion destination;
	public float rotationSpeed = 250.0f;
	private static Vector3[] posicoes = {new Vector3(0, 0, 0),new Vector3(0, 0, 90),new Vector3(0, 0, 180),new Vector3(0, 0, 270)};
	private int currentposition = 0;


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